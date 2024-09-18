using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Meal:BaseEntity,IEntity
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl {  get; set; }
        public int CategoryId {  get; set; }
        public Category? Category {  get; set; } 
        public ICollection<CartItem>? CartItems {  get; set; }
        public ICollection<OrderDetail>? OrderDetails {  get; set; }
        public ICollection<Review>? Reviews {  get; set; }
    }
}
