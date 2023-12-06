namespace QLCongTrinh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            PhanCongs = new HashSet<PhanCong>();
        }

        [Key]
        public int MaNV { get; set; }

        [Required]
        [StringLength(255)]
        public string TenNV { get; set; }

        [Column(TypeName = "money")]
        public decimal Luong { get; set; }

        [Required]
        [StringLength(255)]
        public string MaVT { get; set; }

        [StringLength(10)]
        public string hinhanh { get; set; }

        public int MaTaiKhoan { get; set; }

        [Column(TypeName = "text")]
        public string Mota { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }

        public virtual ViTri ViTri { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhanCong> PhanCongs { get; set; }
    }
}
