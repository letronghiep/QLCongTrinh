namespace QLCongTrinh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NganSach")]
    public partial class NganSach
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaCT { get; set; }

        [Column(TypeName = "money")]
        [DisplayName("Ngân sách ban đầu")]
        public decimal NganSachBD { get; set; }

        public virtual CongTrinh CongTrinh { get; set; }
    }

}