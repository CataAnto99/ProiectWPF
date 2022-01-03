using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;



namespace BookLotModel
{
    [Table("Coupon")]

    public partial class Coupon
    {
        public Coupon()
        {

            Coupons = new HashSet<Coupon>();
        }

        public int CouponId { get; set; }

        public int Code { get; set; }

        public string Text { get; set; }

        public int Value { get; set; }

        public int NumberOfUses { get; set; }


        public virtual ICollection<Coupon> Coupons { get; set; }

        public virtual Order Order { get; set; }

    }
}
    

