using CustomerCardService.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CustomerCardService.Api.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            var contextException = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var responseStatusCode = contextException.Error.GetType().Name switch
            {
                nameof(CardNotFoundException) => HttpStatusCode.BadRequest,
                nameof(TokenExpiredException) => HttpStatusCode.BadRequest,
                nameof(InconsistentCardException) => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError

            };
            return Problem(detail: contextException.Error.Message, statusCode: (int)responseStatusCode);
        }
    }
}
