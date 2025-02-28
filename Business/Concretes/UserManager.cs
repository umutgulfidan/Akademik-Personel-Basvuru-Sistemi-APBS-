using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task AddAsync(User user)
        {
            await _userDal.AddAsync(user);
        }

        public async Task DeleteAsync(User user)
        {
            await _userDal.DeleteAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            await _userDal.UpdateAsync(user);
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await _userDal.GetAsync(u => u.Id == userId);
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> filter)
        {
            return await _userDal.GetAsync(filter);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userDal.GetAllAsync();
        }

        // TC Kimlik No ile kullanıcıyı al
        public async Task<User> GetByNationalityIdAsync(string nationalityId)
        {
            return await _userDal.GetAsync(u => u.NationalityId == nationalityId);
        }

        public async Task<List<OperationClaim>> GetClaimsAsync(User user)
        {
            // User'a ait claimleri döndürme işlemi
            return await _userDal.GetClaimsAsync(user);
        }
    }
}
