//using Microsoft.EntityFrameworkCore;
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

        public virtual DbSet<ChiTietCT> ChiTietCTs { get; set; }
        public virtual DbSet<CongTrinh> CongTrinhs { get; set; }
        public virtual DbSet<NganSach> NganSaches { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<TienDo> TienDoes { get; set; }
        public virtual DbSet<VatLieu> VatLieux { get; set; }
        public virtual DbSet<ViTri> ViTris { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CongTrinh>()
               .Property(e => e.HinhAnh)
               .IsUnicode(false);

            modelBuilder.Entity<CongTrinh>()
                .HasMany(e => e.ChiTietCTs)
                .WithRequired(e => e.CongTrinh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CongTrinh>()
                .HasOptional(e => e.NganSach)
                .WithRequired(e => e.CongTrinh);

            modelBuilder.Entity<CongTrinh>()
                .HasOptional(e => e.TienDo)
                .WithRequired(e => e.CongTrinh);

            modelBuilder.Entity<CongTrinh>()
                .HasMany(e => e.VatLieux)
                .WithRequired(e => e.CongTrinh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NganSach>()
                .Property(e => e.NganSachBD)
                .HasPrecision(19, 2);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.Luong)
                .HasPrecision(19, 2);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.hinhanh)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.Mota)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.ChiTietCTs)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.CongTrinhs)
                .WithOptional(e => e.NhanVien)
                .HasForeignKey(e => e.MaChuThau);

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
                .Property(e => e.GhiChu)
                .IsUnicode(false);

            modelBuilder.Entity<VatLieu>()
                .Property(e => e.DonGia)
                .HasPrecision(19, 2);

            modelBuilder.Entity<VatLieu>()
                .Property(e => e.hinhanh)
                .IsFixedLength();

            modelBuilder.Entity<ViTri>()
                .HasMany(e => e.ChiTietCTs)
                .WithRequired(e => e.ViTri)
                .WillCascadeOnDelete(false);
        }
    }
    //public partial class QLCongTrinhDb : DbContext
    //{
    //    public QLCongTrinhDb()
    //        : base("name=QLCongTrinh")
    //    {
    //    }

    //    public virtual DbSet<CongTrinh> CongTrinhs { get; set; }
    //    public virtual DbSet<NhanVien> NhanViens { get; set; }
    //    public virtual DbSet<Quyen> Quyens { get; set; }
    //    public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    //    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
    //    public virtual DbSet<ChiTietCT> ChiTietCTs { get; set; }
    //    public virtual DbSet<VatLieu> VatLieux { get; set; }
    //    public virtual DbSet<ViTri> ViTris { get; set; }

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {

    //        modelBuilder.Entity<CongTrinh>()
    //           .HasOptional(e => e.NganSach)
    //           .WithRequired(e => e.CongTrinh);
    //        modelBuilder.Entity<CongTrinh>()
    //          .HasOptional(e => e.TienDo)
    //          .WithRequired(e => e.CongTrinh);


    //        modelBuilder.Entity<CongTrinh>()
    //            .HasOptional(e => e.TienDo)
    //            .WithRequired(e => e.CongTrinh)
    //            .WillCascadeOnDelete(false);

    //        modelBuilder.Entity<CongTrinh>()
    //            .HasMany(e => e.VatLieux)
    //            .WithRequired(e => e.CongTrinh)
    //        .WillCascadeOnDelete(false);

    //        modelBuilder.Entity<CongTrinh>()
    //            .Property(e => e.HinhAnh)
    //            .IsFixedLength();
    //        modelBuilder.Entity<CongTrinh>()
    //           .HasMany(e => e.ChiTietCTs)
    //           .WithOptional(e => e.CongTrinh)
    //            .WillCascadeOnDelete(false);

    //        modelBuilder.Entity<NhanVien>()
    //            .Property(e => e.Luong)
    //            .HasPrecision(19, 4);

    //        modelBuilder.Entity<NhanVien>()
    //            .HasMany(e => e.ChiTietCTs)
    //            .WithRequired(e => e.NhanVien)
    //            .WillCascadeOnDelete(false);

    //        modelBuilder.Entity<NhanVien>()
    //           .HasMany(e => e.CongTrinhs)
    //           .WithOptional(e => e.NhanVien)
    //           .HasForeignKey(e => e.MaChuThau);

    //        modelBuilder.Entity<NhanVien>()
    //            .Property(e => e.hinhanh)
    //            .IsFixedLength();

    //        modelBuilder.Entity<NhanVien>()
    //            .Property(e => e.Mota)
    //            .IsUnicode(false);


    //        modelBuilder.Entity<Quyen>()
    //            .HasMany(e => e.TaiKhoans)
    //            .WithRequired(e => e.Quyen)
    //            .WillCascadeOnDelete(false);

    //        modelBuilder.Entity<TaiKhoan>()
    //            .Property(e => e.TenTaiKhoan)
    //            .IsUnicode(false);

    //        modelBuilder.Entity<TaiKhoan>()
    //            .Property(e => e.MatKhau)
    //            .IsUnicode(false);

    //        modelBuilder.Entity<TaiKhoan>()
    //            .Property(e => e.ImageUrl)
    //            .IsUnicode(false);

    //        modelBuilder.Entity<TaiKhoan>()
    //            .Property(e => e.mota)
    //            .IsUnicode(false);

    //        modelBuilder.Entity<TaiKhoan>()
    //            .HasMany(e => e.CongTrinhs)
    //            .WithRequired(e => e.TaiKhoan)
    //            .WillCascadeOnDelete(false);

    //        modelBuilder.Entity<TaiKhoan>()
    //            .HasMany(e => e.NhanViens)
    //            .WithRequired(e => e.TaiKhoan)
    //            .WillCascadeOnDelete(false);

    //        modelBuilder.Entity<TienDo>()
    //            .Property(e => e.GhiChu)
    //            .IsUnicode(false);

    //        modelBuilder.Entity<VatLieu>()
    //            .Property(e => e.DonGia)
    //            .HasPrecision(19, 4);

    //        //modelBuilder.Entity<VatLieu>()
    //        //    .Property(e => e.ThanhTien)
    //        //    .HasPrecision(19, 4);

    //        modelBuilder.Entity<VatLieu>()
    //            .Property(e => e.hinhanh)
    //            .IsFixedLength();


    //    }

    //    public System.Data.Entity.DbSet<NganSach> NganSaches { get; set; }

    //    public System.Data.Entity.DbSet<QLCongTrinh.Models.TienDo> TienDoes { get; set; }
    //}
}
