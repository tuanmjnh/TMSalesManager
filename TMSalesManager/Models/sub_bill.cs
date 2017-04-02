namespace TMSalesManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sub_bill
    {
        public Guid id { get; set; }

        public Guid? id_key { get; set; }

        [StringLength(128)]
        public string code_key { get; set; }

        public Guid? item_id { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        public long? quantity { get; set; }

        public decimal? price_old { get; set; }

        public decimal? price { get; set; }

        public decimal? total_price { get; set; }

        public int? flag { get; set; }

        public string extras { get; set; }

        public virtual bill bill { get; set; }
    }
}
