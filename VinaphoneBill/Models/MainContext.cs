namespace VinaphoneBill.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MainContext : DbContext
    {
        public MainContext()
            : base("name=MainContext")
        {
        }

        public virtual DbSet<bill_month> bill_month { get; set; }
        public virtual DbSet<collected_staff> collected_staff { get; set; }
        public virtual DbSet<customer_info> customer_info { get; set; }
        public virtual DbSet<group> groups { get; set; }
        public virtual DbSet<group_item> group_item { get; set; }
        public virtual DbSet<item> items { get; set; }
        public virtual DbSet<local> locals { get; set; }
        public virtual DbSet<setting> settings { get; set; }
        public virtual DbSet<subscriber_growth> subscriber_growth { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<setting>()
                .Property(e => e.desc)
                .IsUnicode(false);
        }
    }
}
