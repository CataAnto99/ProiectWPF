using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLotModel
{
    [Table("Review")]
    public partial class Review
    {
        public Review()
        {
            Reviews = new HashSet<Review>();
        }
        public int ReviewId { get; set; }

        public int ClientId { get; set; }

        public int RatingClient { get; set; }

        public int BookId { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        public virtual Deposit Deposit { get; set; }
    }
    }

