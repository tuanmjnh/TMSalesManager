namespace TMSalesManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sub_item
    {
        public Guid id { get; set; }

        [StringLength(128)]
        public string app_key { get; set; }

        public Guid? id_key { get; set; }

        public Guid? item_id { get; set; }

        [StringLength(255)]
        public string main_key { get; set; }

        [StringLength(255)]
        public string value { get; set; }

        [StringLength(255)]
        public string sub_value { get; set; }

        public string images { get; set; }

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

        public virtual item item { get; set; }
    }
}
