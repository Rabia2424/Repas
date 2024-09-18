using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Review : BaseEntity, IEntity
    {
        public int Id { get; set; } 
        public int Rating { get; set; } 
        public string Comment {  get; set; }
        public int CustomerId {  get; set; }
        public Customer Customer { get; set; }
        public int MealId { get; set; }
        public Meal Meal { get; set; }
    }
}
