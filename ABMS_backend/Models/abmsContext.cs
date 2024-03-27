using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public virtual DbSet<AccountPost> AccountPosts { get; set; } = null!;
        public virtual DbSet<Building> Buildings { get; set; } = null!;
        public virtual DbSet<Construction> Constructions { get; set; } = null!;
        public virtual DbSet<Elevator> Elevators { get; set; } = null!;
        public virtual DbSet<Expense> Expenses { get; set; } = null!;
        public virtual DbSet<Fee> Fees { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Fund> Funds { get; set; } = null!;
        public virtual DbSet<Hotline> Hotlines { get; set; } = null!;
        public virtual DbSet<ParkingCard> ParkingCards { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Resident> Residents { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<RoomService> RoomServices { get; set; } = null!;
        public virtual DbSet<ServiceCharge> ServiceCharges { get; set; } = null!;
        public virtual DbSet<ServiceType> ServiceTypes { get; set; } = null!;
        public virtual DbSet<UtiliityDetail> UtiliityDetails { get; set; } = null!;
        public virtual DbSet<Utility> Utilities { get; set; } = null!;
        public virtual DbSet<UtilitySchedule> UtilitySchedules { get; set; } = null!;
        public virtual DbSet<Visitor> Visitors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=haidang.c344c4u4ocam.ap-southeast-2.rds.amazonaws.com;port=3306;database=abms;user=admin;password=Haidang4837", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.11.6-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.HasComment("Tài khoản")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.BuildingId, "account_building_FK");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(200)
                    .HasColumnName("avatar")
                    .HasComment("Avatar");

                entity.Property(e => e.BuildingId)
                    .HasMaxLength(100)
                    .HasColumnName("building_id")
                    .HasComment("Mã tòa nhà");

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
                    .HasMaxLength(64)
                    .HasColumnName("password_hash")
                    .HasComment("Mật khẩu hash");

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(64)
                    .HasColumnName("password_salt")
                    .HasComment("Mật khẩu salt");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(100)
                    .HasColumnName("phone_number")
                    .HasComment("Số điện thoại");

                entity.Property(e => e.Role)
                    .HasColumnType("int(11)")
                    .HasColumnName("role")
                    .HasComment("Vai trò: 0 admin, 1 cmb, 2 lễ tân, 3 room");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực");

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .HasColumnName("user_name")
                    .HasComment("Tên tài khoản");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("account_building_FK");
            });

            modelBuilder.Entity<AccountPost>(entity =>
            {
                entity.ToTable("account_post");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.AccountId, "account_post_account_FK");

                entity.HasIndex(e => e.PostId, "account_post_post_FK");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(100)
                    .HasColumnName("account_id")
                    .HasComment("Mã tài khoản");

                entity.Property(e => e.IsRead)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_read")
                    .HasComment("Đã đọc: true, false");

                entity.Property(e => e.PostId)
                    .HasMaxLength(100)
                    .HasColumnName("post_id")
                    .HasComment("Mã bài đăng");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountPosts)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("account_post_account_FK");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.AccountPosts)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("account_post_post_FK");
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.ToTable("building");

                entity.HasComment("Tòa nhà")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address")
                    .HasComment("Địa chỉ");

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

                entity.Property(e => e.NumberOfFloor)
                    .HasColumnType("int(11)")
                    .HasColumnName("number_of_floor")
                    .HasComment("Số tầng");

                entity.Property(e => e.RoomEachFloor)
                    .HasColumnType("int(11)")
                    .HasColumnName("room_each_floor")
                    .HasComment("Số căn mỗi tầng");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực");
            });

            modelBuilder.Entity<Construction>(entity =>
            {
                entity.ToTable("construction");

                entity.HasComment("Thi công")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.RoomId, "construction_room_FK");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.ApproveUser)
                    .HasMaxLength(100)
                    .HasColumnName("approve_user")
                    .HasComment("Người phê duyệt");

                entity.Property(e => e.ConstructionOrganization)
                    .HasMaxLength(100)
                    .HasColumnName("construction_organization")
                    .HasComment("Đơn vị thi công");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description")
                    .HasComment("Mô tả");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time")
                    .HasComment("Giờ kết thúc");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasComment("Tên thi công");

                entity.Property(e => e.PhoneContact)
                    .HasMaxLength(100)
                    .HasColumnName("phone_contact")
                    .HasComment("Số điện thoại liên hệ");

                entity.Property(e => e.Response)
                    .HasMaxLength(100)
                    .HasColumnName("response")
                    .HasComment("Lí do từ chối");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(100)
                    .HasColumnName("room_id")
                    .HasComment("Mã căn hộ");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time")
                    .HasComment("Giờ bắt đầu");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 2 đã gửi, 3 đã duyệt, 4 bị từ chối");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Constructions)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("construction_room_FK");
            });

            modelBuilder.Entity<Elevator>(entity =>
            {
                entity.ToTable("elevator");

                entity.HasComment("Thang chuyển đồ")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.RoomId, "elevator_room_FK");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.ApproveUser)
                    .HasMaxLength(100)
                    .HasColumnName("approve_user")
                    .HasComment("Người phê duyệt");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description")
                    .HasComment("Mô tả");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time")
                    .HasComment("Ngày, giờ kết thúc");

                entity.Property(e => e.Response)
                    .HasMaxLength(100)
                    .HasColumnName("response");

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
                    .HasComment("Trạng thái: 2 đã gửi, 3 đã duyệt, 4 bị từ chối");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Elevators)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("elevator_room_FK");
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.ToTable("expense");

                entity.HasComment("Chi")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.BuildingId, "expense_building_FK");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.BuildingId)
                    .HasMaxLength(100)
                    .HasColumnName("building_id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(100)
                    .HasColumnName("create_user")
                    .HasComment("Người tạo");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description")
                    .HasComment("Mô tả");

                entity.Property(e => e.Expense1)
                    .HasColumnName("expense")
                    .HasComment("Số tiền chi");

                entity.Property(e => e.ExpenseSource)
                    .HasMaxLength(100)
                    .HasColumnName("expense_source")
                    .HasComment("Nguồn chi");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_time")
                    .HasComment("Ngày chỉnh sửa");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(100)
                    .HasColumnName("modify_user")
                    .HasComment("Người chỉnh sửa");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("expense_building_FK");
            });

            modelBuilder.Entity<Fee>(entity =>
            {
                entity.ToTable("fee");

                entity.HasComment("Bảng giá dịch vụ")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.BuildingId, "fee_building_FK");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.BuildingId)
                    .HasMaxLength(100)
                    .HasColumnName("building_id")
                    .HasComment("Mã tòa nhà");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(100)
                    .HasColumnName("create_user")
                    .HasComment("Người tạo");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description")
                    .HasComment("Mô tả");

                entity.Property(e => e.EffectiveDate)
                    .HasColumnName("effective_date")
                    .HasComment("Ngày có hiệu lực");

                entity.Property(e => e.ExpireDate)
                    .HasColumnName("expire_date")
                    .HasComment("Ngày hết hiệu lực");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_time")
                    .HasComment("Ngày chỉnh sửa");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(100)
                    .HasColumnName("modify_user")
                    .HasComment("Người chỉnh sửa");

                entity.Property(e => e.Price)
                    .HasColumnType("int(11)")
                    .HasColumnName("price")
                    .HasComment("Giá");

                entity.Property(e => e.ServiceName)
                    .HasMaxLength(100)
                    .HasColumnName("service_name")
                    .HasComment("Tên dịch vụ");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực");

                entity.Property(e => e.Unit)
                    .HasMaxLength(100)
                    .HasColumnName("unit")
                    .HasComment("Đơn vị");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Fees)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fee_building_FK");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("feedback");

                entity.HasComment("phản hồi")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.RoomId, "feedback_room_FK");

                entity.HasIndex(e => e.ServiceTypeId, "feedback_service_type_FK");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.Content)
                    .HasMaxLength(100)
                    .HasColumnName("content")
                    .HasComment("Nội dung");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.Image)
                    .HasMaxLength(350)
                    .HasColumnName("image")
                    .HasComment("Đường dẫn ảnh");

                entity.Property(e => e.Response)
                    .HasMaxLength(100)
                    .HasColumnName("response");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(100)
                    .HasColumnName("room_id")
                    .HasComment("Mã căn hộ");

                entity.Property(e => e.ServiceTypeId)
                    .HasMaxLength(100)
                    .HasColumnName("service_type_id")
                    .HasComment("Mã loại dịch vụ");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title")
                    .HasComment("Tiêu đề");

                entity.Property(e => e.Response)
                  .HasMaxLength(100)
                  .HasColumnName("response");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("feedback_room_FK");

                entity.HasOne(d => d.ServiceType)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.ServiceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("feedback_service_type_FK");
            });

            modelBuilder.Entity<Fund>(entity =>
            {
                entity.ToTable("fund");

                entity.HasComment("Quỹ")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.BuildingId, "fund_building_FK");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.BuildingId)
                    .HasMaxLength(100)
                    .HasColumnName("building_id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(100)
                    .HasColumnName("create_user")
                    .HasComment("Người tạo");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description")
                    .HasComment("Mô tả");

                entity.Property(e => e.Fund1)
                    .HasColumnName("fund")
                    .HasComment("Số quỹ");

                entity.Property(e => e.FundSource)
                    .HasMaxLength(100)
                    .HasColumnName("fund_source")
                    .HasComment("Nguồn quỹ");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_time")
                    .HasComment("Ngày chỉnh sửa");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(100)
                    .HasColumnName("modify_user")
                    .HasComment("Người chỉnh sửa");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Funds)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fund_building_FK");
            });

            modelBuilder.Entity<Hotline>(entity =>
            {
                entity.ToTable("hotline");

                entity.HasComment("Đường dây nóng")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.BuildingId, "hotline_building_FK");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.BuildingId)
                    .HasMaxLength(100)
                    .HasColumnName("building_id");

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

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Hotlines)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("hotline_building_FK");
            });

            modelBuilder.Entity<ParkingCard>(entity =>
            {
                entity.ToTable("parking_card");

                entity.HasComment("Thẻ gửi xe")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.ResidentId, "parking_card_resident_FK");

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

                entity.Property(e => e.ExpireDate)
                    .HasColumnName("expire_date")
                    .HasComment("Ngày hết hạn");

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .HasColumnName("image")
                    .HasComment("Đường dẫn ảnh");

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

                entity.Property(e => e.Note)
                    .HasMaxLength(100)
                    .HasColumnName("note")
                    .HasComment("Ghi chú");

                entity.Property(e => e.ResidentId)
                    .HasMaxLength(100)
                    .HasColumnName("resident_id")
                    .HasComment("Mã cư dân");

                entity.Property(e => e.Response)
                    .HasMaxLength(100)
                    .HasColumnName("response")
                    .HasComment("Lí do từ chối");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực, 2 đã gửi, 6 chưa thanh toán");

                entity.Property(e => e.Type)
                    .HasColumnType("int(11)")
                    .HasColumnName("type")
                    .HasComment("Loại xe: 1 xe máy, 2 ô tô, 3 xe đạp(xe điện)");

                entity.HasOne(d => d.Resident)
                    .WithMany(p => p.ParkingCards)
                    .HasForeignKey(d => d.ResidentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("parking_card_resident_FK");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("post");

                entity.HasComment("Bài viết")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.BuildingId, "post_building_FK");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.BuildingId)
                    .HasMaxLength(100)
                    .HasColumnName("building_id")
                    .HasComment("Mã căn hộ");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
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

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực, 2 đã gửi, 4 bị từ chối");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title")
                    .HasComment("Tiêu đề");

                entity.Property(e => e.Type)
                    .HasColumnType("int(11)")
                    .HasColumnName("type")
                    .HasComment("Loại: 1 bài viết, 2 thông báo");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("post_building_FK");
            });

            modelBuilder.Entity<Resident>(entity =>
            {
                entity.ToTable("resident");

                entity.HasComment("Cư dân")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.RoomId, "resident_room_FK");

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

                entity.Property(e => e.IsHouseholder)
                    .HasColumnName("is_householder")
                    .HasComment("Chủ căn hộ: true, false");

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

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Residents)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("resident_room_FK");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("room");

                entity.HasComment("Căn hộ")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.AccountId, "room_account_FK");

                entity.HasIndex(e => e.BuildingId, "room_building_FK");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(100)
                    .HasColumnName("account_id")
                    .HasComment("Mã tài khoản");

                entity.Property(e => e.BuildingId)
                    .HasMaxLength(100)
                    .HasColumnName("building_id")
                    .HasComment("Mã tòa nhà");

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

                entity.Property(e => e.RoomArea)
                    .HasColumnName("room_area")
                    .HasComment("Diện tích căn hộ");

                entity.Property(e => e.RoomNumber)
                    .HasMaxLength(100)
                    .HasColumnName("room_number")
                    .HasComment("Số nhà");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("room_account_FK");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("room_building_FK");
            });

            modelBuilder.Entity<RoomService>(entity =>
            {
                entity.ToTable("room_service");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.FeeId, "room_service_fee_FK");

                entity.HasIndex(e => e.RoomId, "room_service_room_FK");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Amount)
                    .HasColumnType("int(11)")
                    .HasColumnName("amount")
                    .HasComment("Số lượng");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(100)
                    .HasColumnName("create_user")
                    .HasComment("Người tạo");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description")
                    .HasComment("Mô tả");

                entity.Property(e => e.FeeId)
                    .HasMaxLength(100)
                    .HasColumnName("fee_id")
                    .HasComment("Mã giá dịch vụ");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(100)
                    .HasColumnName("room_id")
                    .HasComment("Mã căn hộ");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực");

                entity.HasOne(d => d.Fee)
                    .WithMany(p => p.RoomServices)
                    .HasForeignKey(d => d.FeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("room_service_fee_FK");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomServices)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("room_service_room_FK");
            });

            modelBuilder.Entity<ServiceCharge>(entity =>
            {
                entity.ToTable("service_charge");

                entity.HasComment("Thanh toán dịch vụ")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.RoomId, "service_charge_room_FK");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chỉnh của bảng");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(100)
                    .HasColumnName("create_user")
                    .HasComment("Người tạo");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description")
                    .HasComment("Mô tả");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_time")
                    .HasComment("Ngày chỉnh sửa");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(100)
                    .HasColumnName("modify_user")
                    .HasComment("Người chỉnh sửa");

                entity.Property(e => e.Month)
                    .HasColumnType("int(11)")
                    .HasColumnName("month")
                    .HasComment("Tháng");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(100)
                    .HasColumnName("room_id")
                    .HasComment("Mã căn hộ");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 5 đã thanh toán, 6 chưa thanh toán");

                entity.Property(e => e.TotalPrice)
                    .HasColumnName("total_price")
                    .HasComment("Tổng số tiền");

                entity.Property(e => e.Year)
                    .HasColumnType("int(11)")
                    .HasColumnName("year")
                    .HasComment("Năm");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.ServiceCharges)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("service_charge_room_FK");
            });

            modelBuilder.Entity<ServiceType>(entity =>
            {
                entity.ToTable("service_type");

                entity.HasComment("Loại dịch vụ")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.BuildingId, "service_type_building_FK");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.BuildingId)
                    .HasMaxLength(100)
                    .HasColumnName("building_id");

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
                    .HasComment("Ngày chỉnh sửa");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(100)
                    .HasColumnName("modify_user")
                    .HasComment("Người chỉnh sửa");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasComment("Tên dịch vụ");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hiệu lực, 1 còn hiệu lực");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.ServiceTypes)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("service_type_building_FK");
            });

            modelBuilder.Entity<UtiliityDetail>(entity =>
            {
                entity.ToTable("utiliity_detail");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.UtilityId, "utiliity_detail_utility_FK");

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

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_time")
                    .HasComment("Ngày chỉnh sửa");

                entity.Property(e => e.ModifyUser)
                    .HasMaxLength(100)
                    .HasColumnName("modify_user")
                    .HasComment("Người chỉnh sửa");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasComment("Tên detail");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 0 hết hạn sử dụng, 1 còn hạn sử dụng");

                entity.Property(e => e.UtilityId)
                    .HasMaxLength(100)
                    .HasColumnName("utility_id")
                    .HasComment("Mã tiện ích");

                entity.HasOne(d => d.Utility)
                    .WithMany(p => p.UtiliityDetails)
                    .HasForeignKey(d => d.UtilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("utiliity_detail_utility_FK");
            });

            modelBuilder.Entity<Utility>(entity =>
            {
                entity.ToTable("utility");

                entity.HasComment("Tiện ích")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.BuildingId, "utility_building_FK");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

                entity.Property(e => e.BuildingId)
                    .HasMaxLength(100)
                    .HasColumnName("building_id");

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

                entity.Property(e => e.Location)
                    .HasMaxLength(100)
                    .HasColumnName("location")
                    .HasComment("Vị trí");

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
                    .HasColumnName("price_per_slot")
                    .HasComment("Giá tiện ích");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái sử dụng: 0 hết hiệu lực, 1 còn hiệu lực");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Utilities)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("utility_building_FK");
            });

            modelBuilder.Entity<UtilitySchedule>(entity =>
            {
                entity.ToTable("utility_schedule");

                entity.HasComment("Lịch tiện ích")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.RoomId, "utility_schedule_room_FK");

                entity.HasIndex(e => e.UtilityDetailId, "utility_schedule_utiliity_detail_FK");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("Khóa chính của bảng");

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

                entity.Property(e => e.Response)
                    .HasMaxLength(100)
                    .HasColumnName("response");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(100)
                    .HasColumnName("room_id")
                    .HasComment("Mã căn hộ");

                entity.Property(e => e.Slot)
                    .HasMaxLength(100)
                    .HasColumnName("slot")
                    .HasComment("Slot");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 2 đã gửi, 3 đã duyệt, 4 bị từ chối, 5 đã thanh toán");

                entity.Property(e => e.TotalPrice)
                    .HasColumnName("total_price")
                    .HasComment("Tổng số tiền");

                entity.Property(e => e.UtilityDetailId)
                    .HasMaxLength(100)
                    .HasColumnName("utility_detail_id")
                    .HasComment("Mã tiện ích");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.UtilitySchedules)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("utility_schedule_room_FK");

                entity.HasOne(d => d.UtilityDetail)
                    .WithMany(p => p.UtilitySchedules)
                    .HasForeignKey(d => d.UtilityDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("utility_schedule_utiliity_detail_FK");
            });

            modelBuilder.Entity<Visitor>(entity =>
            {
                entity.ToTable("visitor");

                entity.HasComment("Khách thăm")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.RoomId, "visitor_room_FK");

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
                    .HasMaxLength(300)
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

                entity.Property(e => e.Response)
                    .HasMaxLength(100)
                    .HasColumnName("response")
                    .HasComment("Lí do từ chối");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(100)
                    .HasColumnName("room_id")
                    .HasComment("Mã căn hộ");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status")
                    .HasComment("Trạng thái: 2 đã gửi, 3 đã duyệt, 4 bị từ chối");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Visitors)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("visitor_room_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
