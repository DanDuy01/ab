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
        private Mock<HttpContextAccessor> _httpContextAccessorMock;
        private Mock<abmsContext> _abmsContextMock;
        private HotlineManagementService _holineService;
  
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
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var abmsContextMock = new Mock<abmsContext>();
            var service = new HotlineManagementService(abmsContextMock.Object, httpContextAccessorMock.Object);
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

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var abmsContextMock = new Mock<abmsContext>(); // Thay AbmsContext bằng tên của context của bạn

            var service = new HotlineManagementService(abmsContextMock.Object, httpContextAccessorMock.Object);// Thay HotlineService bằng tên của lớp chứa phương thức createHotline của bạn

            // Act
            var result = service.createHotline(dto);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.IsNotNull(result.ErrMsg);
            Assert.IsNull(result.Data);
        }
    }
}