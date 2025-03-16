using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstracts
{
    public interface IUserOperationClaimDal : IEntityRepository<UserOperationClaim>
    {
        Task<List<UserOperationClaim>> GetAllUserOperationClaimsWithRolesAsync(Expression<Func<UserOperationClaim, bool>>? filter = null);
        Task<UserOperationClaim> GetUserOperationClaimWithRoleAsync(Expression<Func<UserOperationClaim, bool>> filter);

    }
}
