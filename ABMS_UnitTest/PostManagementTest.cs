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


        [TestMethod]
        public void getAllPost_ReturnsPosts()
        {
            var posts = new List<Post>
            {
                 new Post { Id = "1", BuildingId = "building_id_1", Title = "Title 1", Type = 1 },
                 new Post { Id = "2", BuildingId = "building_id_2", Title = "Title 2", Type = 2 },
                 new Post { Id = "3", BuildingId = "building_id_1", Title = "Title 3", Type = 1 }
            };

            // Giả mạo Context để trả về dữ liệu mẫu
            _contextMock.Setup(context => context.Posts).Returns(DbSetMock.GetDbSetMock(posts).Object);

            // Tạo DTO cho việc tìm kiếm bài đăng
            var dto = new PostForSearchDTO { buildingId = "building_id_1" };

            // Thực thi phương thức getAllPost()
            var result = _postService.getAllPost(dto);

            // Kiểm tra kết quả
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(2, result.Data.Count); 


        }


        [TestMethod]
        public void getPostById_ExistingId_ReturnsPost()
        {
            // Tạo dữ liệu mẫu cho bài đăng
            var post = new Post
            {
                Id = "1",
                Title = "Sample Post",
                BuildingId = "building_id",
                Content = "Sample content",
                Image = "sample_image.jpg",
                Type = 1,
                CreateUser = "user_id",
                CreateTime = DateTime.Now,
                Status = (int)Constants.STATUS.ACTIVE
            };

            // Giả mạo Context để trả về bài đăng mẫu khi được truy vấn
            _contextMock.Setup(context => context.Posts.Find(It.IsAny<string>())).Returns(post);

            // Thực thi phương thức getPostById với một ID tồn tại
            var result = _postService.getPostById("1");

            // Kiểm tra kết quả
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("1", result.Data.Id);
            Assert.AreEqual("Sample Post", result.Data.Title);
            // Kiểm tra các thuộc tính khác tương tự
        }


        [TestMethod]
        public void updatePost_ValidPost_ReturnsSuccessResponse()
        {
            // Chuẩn bị dữ liệu DTO
            var dto = new PostForInsertDTO
            {
                title = "Updated Title",
                buildingId = "updated_building_id",
                content = "Updated content",
                image = "updated_image.jpg",
                type = 2
            };

            // Giả mạo dữ liệu bài đăng tồn tại
            var existingPost = new Post
            {
                Id = "1",
                Title = "Initial Title",
                BuildingId = "initial_building_id",
                Content = "Initial content",
                Image = "initial_image.jpg",
                Type = 1
            };

            // Giả mạo Context để trả về bài đăng tồn tại khi được truy vấn
            _contextMock.Setup(context => context.Posts.Find(It.IsAny<string>())).Returns(existingPost);

            // Thực thi phương thức updatePost()
            var result = _postService.updatePost("1", dto);

            // Kiểm tra kết quả
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("1", result.Data); // Kết quả trả về là ID của bài đăng đã được cập nhật thành công
        }


    }

    //DbSetMock là một lớp tĩnh được sử dụng để tạo ra đối tượng giả mạo (mock object) của DbSet trong unit test.
    public static class DbSetMock
    {
        // Phương thức này tạo ra một đối tượng giả mạo (mock object) của DbSet cho một kiểu dữ liệu cụ thể.
        // Nó nhận một IEnumerable<T> chứa dữ liệu mẫu cho DbSet.
        public static Mock<DbSet<T>> GetDbSetMock<T>(IEnumerable<T> data) where T : class
        {
            // Chuyển đổi IEnumerable<T> thành IQueryable<T> để có thể sử dụng trong mock object.
            var queryableData = data.AsQueryable();
            // Khởi tạo một đối tượng giả mạo (mock object) của DbSet với kiểu dữ liệu là T.
            var mockSet = new Mock<DbSet<T>>();

            // Cấu hình các thuộc tính của IQueryable trong DbSetMock để trả về dữ liệu mẫu.
            // Điều này cho phép chúng ta sử dụng các phương thức LINQ trên DbSetMock.
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());

            // Trả về đối tượng giả mạo của DbSet.
            return mockSet;
        }
    }


}
