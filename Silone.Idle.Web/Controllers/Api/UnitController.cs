using System.Linq;
using System.Web.Http;
using Silone.Idle.Api;
using Silone.Idle.Data.Services;

namespace Silone.Idle.Web.Controllers.Api
{
    [RoutePrefix("api/unit")]
    public class UnitController : ApiController
    {
        [HttpGet]
        [Route("")]
        public DtoUnit GetUnit()
        {
            var unit = PersonService.Units.First();

            return unit.GetDto();
        }
    }
}
