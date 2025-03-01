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
                var result = context.Set<UserOperationClaim>().Where(u=> u.UserId == user.Id).Include(c=> c.OperationClaim).Select(c=> c.OperationClaim).ToListAsync();
                return await result;
            }
        }
    }
}
