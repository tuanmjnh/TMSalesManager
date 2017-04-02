namespace TMSalesManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class customer
    {
        public Guid id { get; set; }

        [StringLength(150)]
        public string full_name { get; set; }

        [StringLength(50)]
        public string date_of_birth { get; set; }

        [StringLength(150)]
        public string mobile { get; set; }

        public string address { get; set; }

        [StringLength(255)]
        public string facebook { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        [StringLength(128)]
        public string card_id { get; set; }

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
    }
}
