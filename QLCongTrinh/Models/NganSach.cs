namespace QLCongTrinh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NganSach")]
    public partial class NganSach
    {
        [Key]
        [Column(Order = 0)]
        public int MaNganSach { get; set; }

        [Column(TypeName = "money")]
        public decimal NganSachBD { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaCT { get; set; }

        public bool TrangThai { get; set; }

        public virtual CongTrinh CongTrinh { get; set; }
    }
}
