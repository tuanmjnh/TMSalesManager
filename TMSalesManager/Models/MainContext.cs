namespace TMSalesManager.Models
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

        public virtual DbSet<bill> bills { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<group> groups { get; set; }
        public virtual DbSet<item> items { get; set; }
        public virtual DbSet<setting> settings { get; set; }
        public virtual DbSet<sub_bill> sub_bill { get; set; }
        public virtual DbSet<sub_item> sub_item { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<group_item> group_item { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<bill>()
                .Property(e => e.total_price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<bill>()
                .HasMany(e => e.sub_bill)
                .WithOptional(e => e.bill)
                .HasForeignKey(e => e.id_key);

            modelBuilder.Entity<group>()
                .HasMany(e => e.group_item)
                .WithOptional(e => e.group)
                .HasForeignKey(e => e.group_id);

            modelBuilder.Entity<item>()
                .Property(e => e.price_old)
                .HasPrecision(18, 0);

            modelBuilder.Entity<item>()
                .Property(e => e.price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<item>()
                .HasMany(e => e.group_item)
                .WithOptional(e => e.item)
                .HasForeignKey(e => e.item_id);

            modelBuilder.Entity<item>()
                .HasMany(e => e.sub_item)
                .WithOptional(e => e.item)
                .HasForeignKey(e => e.id_key);

            modelBuilder.Entity<sub_bill>()
                .Property(e => e.price_old)
                .HasPrecision(18, 0);

            modelBuilder.Entity<sub_bill>()
                .Property(e => e.price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<sub_bill>()
                .Property(e => e.total_price)
                .HasPrecision(18, 0);
        }
    }
}
