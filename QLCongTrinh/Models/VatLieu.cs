namespace QLCongTrinh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VatLieu")]
    public partial class VatLieu
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int MaVL { get; set; }

        public int MaCT { get; set; }
        [DisplayName("Tên")]
        public string Ten { get; set; }


        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Ngày cung cấp")]
        public DateTime? NgayCungCap { get; set; }
        [DisplayName("Số lượng")]

        public int? SoLuong { get; set; }

        [Column(TypeName = "money")]
        [DisplayName("Đơn giá")]

        public decimal? DonGia { get; set; }
        [DisplayName("Thành tiền")]

        public decimal? ThanhTien
        {
            get
            {
                return DonGia * SoLuong;
            }
        }

        [StringLength(10)]
        [DisplayName("Hình ảnh")]

        public string hinhanh { get; set; }
        public virtual CongTrinh CongTrinh { get; set; }
    }
}
