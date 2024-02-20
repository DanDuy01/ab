using Microsoft.AspNetCore.Mvc;
using ABMS_backend.Utils.Validates;
using ABMS_backend.Services;
using ABMS_backend.DTO;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class CmbAccountManagementController : ControllerBase
    {
        private ICmbAccountManagementService _service;

        public CmbAccountManagementController(ICmbAccountManagementService service)
        {
            _service = service;
        }

        [HttpPost("CmbAccount/create")]
        public IActionResult Create([FromBody] CmbAccountForInsertDTO dto)
        {
            _service.createCmbAccount(dto);
            return Ok();
        }
    }
}
