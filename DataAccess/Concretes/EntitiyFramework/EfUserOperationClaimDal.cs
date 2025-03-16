using Core.DataAccess.EntityFramework;
using Core.Entities;
using Core.Entities.Concrete;
using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EntitiyFramework
{
    public class EfUserOperationClaimDal : EfRepositoryBase<Context, UserOperationClaim>, IUserOperationClaimDal
    {
        public async Task<List<UserOperationClaim>> GetAllUserOperationClaimsWithRolesAsync(Expression<Func<UserOperationClaim, bool>>? filter = null)
        {
            using var context = new Context();
            return filter == null ? await context.UserOperationClaims.Include(x=> x.OperationClaim).ToListAsync() : await context.UserOperationClaims.Where(filter).Include(x=>x.OperationClaim).ToListAsync();
        }
        public async Task<UserOperationClaim> GetUserOperationClaimWithRoleAsync(Expression<Func<UserOperationClaim, bool>> filter)
        {
            using var context = new Context();
            var data = await context.UserOperationClaims.Include(x=> x.OperationClaim).FirstOrDefaultAsync(filter);
            return data;
        }

    }
}
