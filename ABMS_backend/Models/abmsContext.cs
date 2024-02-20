using Microsoft.EntityFrameworkCore;

namespace ABMS_backend.Models
{
    public partial class abmsContext : DbContext
    {
        public abmsContext()
        {
        }

        public abmsContext(DbContextOptions<abmsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Apartment> Apartments { get; set; } = null!;
        public virtual DbSet<CondominiumManagementBoard> CondominiumManagementBoards { get; set; } = null!;
        public virtual DbSet<Elevator> Elevators { get; set; } = null!;
        public virtual DbSet<Expense> Expenses { get; set; } = null!;
        public virtual DbSet<Fund> Funds { get; set; } = null!;
        public virtual DbSet<Hotline> Hotlines { get; set; } = null!;
        public virtual DbSet<ParkingCard> ParkingCards { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Receptionist> Receptionists { get; set; } = null!;
        public virtual DbSet<Resident> Residents { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Utility> Utilities { get; set; } = null!;
        public virtual DbSet<UtilitySchedule> UtilitySchedules { get; set; } = null!;
        public virtual DbSet<Visitor> Visitors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(config.GetConnectionString("value"), new MySqlServerVersion(new Version(11, 3, 2)));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.HasComment("Tài khoản");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.ApartmentId)
                    .HasMaxLength(100)
                    .HasColumnName("apartment_id")
                    .HasComment("Mã tòa nhà");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(200)
                    .HasColumnName("avatar")
                    .HasComment("Avatar");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(100)
                    .HasColumnName("create_user")
                    .HasComment("Người tạo");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email")
                    .HasComment("Email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .HasColumnName("full_name")
                    .HasComment("Họ và tên");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_time")
                    .HasComment("Ngày cập nhật");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(100)
                    .HasColumnName("modify_user")
                    .HasComment("Người cập nhật");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(100)
                    .HasColumnName("password_hash")
                    .HasComment("Mật khẩu hash");

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(100)
                    .HasColumnName("password_salt")
                    .HasComment("Mật khẩu salt");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(100)
                    .HasColumnName("phone_number")
                    .HasComment("Số điện thoại");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực");
            });

            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.ToTable("apartment");

                entity.HasComment("Tòa nhà");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address")
                    .HasComment("Địa chỉ");

                entity.Property(e => e.CmbId)
                    .HasMaxLength(100)
                    .HasColumnName("cmb_id")
                    .HasComment("Ban quản lý");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(100)
                    .HasColumnName("create_user")
                    .HasComment("Người tạo");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_time")
                    .HasComment("Ngày cập nhật");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(100)
                    .HasColumnName("modify_user")
                    .HasComment("Người cập nhật");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasComment("Tên tòa nhà");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực");
            });

            modelBuilder.Entity<CondominiumManagementBoard>(entity =>
            {
                entity.ToTable("condominium_management_board");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(100)
                    .HasColumnName("account_id")
                    .HasComment("Mã tài khoản");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực");
            });

            modelBuilder.Entity<Elevator>(entity =>
            {
                entity.ToTable("elevator");

                entity.HasComment("Thang máy");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.ActualTotalTime)
                    .HasColumnType("time")
                    .HasColumnName("actual_total_time")
                    .HasComment("Số giờ thực tế");

                entity.Property(e => e.ApproveUser)
                    .HasMaxLength(100)
                    .HasColumnName("approve_user")
                    .HasComment("Người phê duyệt");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description")
                    .HasComment("Mô tả");

                entity.Property(e => e.PlanTotalTime)
                    .HasColumnType("time")
                    .HasColumnName("plan_total_time")
                    .HasComment("Số giờ dự kiến");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(100)
                    .HasColumnName("room_id")
                    .HasComment("Mã căn hộ");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time")
                    .HasComment("Ngày, giờ bắt đầu");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 đã gửi, 1 đã duyệt, 2 bị từ chối");
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.ToTable("expense");

                entity.HasComment("Chi");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.Expense1)
                    .HasColumnName("expense")
                    .HasComment("Tổng chi");

                entity.Property(e => e.ExpenseSource)
                    .HasMaxLength(100)
                    .HasColumnName("expense_source")
                    .HasComment("Nguồn chi");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực");

                entity.Property(e => e.Step)
                    .HasColumnName("step")
                    .HasComment("Số tiền chi");
            });

            modelBuilder.Entity<Fund>(entity =>
            {
                entity.ToTable("fund");

                entity.HasComment("Quỹ");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.Fund1)
                    .HasColumnName("fund")
                    .HasComment("Tổng quỹ");

                entity.Property(e => e.FundSource)
                    .HasMaxLength(100)
                    .HasColumnName("fund_source")
                    .HasComment("Nguồn quỹ");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực");

                entity.Property(e => e.Step)
                    .HasColumnName("step")
                    .HasComment("Số tiền cộng thêm");
            });

            modelBuilder.Entity<Hotline>(entity =>
            {
                entity.ToTable("hotline");

                entity.HasComment("Đường dây nóng");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasComment("Tên");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(100)
                    .HasColumnName("phone_number")
                    .HasComment("Số điện thoại");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực");
            });

            modelBuilder.Entity<ParkingCard>(entity =>
            {
                entity.ToTable("parking_card");

                entity.HasComment("Thẻ gửi xe");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.Brand)
                    .HasMaxLength(100)
                    .HasColumnName("brand")
                    .HasComment("Nhãn hiệu");

                entity.Property(e => e.Color)
                    .HasMaxLength(100)
                    .HasColumnName("color")
                    .HasComment("Màu");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(100)
                    .HasColumnName("create_user")
                    .HasComment("Người tạo");

                entity.Property(e => e.LicensePlate)
                    .HasMaxLength(100)
                    .HasColumnName("license_plate")
                    .HasComment("Biển số xe");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_time")
                    .HasComment("Ngày cập nhật");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(100)
                    .HasColumnName("modify_user")
                    .HasComment("Người cập nhật");

                entity.Property(e => e.ResidentId)
                    .HasMaxLength(100)
                    .HasColumnName("resident_id")
                    .HasComment("Mã cư dân");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực, 2 chưa thanh toán");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("post");

                entity.HasComment("Bài viết");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.Content)
                    .HasMaxLength(200)
                    .HasColumnName("content")
                    .HasComment("Nội dung");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(100)
                    .HasColumnName("create_user")
                    .HasComment("Người tạo");

                entity.Property(e => e.Image)
                    .HasMaxLength(200)
                    .HasColumnName("image")
                    .HasComment("Ảnh đính kèm");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_time")
                    .HasComment("Ngày cập nhật");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(100)
                    .HasColumnName("modify_user")
                    .HasComment("Người cập nhật");

                entity.Property(e => e.ReceptionistId)
                    .HasMaxLength(100)
                    .HasColumnName("receptionist_id")
                    .HasComment("Mã lễ tân");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title")
                    .HasComment("Tiêu đề");
            });

            modelBuilder.Entity<Receptionist>(entity =>
            {
                entity.ToTable("receptionist");

                entity.HasComment("Lễ tân");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(100)
                    .HasColumnName("account_id")
                    .HasComment("Mã tài khoản");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(100)
                    .HasColumnName("create_user")
                    .HasComment("Người tạo");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_time")
                    .HasComment("Ngày cập nhật");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(100)
                    .HasColumnName("modify_user")
                    .HasComment("Người cập nhật");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực");
            });

            modelBuilder.Entity<Resident>(entity =>
            {
                entity.ToTable("resident");

                entity.HasComment("Cư dân");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(100)
                    .HasColumnName("create_user")
                    .HasComment("Người tạo");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("date_of_birth")
                    .HasComment("Ngày sinh");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .HasColumnName("full_name")
                    .HasComment("Họ và tên");

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasComment("Giới tính: true nam, false nữ");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_time")
                    .HasComment("Ngày cập nhật");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(100)
                    .HasColumnName("modify_user")
                    .HasComment("Người cập nhật");

                entity.Property(e => e.Phone)
                    .HasMaxLength(100)
                    .HasColumnName("phone")
                    .HasComment("Số điện thoại");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(100)
                    .HasColumnName("room_id")
                    .HasComment("Mã căn hộ");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("room");

                entity.HasComment("Căn hộ");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(100)
                    .HasColumnName("account_id")
                    .HasComment("Mã tài khoản");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(100)
                    .HasColumnName("create_user")
                    .HasComment("Người tạo");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_time")
                    .HasComment("Ngày cập nhật");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(100)
                    .HasColumnName("modify_user")
                    .HasComment("Người cập nhật");

                entity.Property(e => e.NumberOfResident)
                    .HasColumnType("int(11)")
                    .HasColumnName("number_of_resident")
                    .HasComment("Số thành viên");

                entity.Property(e => e.RoomNumber)
                    .HasMaxLength(100)
                    .HasColumnName("room_number")
                    .HasComment("Số nhà");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực");
            });

            modelBuilder.Entity<Utility>(entity =>
            {
                entity.ToTable("utility");

                entity.HasComment("Tiện ích");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.CloseTime)
                    .HasColumnType("time")
                    .HasColumnName("close_time")
                    .HasComment("Giờ đóng cửa");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasComment("Ngày đưa vào hệ thống");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(100)
                    .HasColumnName("create_user")
                    .HasComment("Người đưa vào hệ thống");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description")
                    .HasComment("Mô tả");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_time")
                    .HasComment("Ngày cập nhật");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(100)
                    .HasColumnName("modify_user")
                    .HasComment("Người cập nhật");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasComment("Tên tiện ích");

                entity.Property(e => e.NumberOfSlot)
                    .HasColumnType("int(11)")
                    .HasColumnName("number_of_slot")
                    .HasComment("Số slot trong 1 ngày");

                entity.Property(e => e.OpenTime)
                    .HasColumnType("time")
                    .HasColumnName("open_time")
                    .HasComment("Giờ mở cửa");

                entity.Property(e => e.PricePerSlot)
                    .HasMaxLength(100)
                    .HasColumnName("price_per_slot")
                    .HasComment("Giá tiện ích");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái sử dụng: 0 hết hiệu lực, 1 còn hiệu lực");
            });

            modelBuilder.Entity<UtilitySchedule>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("utility_schedule");

                entity.HasComment("Lịch tiện ích");

                entity.Property(e => e.ApproveUser)
                    .HasMaxLength(100)
                    .HasColumnName("approve_user")
                    .HasComment("Người phê duyệt");

                entity.Property(e => e.BookingDate)
                    .HasColumnName("booking_date")
                    .HasComment("Đặt ngày");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description")
                    .HasComment("Mô tả");

                entity.Property(e => e.NumberOfPerson)
                    .HasColumnType("int(11)")
                    .HasColumnName("number_of_person")
                    .HasComment("Số người tham gia");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(100)
                    .HasColumnName("room_id")
                    .HasComment("Mã căn hộ");

                entity.Property(e => e.Slot)
                    .HasColumnType("int(11)")
                    .HasColumnName("slot")
                    .HasComment("Slot");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 đã gửi, 1 đã duyệt, 2 bị từ chối, 3 đã thanh toán");

                entity.Property(e => e.UtilityId)
                    .HasMaxLength(100)
                    .HasColumnName("utility_id")
                    .HasComment("Mã tiện ích");
            });

            modelBuilder.Entity<Visitor>(entity =>
            {
                entity.ToTable("visitor");

                entity.HasComment("Khách thăm");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.ApproveUser)
                    .HasMaxLength(100)
                    .HasColumnName("approve_user")
                    .HasComment("Người phê duyệt");

                entity.Property(e => e.ArrivalTime)
                    .HasColumnType("datetime")
                    .HasColumnName("arrival_time")
                    .HasComment("Ngày, giờ đến");

                entity.Property(e => e.DepartureTime)
                    .HasColumnType("datetime")
                    .HasColumnName("departure_time")
                    .HasComment("Ngày, giờ đi");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description")
                    .HasComment("Mô tả");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .HasColumnName("full_name")
                    .HasComment("Họ và tên");

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasComment("Giới tính");

                entity.Property(e => e.IdentityCardImgUrl)
                    .HasMaxLength(100)
                    .HasColumnName("identity_card_img_url")
                    .HasComment("Ảnh cccd");

                entity.Property(e => e.IdentityNumber)
                    .HasMaxLength(100)
                    .HasColumnName("identity_number")
                    .HasComment("Số cccd");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(100)
                    .HasColumnName("phone_number")
                    .HasComment("Số điện thoại");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(100)
                    .HasColumnName("room_id")
                    .HasComment("Mã căn hộ");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 đã gửi, 1 đã duyệt, 2 bị từ chối");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
