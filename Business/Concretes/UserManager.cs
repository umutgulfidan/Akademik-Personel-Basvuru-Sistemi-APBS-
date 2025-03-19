using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntitiyFramework;
using Entities.Dtos;
using Entities.Dtos.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IMapper _mapper;
        public UserManager(IUserDal userDal, IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
        }

        public async Task<IResult> AddAsync(User user)
        {
            await _userDal.AddAsync(user);
            return new SuccessResult(Messages.UserAdded);
        }
        public async Task<IResult> DeleteAsync(int id)
        {
            await _userDal.DeleteByIdAsync(id);
            return new SuccessResult(Messages.UserDeleted);
        }
        public async Task<IResult> UpdateAsync(User user)
        {
            await _userDal.UpdateAsync(user);
            return new SuccessResult(Messages.UserUpdated);
        }

        public async Task<IDataResult<User>> GetByIdAsync(int userId)
        {
            return new SuccessDataResult<User>(await _userDal.GetAsync(u => u.Id == userId),Messages.UserListed);
        }

        public async Task<IDataResult<User>> GetAsync(Expression<Func<User, bool>> filter)
        {
            return new SuccessDataResult<User>(await _userDal.GetAsync(filter),Messages.UserListed);
        }

        public async Task<IDataResult<List<User>>> GetAllAsync()
        {
            return new SuccessDataResult<List<User>>(await _userDal.GetAllAsync(),Messages.UserListed);
        }

        // TC Kimlik No ile kullanıcıyı al
        public async Task<IDataResult<User>> GetByNationalityIdAsync(string nationalityId)
        {
            return new SuccessDataResult<User>(await _userDal.GetAsync(u => u.NationalityId == nationalityId), Messages.UserListed);
        }

        public async Task<List<OperationClaim>> GetClaimsAsync(User user)
        {
            // User'a ait claimleri döndürme işlemi
            return await _userDal.GetClaimsAsync(user);
        }

        public async Task<IDataResult<GetUserDto>> GetUserDto(int id)
        {
            var user = await _userDal.GetAsync(u => u.Id == id);
            var result = _mapper.Map<GetUserDto>(user);

            return new SuccessDataResult<GetUserDto>(result,Messages.UserListed);

        }

        public async Task<IResult> ActivateUserAsync(int userId)
        {
            var user = await _userDal.GetAsync(x => x.Id == userId);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            user.Status = true;
            await _userDal.UpdateAsync(user);
            return new SuccessResult(Messages.UserActivate);
        }

        public async Task<IResult> DeactivateUserAsync(int userId)
        {
            var user = await _userDal.GetAsync(x => x.Id == userId);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            user.Status = false;
            await _userDal.UpdateAsync(user);
            return new SuccessResult(Messages.UserDeactivate);
        }


        public async Task<IDataResult<List<GetUserDto>>> GetUsersByQuery(UserQueryDto query)
        {
            var users = await _userDal.GetUsersByQueryAsync(query);
            var mappedUsers = _mapper.Map<List<GetUserDto>>(users);
            return new SuccessDataResult<List<GetUserDto>>(mappedUsers,Messages.UserListed);
        }



    }
}
