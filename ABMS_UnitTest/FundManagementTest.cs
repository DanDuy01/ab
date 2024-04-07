using ABMS_backend.DTO.FundDTO;
using ABMS_backend.DTO.PostDTO;
using ABMS_backend.Models;
using ABMS_backend.Services;
using ABMS_backend.Utils.Validates;
using ABMS_UnitTest.TestMocks;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Net;


namespace ABMS_UnitTest
{
    [TestClass]
    public class FundManagementTest
    {
        private FundManagementService _fundService;
        private Mock<abmsContext> _contextMock;


        [TestInitialize]
        public void Setup()
        {
            // Giả mạo HttpContextAccessor
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            // Giả mạo HttpContext và HttpRequest
            var httpContextMock = new Mock<HttpContext>();
            var httpRequestMock = new Mock<HttpRequest>();

            // Tạo một token giả mạo
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyX2lkIiwianRpIjoiMTIzNDU2Nzg5MCIsImlhdCI6MTY0NTc5NzExMywiZXhwIjoxNjQ1Nzk3NzEzfQ.GkBy5-3S9qKzYtVurW6l-lBG7_QZPLtNM1p1RuANsk4";
            httpRequestMock.Setup(req => req.Headers["Authorization"]).Returns(token);
            httpContextMock.SetupGet(c => c.Request).Returns(httpRequestMock.Object);
            httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContextMock.Object);

            // Giả mạo HttpContextAccessor trong service
            _contextMock = new Mock<abmsContext>();
            _fundService = new FundManagementService(_contextMock.Object, httpContextAccessorMock.Object);
        }


        [TestMethod]
        public void createFund_Test1()
        {
            // Chuẩn bị dữ liệu DTO
            var dto = new FundForInsertDTO
            {               
                buildingId = "buildin",
                fund = 1,
                fundSource = "aaaa",
                description = "aa",

            };

            // Giả mạo thêm dữ liệu khi thêm gia trị mới
            _contextMock.Setup(context => context.Funds.Add(It.IsAny<Fund>()));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);

            // Thực thi hàm
            var result = _fundService.createFund(dto);

            // Kiểm tra kết quả
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }


        [TestMethod]
        public void createFund_Test2()
        {
            // Chuẩn bị dữ liệu DTO
            var dto = new FundForInsertDTO
            {
                buildingId = "",
                fund = 1,
                fundSource = "aaaa",
                description = "aa",

            };

            // Giả mạo thêm dữ liệu khi thêm gia trị mới
            _contextMock.Setup(context => context.Funds.Add(It.IsAny<Fund>()));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);

            // Thực thi hàm
            var result = _fundService.createFund(dto);

            // Kiểm tra kết quả
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [TestMethod]
        public void createFund_Test3()
        {
            // Chuẩn bị dữ liệu DTO
            var dto = new FundForInsertDTO
            {
                buildingId = "",
                fund = 1,
                fundSource = "",
                description = "",

            };

            // Giả mạo thêm dữ liệu khi thêm gia trị mới
            _contextMock.Setup(context => context.Funds.Add(It.IsAny<Fund>()));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);

            // Thực thi hàm
            var result = _fundService.createFund(dto);

            // Kiểm tra kết quả
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
        }


        [TestMethod]
        public void deleteFund_Test1()
        {
            // Tạo một dữ liệu mẫu để kiểm tra
            var fundId = "1";
            var fund = new Fund
            {
                Id = fundId,
                BuildingId = "ddd",
                Fund1 = 1,
                FundSource = "Test Post",
                Status = (int)Constants.STATUS.ACTIVE
            };
            _contextMock.Setup(m => m.Funds.Find(fundId)).Returns(fund);

            // Gọi phương thức để xóa 
            var result = _fundService.deleteFund(fundId);

            // Kiểm tra kết quả
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(fundId, result.Data);
        }

        [TestMethod]
        public void deleteFund_Test2()
        {
            string nullId = null;

            _contextMock.Setup(x => x.Hotlines.Find(nullId));

            // Act
            var result = _fundService.deleteFund(nullId);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);

        }

        [TestMethod]
        public void getAllFund_ReturnsFund()
        {
            var funds = new List<Fund>
            {
                 new Fund { Id = "1", BuildingId = "building_id_1", Fund1 = 1, FundSource = "aaa" },
                 new Fund { Id = "2", BuildingId = "building_id_1", Fund1 = 2, FundSource = "aaa" },
                 new Fund { Id = "3", BuildingId = "building_id_1", Fund1 = 4, FundSource = "aaa" },
                 
            };

            // Giả mạo Context để trả về dữ liệu mẫu
            _contextMock.Setup(context => context.Funds).Returns(DbSetMock.GetDbSetMock(funds).Object);

            // Tạo DTO cho việc tìm kiếm bài đăng
            var dto = new FundForSearchDTO { buildingId = "building_id_1" };

            // Thực thi phương thức getAllFund()
            var result = _fundService.getAllFund(dto);

            // Kiểm tra kết quả
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(3, result.Data.Count);

        }


        [TestMethod]
        public void getFundById_ExistingId_ReturnsPost()
        {
            // Tạo dữ liệu mẫu cho bài đăng
            var fund = new Fund
            {
                Id = "1",
                BuildingId = "initial_building_id",
                Fund1 = 1,
                FundSource = "ddd",
                Description = "Description",
                CreateUser = "user_id",
                CreateTime = DateTime.Now,
                Status = (int)Constants.STATUS.ACTIVE
            };

            // Giả mạo Context để trả về bài đăng mẫu khi được truy vấn
            _contextMock.Setup(context => context.Funds.Find(It.IsAny<string>())).Returns(fund);

            // Thực thi phương thức getFundById với một ID tồn tại
            var result = _fundService.getFundById("1");

            // Kiểm tra kết quả
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("1", result.Data.Id);
            Assert.AreEqual("initial_building_id", result.Data.BuildingId);
            // Kiểm tra các thuộc tính khác tương tự
        }



        [TestMethod]
        public void updateFund_Test1()
        {
            // Chuẩn bị dữ liệu DTO
            var dto = new FundForInsertDTO
            {
                buildingId = "buiding_id",
                fund = 1,
                fundSource = "aaaa",
                description = "aa",

            };

            // Giả mạo dữ liệu tồn tại
            var existingFund = new Fund
            {
                Id = "1",               
                BuildingId = "initial_building_id",
                Fund1= 1,
                FundSource ="ddd",
                Description= "Description",
            };

            // Giả mạo Context để trả về dữ liệu tồn tại khi được truy vấn
            _contextMock.Setup(context => context.Funds.Find(It.IsAny<string>())).Returns(existingFund);

            // Thực thi phương thức updateFund()
            var result = _fundService.updateFund("1", dto);

            // Kiểm tra kết quả
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        }

        [TestMethod]
        public void updateFund_Test2()
        {
            // Chuẩn bị dữ liệu DTO
            var dto = new FundForInsertDTO
            {
                buildingId = "",
                fund = 1,
                fundSource = "aaaa",
                description = "aa",

            };

            // Giả mạo dữ liệu tồn tại
            var existingFund = new Fund
            {
                Id = "1",
                BuildingId = "initial_building_id",
                Fund1 = 1,
                FundSource = "ddd",
                Description = "Description",
            };

            // Giả mạo Context để trả về dữ liệu tồn tại khi được truy vấn
            _contextMock.Setup(context => context.Funds.Find(It.IsAny<string>())).Returns(existingFund);

            // Thực thi phương thức updateFund()
            var result = _fundService.updateFund("1", dto);

            // Kiểm tra kết quả
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);

        }

        [TestMethod]
        public void updateFund_Test3()
        {
            // Chuẩn bị dữ liệu DTO
            var dto = new FundForInsertDTO
            {
                buildingId = "",
                fund = 1,
                fundSource = "",
                description = "",

            };

            // Giả mạo dữ liệu tồn tại
            var existingFund = new Fund
            {
                Id = "1",
                BuildingId = "initial_building_id",
                Fund1 = 1,
                FundSource = "ddd",
                Description = "Description",
            };

            // Giả mạo Context để trả về dữ liệu tồn tại khi được truy vấn
            _contextMock.Setup(context => context.Funds.Find(It.IsAny<string>())).Returns(existingFund);

            // Thực thi phương thức updateFund()
            var result = _fundService.updateFund("1", dto);

            // Kiểm tra kết quả
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);

        }
    }
}
