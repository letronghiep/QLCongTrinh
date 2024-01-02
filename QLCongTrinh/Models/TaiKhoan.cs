namespace QLCongTrinh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoan()
        {
            CongTrinhs = new HashSet<CongTrinh>();
            NhanViens = new HashSet<NhanVien>();
        }

        [Key]
        [DisplayName("Mã tài khoản")]
        public int MaTaiKhoan { get; set; }

        [Required(ErrorMessage = "Tên tài khoản không được để trống")]
        [StringLength(255)]
        [DisplayName("Tài khoản")]
        public string TenTaiKhoan { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(255)]
        [DataType(DataType.Password)]
        [DisplayName("Mật khẩu")]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Tên người dùng không được bỏ trống")]
        [StringLength(255)]
        [DisplayName("Tên người dùng")]
        public string TenNguoiDung { get; set; }
        [DisplayName("Quyền")]
        public int IdQuyen { get; set; }

        [StringLength(20)]
        [DisplayName("Hình ảnh")]
        public string ImageUrl { get; set; }

        [Column(TypeName = "text")]
        [DisplayName("Mô tả")]
        public string mota { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CongTrinh> CongTrinhs { get; set; }

        public virtual ICollection<NhanVien> NhanViens { get; set; }
        public virtual Quyen Quyen { get; set; }
    }
}
