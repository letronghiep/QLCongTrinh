namespace QLCongTrinh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TienDo")]
    public partial class TienDo
    {
        [Key]
        [Column(Order = 0)]
        public int MaCT { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaTienDo { get; set; }

        public byte TinhTrang { get; set; }

        [Column(TypeName = "text")]
        public string ghichu { get; set; }

        public virtual CongTrinh CongTrinh { get; set; }
    }
}
