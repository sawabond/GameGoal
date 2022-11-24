using MediatR;
using System.Security.Claims;

namespace Presentation.Controllers;

public abstract class AuthorizedApiController : ApiController
{
    protected AuthorizedApiController(ISender sender) : base(sender)
    {
    }

    protected string UserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString();
}
