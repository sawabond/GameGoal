using Application.Abstractions.Messaging;
using Application.AppUsers.ViewModels;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;

namespace Application.AppUsers.Commands.CreateUser;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public CreateUserCommandHandler(
        IUnitOfWork uow, 
        IMapper mapper, 
        IUserService userService,
        ITokenService tokenService)
    {
        _uow = uow;
        _mapper = mapper;
        _userService = userService;
        _tokenService = tokenService;
    }
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _uow.UserRepository.GetUserByUsernameAsync(request.UserName);

        if (existingUser is not null)
        {
            return Result.Fail().WithError("User with this name already exists");
        }

        var newUser = _mapper.Map<AppUser>(request);

        var result = await _userService.CreateUserAsync(newUser, request.Password);

        if (!result.Succeeded)
        {
            return Result.Fail().WithErrors(result.Errors.Select(e => e.Description));
        }

        var authResult = await _tokenService.CreateToken(newUser);

        if (!authResult.IsSuccess)
        {
            return Result.Fail().WithErrors(authResult.Errors);
        }

        var userViewModel = _mapper.Map<AppUserViewModel>(newUser);
        userViewModel.Token = authResult.Value;

        return Result.Success();
    }
}
