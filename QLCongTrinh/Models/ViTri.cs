namespace QLCongTrinh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ViTri")]
    public partial class ViTri
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ViTri()
        {
            ChiTietCTs = new HashSet<ChiTietCT>();
        }

        [Key]
        public int MaVT { get; set; }

        [Required]
        [StringLength(255)]
        public string TenVT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietCT> ChiTietCTs { get; set; }
    }
}
