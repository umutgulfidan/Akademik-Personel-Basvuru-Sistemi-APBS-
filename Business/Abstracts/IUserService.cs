using Business.Dtos;
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
        Task<IResult> AddAsync(User user);
        Task<IResult> DeleteAsync(User user);
        Task<IResult> UpdateAsync(User user);
        Task<IDataResult<User>> GetByIdAsync(int userId);
        Task<IDataResult<User>> GetAsync(Expression<Func<User, bool>> filter);
        Task<IDataResult<List<User>>> GetAllAsync();
        Task<IDataResult<User>> GetByNationalityIdAsync(string nationalityId); // TC Kimlik Numarasına göre kullanıcı al
        Task<List<OperationClaim>> GetClaimsAsync(User user); // Kullanıcıya ait claim'leri al

        //
        Task<IDataResult<GetUserDto>> GetUserDto(int id);
    }
}
