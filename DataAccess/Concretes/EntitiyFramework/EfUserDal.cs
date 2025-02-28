using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EntitiyFramework
{
    public class EfUserDal : EfRepositoryBase<Context,User>,IUserDal
    {
        public async Task<List<OperationClaim>> GetClaimsAsync(User user)
        {
            using (var context = new Context())
            {
                var result = from userClaim in context.UserOperationClaims
                             join claim in context.OperationClaims
                             on userClaim.OperationClaimId equals claim.Id
                             where userClaim.UserId == user.Id
                             select claim;

                return await result.ToListAsync();
            }
        }
    }
}
