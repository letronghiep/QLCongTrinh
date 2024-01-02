namespace QLCongTrinh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TienDo")]
    public partial class TienDo
    {
        [Key]
        [Column(Order = 0)]
        public int MaCT { get; set; }

        [DisplayName("Trạng thái")]
        public bool TrangThai { get; set; }

        [DisplayName("Ghi chú")]
        public string GhiChu { get; set; }

        public virtual CongTrinh CongTrinh { get; set; }
    }
}
