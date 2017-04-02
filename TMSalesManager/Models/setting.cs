namespace TMSalesManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class setting
    {
        public Guid id { get; set; }

        [Required]
        [StringLength(255)]
        public string module_key { get; set; }

        [Required]
        [StringLength(255)]
        public string sub_key { get; set; }

        public string value { get; set; }

        public string sub_value { get; set; }

        [Column(TypeName = "ntext")]
        public string desc { get; set; }

        public string extra { get; set; }
    }
}
