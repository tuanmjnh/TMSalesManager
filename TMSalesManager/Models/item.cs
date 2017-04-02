namespace TMSalesManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("item")]
    public partial class item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public item()
        {
            group_item = new HashSet<group_item>();
            sub_item = new HashSet<sub_item>();
        }

        public Guid id { get; set; }

        [StringLength(255)]
        public string app_key { get; set; }

        [StringLength(255)]
        public string id_key { get; set; }

        [StringLength(255)]
        public string code_key { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        [Column(TypeName = "ntext")]
        public string desc { get; set; }

        public long? quantity { get; set; }

        public long? quantity_total { get; set; }

        public decimal? price_old { get; set; }

        public decimal? price { get; set; }

        public string images { get; set; }

        [Column(TypeName = "image")]
        public byte[] image { get; set; }

        public DateTime? started_at { get; set; }

        public DateTime? ended_at { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sub_item> sub_item { get; set; }
    }
}
