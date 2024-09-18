using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Payment: BaseEntity, IEntity
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount {  get; set; }
        public string PaymentMethod {  get; set; }
        public string PaymentStatus {  get; set; }
        public Order Order { get; set; }

    }
}
