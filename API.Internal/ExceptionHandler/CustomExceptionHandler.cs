using Model.Base;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace API.Internal.ExceptionHandler
{
    internal class CustomExceptionHandler : System.Web.Http.ExceptionHandling.ExceptionHandler
    {
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            ResponseInfo<string> result = new ResponseInfo<string>();
            result.ResponseCode = "999";
            result.ResponseMsgTh = "เกิดข้อผิดพลาดบางประการ!";
            result.ResponseMsgEn = "System Exception!";

            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, result);

            context.Result = new ResponseMessageResult(response);

            return base.HandleAsync(context, cancellationToken);
        }
    }
}