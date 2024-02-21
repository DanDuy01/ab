using Microsoft.AspNetCore.Mvc;
using ABMS_backend.Utils.Validates;
using ABMS_backend.DTO;
using ABMS_backend.Repositories;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class CmbAccountManagementController : ControllerBase
    {
        private ICmbAccountManagementRepository _service;

        public CmbAccountManagementController(ICmbAccountManagementRepository service)
        {
            _service = service;
        }

<<<<<<< HEAD
        [HttpPost("CmbAccount/create")]
        public IActionResult Create([FromBody] AccountDTO dto)
=======
        [HttpPost("cmb-account/create")]
        public ResponseData<string> Create([FromBody] AccountForInsertDTO dto)
>>>>>>> ae8801e6f333eda5eeb0e6347c14a65027ee5e0b
        {
            ResponseData<string> response = _service.createCmbAccount(dto);
            return response;
        }
    }
}
