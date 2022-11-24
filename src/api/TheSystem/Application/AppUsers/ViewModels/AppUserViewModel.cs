using Application.Abstractions;

namespace Application.AppUsers.ViewModels;

public sealed class AppUserViewModel : IViewModel
{
    public string Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string Token { get; set; }
}
