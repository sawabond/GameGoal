using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize(Roles = RoleConstants.User)]
public sealed class SmokerSystemController : AuthorizedApiController
{
    public SmokerSystemController(ISender sender) : base(sender)
    {

    }

}
