using Application.Abstractions.Messaging;
using Application.AppUsers.Commands.CreateUser;
using Application.Services.Abstractions;
using Domain;
using Domain.Shared;

namespace Application.AppUsers.Commands.CreateUserFromFile;

public sealed class CreateUsersFromFileCommandHandler : ICommandHandler<CreateUsersFromFileCommand>
{
    private readonly IUserRegistrer _registrer;

    public CreateUsersFromFileCommandHandler(IUserRegistrer registrer)
    {
        _registrer = registrer;
    }
    public async Task<Result> Handle(CreateUsersFromFileCommand request, CancellationToken cancellationToken)
    {
        if (request.FileForm is null)
        {
            return Result.Fail().WithError("File is not provided");
        }

        using var readStream = new StreamReader(request.FileForm.OpenReadStream());

        Dictionary<string, string> nickPassPairs = new();

        try
        {
            while (readStream.Peek() >= 0)
            {
                var line = readStream.ReadLine();

                var nick = line.Split(':')[0];
                var pass = line.Split(':')[1];

                nickPassPairs.Add(nick, pass);
            }
        }
        catch
        {
            return Result.Fail().WithError("Unable to parse the file content");
        }
        
        var results = new List<Result>();

        nickPassPairs.ToList().ForEach(async pair =>
        {
            var command = new CreateUserCommand
            {
                UserName = pair.Key,
                Password = pair.Value,
                Role = RoleConstants.User,
                CompanyId = request.CompanyId
            };

            var result = await _registrer.RegisterUserAsync(command);
            results.Add(result);
        });

        return results.All(r => r.IsSuccess)
            ? Result.Success()
            : Result.Fail().WithErrors(results.SelectMany(r => r.Errors));
    }
}
