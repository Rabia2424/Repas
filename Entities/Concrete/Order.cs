using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Order: BaseEntity, IEntity
    {
        public int Id { get; set; } 
        public string OrderStatus {  get; set; }
        public decimal TotalAmount {  get; set; }
        public string PaymentMethod {  get; set; }
        public int CustomerId {  get; set; }
        public Customer Customer { get; set; }
        public int PaymentId {  get; set; }
        public Payment Payment { get; set; }
        public int? PromotionId { get; set; } // Nullable, kampanya uygulanmayabilir
        public Promotion Promotion { get; set; }
        public ICollection<OrderDetail> OrderDetails {  get; set; }
    }
}
