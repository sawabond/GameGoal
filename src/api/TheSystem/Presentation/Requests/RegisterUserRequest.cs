namespace Presentation.Requests;

public sealed record RegisterUserRequest
    (string UserName,
    string Password);
