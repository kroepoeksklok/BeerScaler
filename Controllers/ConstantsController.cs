using BeerScaler.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BeerScaler.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ConstantsController : ControllerBase {
        [HttpGet()]
        public ActionResult<ConstantsDto> Get() {
            return new ConstantsDto();
        }
    }
}
