using Interface.Controller;
using Model.Alive;
using System.Web.Http;

namespace API.Gateway.Controllers
{
    public class AliveController : BaseController, IAlive
    {
        [AllowAnonymous]
        [HttpGet]
        public AliveResponse CheckAlive()
        {
            return base.GetDataFromAPINotAuthen<AliveResponse>(apiPathAndQuery);
        }
    }
}