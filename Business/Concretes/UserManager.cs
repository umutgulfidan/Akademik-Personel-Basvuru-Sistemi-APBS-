using Business.Abstract;
using Business.Dtos;
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

        public async Task<IResult> AddAsync(User user)
        {
            await _userDal.AddAsync(user);
            return new SuccessResult();
        }

        public async Task<IResult> DeleteAsync(User user)
        {
            await _userDal.DeleteAsync(user);
            return new SuccessResult();
        }

        public async Task<IResult> UpdateAsync(User user)
        {
            await _userDal.UpdateAsync(user);
            return new SuccessResult();
        }

        public async Task<IDataResult<User>> GetByIdAsync(int userId)
        {
            return new SuccessDataResult<User>(await _userDal.GetAsync(u => u.Id == userId));
        }

        public async Task<IDataResult<User>> GetAsync(Expression<Func<User, bool>> filter)
        {
            return new SuccessDataResult<User>(await _userDal.GetAsync(filter));
        }

        public async Task<IDataResult<List<User>>> GetAllAsync()
        {
            return new SuccessDataResult<List<User>>(await _userDal.GetAllAsync());
        }

        // TC Kimlik No ile kullanıcıyı al
        public async Task<IDataResult<User>> GetByNationalityIdAsync(string nationalityId)
        {
            return new SuccessDataResult<User>( await _userDal.GetAsync(u => u.NationalityId == nationalityId));
        }

        public async Task<List<OperationClaim>> GetClaimsAsync(User user)
        {
            // User'a ait claimleri döndürme işlemi
            return await _userDal.GetClaimsAsync(user);
        }

        public async Task<IDataResult<GetUserDto>> GetUserDto(int id)
        {
            var user = await _userDal.GetAsync(u => u.Id == id);
            var result = new GetUserDto()
            {
                Id = user.Id,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                NationalityId = user.NationalityId,
            };

            return new SuccessDataResult<GetUserDto>(result);

        }
    }
}
