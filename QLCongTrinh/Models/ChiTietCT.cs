

namespace QLCongTrinh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietCT")]
    public partial class ChiTietCT
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Công trình")]
        public int MaCT { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Họ tên")]

        public int MaNV { get; set; }
        [DisplayName("Vị trí")]


        public int MaVT { get; set; }

        public virtual CongTrinh CongTrinh { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual ViTri ViTri { get; set; }
    }
}