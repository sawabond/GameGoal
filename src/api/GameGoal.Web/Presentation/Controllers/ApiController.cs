using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public abstract class ApiController : ControllerBase
    {
        protected readonly ISender _sender;

        protected ApiController(ISender sender)
        {
            _sender = sender;
        }
    }
}
