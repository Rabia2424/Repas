using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class OrderDetail: BaseEntity, IEntity
    {
        public int Id { get; set; }     
        public int Quantity {  get; set; }
        public decimal Price { get; set; }  
        public int MealId {  get; set; }
        public Meal Meal { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}
