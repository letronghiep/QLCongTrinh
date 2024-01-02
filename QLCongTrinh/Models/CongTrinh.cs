namespace QLCongTrinh.Models
{
    //using QLCongTrinh.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [Table("CongTrinh")]
    public partial class CongTrinh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CongTrinh()
        {
            ChiTietCTs = new HashSet<ChiTietCT>();
            VatLieux = new HashSet<VatLieu>();
        }

        [Key]
        [DisplayName("Mã công trình")]

        public int MaCT { get; set; }

        [Required(ErrorMessage = "Nhập tên công trình")]
        [DisplayName("Tên công trình")]
        public string TenCT { get; set; }
        [DisplayName("Tài khoản")]

        public int MaTaiKhoan { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Ngày bắt đầu")]

        public DateTime NgayBatDau { get; set; }

        [Column(TypeName = "date")]

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Ngày kết thúc")]

        public DateTime? NgayKetThuc { get; set; }

        [Column(TypeName = "text")]
        [DisplayName("Hình ảnh")]

        public string HinhAnh { get; set; }
        [DisplayName("Chủ thầu")]
        [Column(TypeName = "date")]

        public DateTime created_at { get; set; }
        public int? MaChuThau { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietCT> ChiTietCTs { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }

        public virtual NganSach NganSach { get; set; }

        public virtual TienDo TienDo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VatLieu> VatLieux { get; set; }
    }
}
