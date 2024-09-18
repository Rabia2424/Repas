using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Customer:BaseEntity, IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public Cart Cart { get; set; }
        public ICollection<Order>? Orders { get; set; } = new List<Order>();
        public ICollection<Review>? Reviews { get; set; } = new List<Review>();
        public ICollection<Address>? Addresses { get; set; } = new List<Address>();
        public ICollection<UserOperationClaim>? UserOperationClaims { get; set; }
    }
}
