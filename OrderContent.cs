using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLotModel
{
    [Table("OrderContent")]

    public partial class OrderContent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        /*public OrderContent() {

            OrdersContent = new HashSet<OrderContent>(); }*/

        public int OrderContentId { get; set; }

        public int BookId { get; set; }

        public int Quantity { get; set; }
        public virtual Order Order { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<OrderContent> OrdersContent { get; set; }

    }
}
