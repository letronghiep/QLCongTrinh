namespace QLCongTrinh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            ChiTietCTs = new HashSet<ChiTietCT>();
            CongTrinhs = new HashSet<CongTrinh>();

        }

        [Key]
        public int MaNV { get; set; }
        [DisplayName("Tên nhân viên")]
        [Required(ErrorMessage = "Tên nhân viên không được để trống")]
        [StringLength(255)]
        public string TenNV { get; set; }

        [Column(TypeName = "money")]
        [DisplayName("Lương")]


        public decimal Luong { get; set; }

        [StringLength(50)]
        [DisplayName("Hình ảnh")]

        public string hinhanh { get; set; }
        [DisplayName("Tài khoản")]

        public int MaTaiKhoan { get; set; }

        [Column(TypeName = "text")]
        [DisplayName("Mô tả")]

        public string Mota { get; set; }
        [DisplayName("Số điện thoại")]

        public string Sdt { get; set; }
        [DisplayName("Xã")]

        public string Xa { get; set; }
        [DisplayName("Huyện")]

        public string Huyen { get; set; }
        [DisplayName("Tỉnh")]

        public string Tinh { get; set; }


        public virtual TaiKhoan TaiKhoan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietCT> ChiTietCTs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CongTrinh> CongTrinhs { get; set; }
    }
}
