using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Category: BaseEntity, IEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Meal>? Meals { get; set; }
    }
}
