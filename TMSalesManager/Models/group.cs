namespace TMSalesManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("group")]
    public partial class group
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public group()
        {
            group_item = new HashSet<group_item>();
        }

        public Guid id { get; set; }

        [StringLength(128)]
        public string app_key { get; set; }

        [StringLength(128)]
        public string id_key { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        public string parent_id { get; set; }

        public string parents_id { get; set; }

        public int? level { get; set; }

        [Column(TypeName = "ntext")]
        public string desc { get; set; }

        [StringLength(128)]
        public string created_by { get; set; }

        public DateTime? created_at { get; set; }

        [StringLength(128)]
        public string updated_by { get; set; }

        public DateTime? updated_at { get; set; }

        public int? flag { get; set; }

        public string extras { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<group_item> group_item { get; set; }
    }
}
