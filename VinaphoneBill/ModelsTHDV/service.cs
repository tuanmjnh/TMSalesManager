namespace VinaphoneBill.ModelsTHDV
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class service
    {
        public Guid id { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        public int? flag { get; set; }

        public string extra { get; set; }
    }
}
