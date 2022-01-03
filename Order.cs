namespace BookLotModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int OrderId { get; set; }

        public int? ClientId { get; set; }

        public int? CouponId { get; set; }

        public int? BookId { get; set; }

        public decimal? Total { get; set; }

        public int Quantity { get; set; }

        public virtual Client Client { get; set; }

        public virtual Deposit Deposit { get; set; }

        //public virtual OrderContent OrderContent { get; set; }

        //public virtual ICollection<OrderContent> OrdersContent { get; set; }

        public virtual ICollection<Coupon> Coupons { get; set; }
    }
}
