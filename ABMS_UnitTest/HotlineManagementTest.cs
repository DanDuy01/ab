using ABMS_backend.DTO.HotlineDTO;
using ABMS_backend.Models;
using ABMS_backend.Services;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using System.Net;

namespace ABMS_UnitTest
{
    [TestClass]
    public class HotlineManagementTest
    {
        private readonly abmsContext _abmsContext;
        public HotlineManagementTest()
        {
            _abmsContext = new abmsContext();
            
        }

        [TestMethod]
        public void CreateHotline_Test()
        {
            // Arrange
            HotlineForInsertDTO dto = new HotlineForInsertDTO
            {
                phoneNumber = "0963535117",
                name = "name",
                buildingId = "1"
            };

            var service = new HotlineManagementService(_abmsContext, null);
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

            var service = new HotlineManagementService(_abmsContext, null);
            // Act
            var result = service.createHotline(dto);
            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.IsNotNull(result.ErrMsg);
            Assert.IsNull(result.Data);
        }


        

    }
}