using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Address : BaseEntity, IEntity
    {
        public int Id { get; set; }    
        public string AddressLine1 {  get; set; }
        public string AddressLine2 {  get; set; }
        public string City {  get; set; }
        public string PostalCode {  get; set; }
        public string Country { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
