using ABMS_backend.DTO.HotlineDTO;
using ABMS_backend.DTO.PostDTO;
using ABMS_backend.Models;
using ABMS_backend.Services;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using System.Net;

namespace ABMS_UnitTest
{
    [TestClass]
    public class HotlineManagementTest
    {
        //private readonly abmsContext _abmsContext;
        //public HotlineManagementTest()
        //{
        //    _abmsContext = new abmsContext();

        //}

        //[TestMethod]
        //public void CreateHotline_Test1()
        //{
        //    // Arrange
        //    HotlineForInsertDTO dto = new HotlineForInsertDTO
        //    {
        //        phoneNumber = "0963535117",
        //        name = "name",
        //        buildingId = "1"
        //    };

        //    var service = new HotlineManagementService(_abmsContext, null);
        //    // Act
        //    var result = service.createHotline(dto);
        //    // Assert
        //    Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        //}


        //[TestMethod]
        //public void CreateHotline_Test2()
        //{
        //    // Arrange
        //    var dto = new HotlineForInsertDTO
        //    { 
        //        phoneNumber = "123456789",
        //        name = "", // Invalid name
        //        buildingId = "1"
        //    };

        //    var service = new HotlineManagementService(_abmsContext, null);
        //    // Act
        //    var result = service.createHotline(dto);
        //    // Assert
        //    Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
        //    Assert.IsNotNull(result.ErrMsg);
        //    Assert.IsNull(result.Data);
        //}

        //[TestMethod]
        //public void CreateHotline_Test3()
        //{
        //    // Arrange
        //    var dto = new HotlineForInsertDTO
        //    {
        //        phoneNumber = "",
        //        name = "", // Invalid name
        //        buildingId = ""
        //    };

        //    var service = new HotlineManagementService(_abmsContext, null);
        //    // Act
        //    var result = service.createHotline(dto);
        //    // Assert
        //    Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
        //    Assert.IsNotNull(result.ErrMsg);
        //    Assert.IsNull(result.Data);
        //}


        //[TestMethod]
        //public void DeleteHotline_Test1()
        //{
        //    // Arrange
        //    // Thêm một đối tượng hotline vào cơ sở dữ liệu để kiểm tra việc xóa
        //    var hotline = new Hotline { Id = "2", PhoneNumber = "0963535176", Name = "name", BuildingId = "1" };
        //    _abmsContext.Hotlines.Add(hotline);
        //    _abmsContext.SaveChanges();

        //    var service = new HotlineManagementService(_abmsContext, null);

        //    // Act
        //    var result = service.deleteHotline("2");

        //    // Assert
        //    Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        //}

        private Mock<abmsContext> _contextMock;
        private HotlineManagementService _hotlineService;

        [TestInitialize]
        public void Setup()
        {
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _contextMock = new Mock<abmsContext>();
            var httpContextMock = new Mock<HttpContext>();
            var httpRequestMock = new Mock<HttpRequest>();
            httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContextMock.Object);
            _hotlineService = new HotlineManagementService(_contextMock.Object, httpContextAccessorMock.Object); // Thay thế YourDbContext bằng lớp DbContext thực tế của bạn
        }


        [TestMethod]
        public void createHotline_Test1()
        {
            // Chuẩn bị dữ liệu DTO
            var dto = new HotlineForInsertDTO
            {
                phoneNumber = "0963535176",
                name = "name",
                buildingId = "1"
            };

            // Giả mạo thêm dữ liệu khi thêm bài đăng
            _contextMock.Setup(context => context.Hotlines.Add(It.IsAny<Hotline>()));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);

            // Thực thi hàm
            var result = _hotlineService.createHotline(dto);

            // Kiểm tra kết quả

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void createHotline_Test2()
        {
            // Chuẩn bị dữ liệu DTO
            var dto = new HotlineForInsertDTO
            {
                phoneNumber = "0963535176",
                name = "",
                buildingId = "1"
            };

            // Giả mạo thêm dữ liệu khi thêm bài đăng
            _contextMock.Setup(context => context.Hotlines.Add(It.IsAny<Hotline>()));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);

            // Thực thi hàm
            var result = _hotlineService.createHotline(dto);

            // Kiểm tra kết quả

            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [TestMethod]
        public void createHotline_Test3()
        {
            // Chuẩn bị dữ liệu DTO
            var dto = new HotlineForInsertDTO
            {
                phoneNumber = "",
                name = "",
                buildingId = ""
            };

            // Giả mạo thêm dữ liệu khi thêm bài đăng
            _contextMock.Setup(context => context.Hotlines.Add(It.IsAny<Hotline>()));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);

            // Thực thi hàm
            var result = _hotlineService.createHotline(dto);

            // Kiểm tra kết quả

            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [TestMethod]
        public void DeleteHotline_Test()
        {
            // Arrange
            string existingId = "existingId";
            Hotline existingHotline = new Hotline { Id = existingId };

            _contextMock.Setup(x => x.Hotlines.Find(existingId)).Returns(existingHotline);

            // Act
            var result = _hotlineService.deleteHotline(existingId);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            
        }

        [TestMethod]
        public void DeleteHotline_Test2()
        {
            string nullId = null;

            _contextMock.Setup(x => x.Hotlines.Find(nullId)).Returns((Hotline)null);

            // Act
            var result = _hotlineService.deleteHotline(nullId);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);

        }


        [TestMethod]
        public void updateHoline_Test1()
        {
            // Chuẩn bị dữ liệu DTO
            var dto = new HotlineForInsertDTO
            {
                phoneNumber = "0963535176",
                name = "name",
                buildingId = "1"
            };

            // Giả mạo dữ liệu bài đăng tồn tại
            var existingHoline = new Hotline
            {
                Id = "1",
                PhoneNumber = "0963535176",
                Name = "name",
                BuildingId = "1"
            };

            // Giả mạo Context để trả về bài đăng tồn tại khi được truy vấn
            _contextMock.Setup(context => context.Hotlines.Find(It.IsAny<string>())).Returns(existingHoline);

            // Thực thi phương thức updatePost()
            var result = _hotlineService.updateHotline("1", dto);

            // Kiểm tra kết quả    
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            
        }

        [TestMethod]
        public void updateHoline_Test2()
        {
            // Chuẩn bị dữ liệu DTO
            var dto = new HotlineForInsertDTO
            {
                phoneNumber = "0963535176",
                name = "",
                buildingId = "1"
            };

            // Giả mạo dữ liệu bài đăng tồn tại
            var existingHoline = new Hotline
            {
                Id = "1",
                PhoneNumber = "0963535176",
                Name = "name",
                BuildingId = "1"
            };

            // Giả mạo Context để trả về bài đăng tồn tại khi được truy vấn
            _contextMock.Setup(context => context.Hotlines.Find(It.IsAny<string>())).Returns(existingHoline);

            // Thực thi phương thức updatePost()
            var result = _hotlineService.updateHotline("1", dto);

            // Kiểm tra kết quả    
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);

        }

        [TestMethod]
        public void updateHoline_Test3()
        {
            // Chuẩn bị dữ liệu DTO
            var dto = new HotlineForInsertDTO
            {
                phoneNumber = "",
                name = "",
                buildingId = ""
            };

            // Giả mạo dữ liệu bài đăng tồn tại
            var existingHoline = new Hotline
            {
                Id = "1",
                PhoneNumber = "0963535176",
                Name = "name",
                BuildingId = "1"
            };

            // Giả mạo Context để trả về bài đăng tồn tại khi được truy vấn
            _contextMock.Setup(context => context.Hotlines.Find(It.IsAny<string>())).Returns(existingHoline);

            // Thực thi phương thức updatePost()
            var result = _hotlineService.updateHotline("1", dto);

            // Kiểm tra kết quả    
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);

        }

    }
}