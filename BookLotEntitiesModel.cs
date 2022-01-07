using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BookLotModel
{
    public partial class BookLotEntitiesModel : DbContext
    {
        public BookLotEntitiesModel()
            : base("name=BookLotEntitiesModel")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Deposit> Deposits { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderContent> OrdersContent { get; set; }
        
        public virtual DbSet<Coupon> Coupon { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Client)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Deposit>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Deposit)
                .WillCascadeOnDelete();

            
            modelBuilder.Entity<Deposit>()
                .HasMany(e => e.Reviews)
                .WithOptional(e => e.Deposit)
                .WillCascadeOnDelete();
            


            modelBuilder.Entity<Order>()
                .HasMany(e => e.Coupons)
                .WithOptional(e => e.Order)
                .WillCascadeOnDelete();

        }
    }
}
