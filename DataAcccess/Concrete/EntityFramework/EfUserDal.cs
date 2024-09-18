﻿using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, RepasProjectContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using(var context = new RepasProjectContext())
            {
                var result = from o in context.OperationClaims
                             join op in context.UserOperationClaims
                             on o.Id equals op.OperationClaimId
                             where op.UserId == user.Id
                             select new OperationClaim
                             {
                                 Id = o.Id,
                                 Name = o.Name
                             };

                return result.ToList();
            }
        }
    }
}
