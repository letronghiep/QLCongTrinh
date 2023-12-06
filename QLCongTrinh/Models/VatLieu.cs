namespace QLCongTrinh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VatLieu")]
    public partial class VatLieu
    {
        [Key]
        [Column(Order = 0)]
        public int MaVL { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaCT { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayCungCap { get; set; }

        public int? SoLuong { get; set; }

        [Column(TypeName = "money")]
        public decimal? DonGia { get; set; }

        [Column(TypeName = "money")]
        public decimal? ThanhTien { get; set; }

        [StringLength(10)]
        public string hinhanh { get; set; }

        public virtual CongTrinh CongTrinh { get; set; }
    }
}
