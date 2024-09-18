using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Promotion: BaseEntity, IEntity
    {
        public int Id { get; set; }     
        public string Code {  get; set; }
        public string Name { get; set; }
        public string Description {  get; set; }
        public decimal DiscountAmount {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
