using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABMS_UnitTest.TestMocks
{
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
