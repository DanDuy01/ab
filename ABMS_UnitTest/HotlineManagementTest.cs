using ABMS_backend.DTO.HotlineDTO;
using ABMS_backend.Models;
using ABMS_backend.Services;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Net;

namespace ABMS_UnitTest
{
    [TestClass]
    public class HotlineManagementTest
    {
        private readonly abmsContext _abmsContext;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HotlineManagementTest()
        {
            _abmsContext = new abmsContext();
            _httpContextAccessor = new HttpContextAccessor();
        }

        [TestMethod]
        public void CreateHotline_Test()
        { 
            // Arrange
            HotlineForInsertDTO dto = new HotlineForInsertDTO
            {
                phoneNumber = "0963535178",
                name = "name",
                buildingId = "1"
            };

            var service = new HotlineManagementService(_abmsContext, _httpContextAccessor);
            // Act
            var result = service.createHotline(dto);
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            
        }


        [TestMethod]
        public void CreateHotline_InvalidData_ReturnsInternalServerErrorWithError()
        {
            // Arrange
            var dto = new HotlineForInsertDTO
            { 
                phoneNumber = "123456789",
                name = "", // Invalid name
                buildingId = "1"
            };

            var service = new HotlineManagementService(_abmsContext, _httpContextAccessor);

            // Act
            var result = service.createHotline(dto);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.IsNotNull(result.ErrMsg);
            Assert.IsNull(result.Data);
        }
    }
}