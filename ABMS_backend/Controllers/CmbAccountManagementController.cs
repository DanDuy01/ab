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

        [HttpPost("cmb-account/create")]
        public ResponseData<string> Create([FromBody] AccountForInsertDTO dto)
        {
            ResponseData<string> response = _service.createCmbAccount(dto);
            return response;
        }

        [HttpPut("cmb-account/update/{id}")]
        public ResponseData<string> Update(String id, [FromBody] AccountForInsertDTO dto)
        {
            ResponseData<string> response = _service.updateCmbAccount(id, dto);
            return response;
        }

        [HttpDelete("cmb-account/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _service.deleteCmbAccount(id);
            return response;
        }
    }
}
