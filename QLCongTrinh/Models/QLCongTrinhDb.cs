using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QLCongTrinh.Models
{
    public partial class QLCongTrinhDb : DbContext
    {
        public QLCongTrinhDb()
            : base("name=QLCongTrinh")
        {
        }

        public virtual DbSet<CongTrinh> CongTrinhs { get; set; }
        public virtual DbSet<NganSach> NganSaches { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<PhanCong> PhanCongs { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<TienDo> TienDoes { get; set; }
        public virtual DbSet<VatLieu> VatLieux { get; set; }
        public virtual DbSet<ViTri> ViTris { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CongTrinh>()
                .Property(e => e.SoDu)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CongTrinh>()
                .HasMany(e => e.NganSaches)
                .WithRequired(e => e.CongTrinh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CongTrinh>()
                .HasMany(e => e.PhanCongs)
                .WithRequired(e => e.CongTrinh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CongTrinh>()
                .HasMany(e => e.TienDoes)
                .WithRequired(e => e.CongTrinh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CongTrinh>()
                .HasMany(e => e.VatLieux)
                .WithRequired(e => e.CongTrinh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NganSach>()
                .Property(e => e.NganSachBD)
                .HasPrecision(19, 4);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.Luong)
                .HasPrecision(19, 4);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaVT)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.hinhanh)
                .IsFixedLength();

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.Mota)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.PhanCongs)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Quyen>()
                .HasMany(e => e.TaiKhoans)
                .WithRequired(e => e.Quyen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.TenTaiKhoan)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.mota)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.CongTrinhs)
                .WithRequired(e => e.TaiKhoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.NhanViens)
                .WithRequired(e => e.TaiKhoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TienDo>()
                .Property(e => e.ghichu)
                .IsUnicode(false);

            modelBuilder.Entity<VatLieu>()
                .Property(e => e.DonGia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<VatLieu>()
                .Property(e => e.ThanhTien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<VatLieu>()
                .Property(e => e.hinhanh)
                .IsFixedLength();

            modelBuilder.Entity<ViTri>()
                .Property(e => e.MaVT)
                .IsUnicode(false);

            modelBuilder.Entity<ViTri>()
                .HasMany(e => e.NhanViens)
                .WithRequired(e => e.ViTri)
                .WillCascadeOnDelete(false);
        }
    }
}
