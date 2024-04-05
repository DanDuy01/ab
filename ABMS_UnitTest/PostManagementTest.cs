using ABMS_backend.DTO.PostDTO;
using ABMS_backend.Models;
using ABMS_backend.Services;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Net;

namespace ABMS_UnitTest
{
    [TestClass]
    public class PostManagementTest
    {
        
        private PostManagementService _postService;
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
            _postService = new PostManagementService(_contextMock.Object, httpContextAccessorMock.Object);
        }


        [TestMethod]
        public void createPost_ValidPost_ReturnsSuccessResponse()
        {
            // Chuẩn bị dữ liệu DTO
            var dto = new PostForInsertDTO
            {
                title = "aa",
                buildingId = "building_id",
                content = "aaaa",
                image = "aa",
                type = 2
            };

            // Giả mạo thêm dữ liệu khi thêm bài đăng
            _contextMock.Setup(context => context.Posts.Add(It.IsAny<Post>()));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);

            // Thực thi hàm
            var result = _postService.createPost(dto);

            // Kiểm tra kết quả
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }


        [TestMethod]
        public void deletePost_ValidId_ReturnsSuccessResponse()
        {
            // Tạo một bài đăng mẫu để kiểm tra
            var postId = "1";
            var post = new Post
            {
                Id = postId,
                Title = "Test Post",
                Content = "Test Content",
                Status = (int)Constants.STATUS.ACTIVE
            };
            _contextMock.Setup(m => m.Posts.Find(postId)).Returns(post);

            // Gọi phương thức để xóa bài đăng
            var result = _postService.deletePost(postId);

            // Kiểm tra kết quả
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(postId, result.Data);
        }
      
    }
    


}
