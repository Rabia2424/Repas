using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class CartItem: BaseEntity, IEntity
    {
        public int Id { get; set; }
        public int Quantity {  get; set; }
        public decimal Price {  get; set; }
        public int CartId {  get; set; }
        public Cart Cart { get; set; }
        public int MealId {  get; set; }
        public Meal Meal { get; set; }
    }
}
