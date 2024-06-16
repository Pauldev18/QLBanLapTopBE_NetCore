using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QLBanHangBE.Models
{
    public partial class QLLapTopContext : DbContext
    {
        public QLLapTopContext()
        {
        }

        public QLLapTopContext(DbContextOptions<QLLapTopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; } = null!;
        public virtual DbSet<HoaDon> HoaDons { get; set; } = null!;
        public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;
        public virtual DbSet<SanPham> SanPhams { get; set; } = null!;
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=QLLapTop;User ID=sa;Password=Pauldev182@;Integrated Security=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietHoaDon>(entity =>
            {
                entity.HasKey(e => new { e.Mahd, e.Malaptop })
                    .HasName("PK__ChiTietH__264FB786246DBE7E");

                entity.ToTable("ChiTietHoaDon");

                entity.Property(e => e.Mahd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("mahd")
                    .IsFixedLength();

                entity.Property(e => e.Malaptop)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("malaptop")
                    .IsFixedLength();

                entity.Property(e => e.Dongia).HasColumnName("dongia");

                entity.Property(e => e.Soluong).HasColumnName("soluong");

                entity.Property(e => e.Thanhtien).HasColumnName("thanhtien");

                entity.HasOne(d => d.MahdNavigation)
                    .WithMany(p => p.ChiTietHoaDons)
                    .HasForeignKey(d => d.Mahd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietHoa__mahd__4316F928");

                entity.HasOne(d => d.MalaptopNavigation)
                    .WithMany(p => p.ChiTietHoaDons)
                    .HasForeignKey(d => d.Malaptop)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietHo__malap__440B1D61");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.Mahd)
                    .HasName("PK__HoaDon__7A2100DEAF47AA0A");

                entity.ToTable("HoaDon");

                entity.Property(e => e.Mahd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("mahd")
                    .IsFixedLength();

                entity.Property(e => e.Makh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("makh")
                    .IsFixedLength();

                entity.Property(e => e.Manv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("manv")
                    .IsFixedLength();

                entity.Property(e => e.Ngaylaphd)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaylaphd");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Thanhtien).HasColumnName("thanhtien");

                entity.HasOne(d => d.MakhNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.Makh)
                    .HasConstraintName("FK__HoaDon__makh__403A8C7D");

                entity.HasOne(d => d.ManvNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.Manv)
                    .HasConstraintName("FK__HoaDon__manv__3F466844");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.Makh)
                    .HasName("PK__KhachHan__7A21BB4CCF46D179");

                entity.ToTable("KhachHang");

                entity.Property(e => e.Makh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("makh")
                    .IsFixedLength();

                entity.Property(e => e.Diachi)
                    .HasMaxLength(30)
                    .HasColumnName("diachi");

                entity.Property(e => e.Dienthoai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("dienthoai")
                    .IsFixedLength();

                entity.Property(e => e.Hokh)
                    .HasMaxLength(30)
                    .HasColumnName("hokh");

                entity.Property(e => e.Ngaysinh)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaysinh");

                entity.Property(e => e.Phai)
                    .HasMaxLength(3)
                    .HasColumnName("phai");

                entity.Property(e => e.Tenkh)
                    .HasMaxLength(12)
                    .HasColumnName("tenkh");
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.Manv)
                    .HasName("PK__NhanVien__7A21B37D2ACD547E");

                entity.ToTable("NhanVien");

                entity.Property(e => e.Manv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("manv")
                    .IsFixedLength();

                entity.Property(e => e.Diachi)
                    .HasMaxLength(30)
                    .HasColumnName("diachi");

                entity.Property(e => e.Dienthoai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("dienthoai")
                    .IsFixedLength();

                entity.Property(e => e.Honv)
                    .HasMaxLength(30)
                    .HasColumnName("honv");

                entity.Property(e => e.Ngaysinh)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaysinh");

                entity.Property(e => e.Phai)
                    .HasMaxLength(3)
                    .HasColumnName("phai");

                entity.Property(e => e.Tennv)
                    .HasMaxLength(12)
                    .HasColumnName("tennv");
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.Malaptop)
                    .HasName("PK__SanPham__C6EB7584B75F73B2");

                entity.ToTable("SanPham");

                entity.Property(e => e.Malaptop)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("malaptop")
                    .IsFixedLength();

                entity.Property(e => e.Dongia).HasColumnName("dongia");

                entity.Property(e => e.Hinhanh)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("hinhanh");

                entity.Property(e => e.Soluong).HasColumnName("soluong");

                entity.Property(e => e.Tenhang)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("tenhang");

                entity.Property(e => e.Tenlaptop)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tenlaptop");
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasKey(e => e.Taikhoan1)
                    .HasName("PK__TaiKhoan__FE7B87314C6C32B0");

                entity.ToTable("TaiKhoan");

                entity.Property(e => e.Taikhoan1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("taikhoan")
                    .IsFixedLength();

                entity.Property(e => e.Matkhau)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("matkhau")
                    .IsFixedLength();

                entity.Property(e => e.Quyen).HasColumnName("quyen");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
