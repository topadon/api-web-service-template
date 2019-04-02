using Interface.Controller;
using Model.Alive;
using System;
using System.Web.Http;

namespace API.Internal.Controllers
{
    public class AliveController : ApiController, IAlive
    {
        [HttpGet]
        public AliveResponse CheckAlive()
        {
            return new AliveResponse { isAlive = true };
        }
    }
}