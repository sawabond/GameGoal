using Application.AppUsers.Commands.CreateUser;
using Application.AppUsers.ViewModels;
using Application.Extensions;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;

namespace Application.Services;

public sealed class UserRegistrer : IUserRegistrer
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public UserRegistrer(
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
    public async Task<Result> RegisterUserAsync(CreateUserCommand request)
    {
        var company = await _uow.UserRepository.GetUserIncludingAll(request.CompanyId);

        var existingUser = company
            ?.CompanyMembers
            .Where(cm => cm.UserName == request.UserName)
            .FirstOrDefault();

        if (existingUser is not null)
        {
            return Result.Fail().WithError($"User with name {request.UserName} already exists");
        }

        var newUser = _mapper.Map<AppUser>(request);

        var result = await _userService.CreateUserAsync(newUser, request.Password);

        if (!result.Succeeded)
        {
            return Result.Fail().WithErrors(result.Errors.Select(e => e.Description));
        }

        var roleResult = await _userService.AddToRoleAsync(newUser, request.Role);

        if (!roleResult.Succeeded)
        {
            return Result.Fail().WithErrors(roleResult.Errors.Select(e => e.Description));
        }

        var authResult = await _tokenService.CreateToken(newUser);

        if (!authResult.IsSuccess)
        {
            return Result.Fail().WithErrors(authResult.Errors);
        }

        var userViewModel = _mapper.Map<AppUserViewModel>(newUser);
        userViewModel.Token = authResult.Value;

        var hasCompanyId = !request.CompanyId.IsEmptyGuid();

        if (hasCompanyId)
        {
            company.CompanyMembers.Add(newUser);
            await _uow.ConfirmAsync();
        }

        return Result.Success();
    }
}
