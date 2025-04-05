using AutoMapper;
using Business.Abstract;
using Business.Abstracts;
using Business.BusinessAspects;
using Business.Constants;
using Business.ValidationRules.User;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
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
        private readonly IFileService _fileService;
        public UserManager(IUserDal userDal, IMapper mapper,IFileService fileService)
        {
            _userDal = userDal;
            _mapper = mapper;
            _fileService = fileService;
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
            return new SuccessDataResult<User>(await _userDal.GetReadOnlyAsync(u => u.Id == userId),Messages.UserListed);
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
            if (user.ImageKey != null) {
                result.ImageUrl = await _fileService.GetPreSignedUrlAsync(user.ImageKey, 10);
            }


            return new SuccessDataResult<GetUserDto>(result,Messages.UserListed);

        }
        [SecuredOperation("Admin")]
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

        [SecuredOperation("Admin")]
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

        [SecuredOperation("Admin")]
        public async Task<IDataResult<List<GetUserDto>>> GetUsersByQuery(UserQueryDto query)
        {
            var users = await _userDal.GetUsersByQueryAsync(query);
            var mappedUsers = _mapper.Map<List<GetUserDto>>(users);
            return new SuccessDataResult<List<GetUserDto>>(mappedUsers,Messages.UserListed);
        }

        [ValidationAspect(typeof(UpdateUserInfoDtoValidator))]
        public async Task<IResult> UpdateProfileAsync(int userId, UpdateUserInfoDto updateUserDto)
        {
            var userToCheck = await GetAsync(x=> x.Id == userId);
            if (userToCheck.Data == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            if (userToCheck.Data.Status == false)
            {
                return new ErrorResult(Messages.UserPassiveAccount);
            }
            if (!HashingHelper.VerifyPasswordHash(updateUserDto.CurrentPassword, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorResult(Messages.UserPasswordError);
            }
            var checkEmail = await GetAsync(x => x.Email == updateUserDto.Email);
            if(checkEmail.Data != null && checkEmail.Data.Id != userToCheck.Data.Id)
            {
                return new ErrorResult(Messages.EmailAlreadyExists);
            }
            userToCheck.Data.Email = updateUserDto.Email;
            await this.UpdateAsync(userToCheck.Data);
            return new SuccessResult(Messages.UserUpdated);
        }
        [ValidationAspect(typeof(ChangePasswordDtoValidator))]
        public async Task<IResult> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto)
        {
            var userToCheck = await GetAsync(x => x.Id == userId);
            if (userToCheck.Data == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            if (userToCheck.Data.Status == false)
            {
                return new ErrorResult(Messages.UserPassiveAccount);
            }
            if (!HashingHelper.VerifyPasswordHash(changePasswordDto.CurrentPassword, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorResult(Messages.UserPasswordError);
            }

            byte[] passwordHash;
            byte[] passwordSalt;
            HashingHelper.CreatePasswordHash(changePasswordDto.NewPassword, out passwordHash, out passwordSalt);
            userToCheck.Data.PasswordHash = passwordHash;
            userToCheck.Data.PasswordSalt = passwordSalt;
            await this.UpdateAsync(userToCheck.Data);
            return new SuccessResult(Messages.UserUpdated);
        }

        public async Task<IResult> ChangeProfilePhoto(int userId,Microsoft.AspNetCore.Http.IFormFile file)
        {
            var userToCheck = await _userDal.GetAsync(x=> x.Id == userId);
            var imageKey =  userToCheck.ImageKey;

            if (imageKey == null)
            {
                imageKey = await _fileService.UploadFileAsync(file,Paths.AwsProfilePhotoFolder);
            }
            else
            {
                imageKey = await _fileService.UpdateFileAsync(file,imageKey,Paths.AwsProfilePhotoFolder);
            }

            userToCheck.ImageKey = imageKey;
            await _userDal.UpdateAsync(userToCheck);
            return new SuccessResult(Messages.UserProfilePhotoUpdated);
        }
    }
}
