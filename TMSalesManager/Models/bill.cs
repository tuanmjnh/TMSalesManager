namespace TMSalesManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("bill")]
    public partial class bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public bill()
        {
            sub_bill = new HashSet<sub_bill>();
        }

        public Guid id { get; set; }

        [StringLength(128)]
        public string code_key { get; set; }

        [StringLength(128)]
        public string customer_id { get; set; }

        public long? total_item { get; set; }

        public long? total_quantity { get; set; }

        public decimal? total_price { get; set; }

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
        public virtual ICollection<sub_bill> sub_bill { get; set; }
    }
}
