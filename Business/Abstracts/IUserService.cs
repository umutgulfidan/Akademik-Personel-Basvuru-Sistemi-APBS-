using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task AddAsync(User user);
        Task DeleteAsync(User user);
        Task UpdateAsync(User user);
        Task<User> GetByIdAsync(int userId);
        Task<User> GetAsync(Expression<Func<User, bool>> filter);
        Task<List<User>> GetAllAsync();
        Task<User> GetByNationalityIdAsync(string nationalityId); // TC Kimlik Numarasına göre kullanıcı al
        Task<List<OperationClaim>> GetClaimsAsync(User user); // Kullanıcıya ait claim'leri al
    }
}
