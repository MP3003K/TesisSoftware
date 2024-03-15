using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Base
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}

