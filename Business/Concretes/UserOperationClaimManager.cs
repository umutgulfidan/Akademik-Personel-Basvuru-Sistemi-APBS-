using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.ValidationRules.UserOperationClaim;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Dtos.UserOperationClaim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private readonly IMapper _mapper;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal, IMapper mapper)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _mapper = mapper;
        }
        [ValidationAspect(typeof(AddUserOperationClaimDtoValidator))]
        public async Task<IResult> Add(AddUserOperationClaimDto addDto)
        {
            var data = _mapper.Map<UserOperationClaim>(addDto);
            await _userOperationClaimDal.AddAsync(data);
            return new SuccessResult(Messages.UserOperationClaimAdded);
        }

        public async Task<IResult> Delete(int id)
        {
            await _userOperationClaimDal.DeleteByIdAsync(id);
            return new SuccessResult(Messages.UserOperationClaimDeleted);
        }

        public async Task<IDataResult<List<GetUserOperationClaimDto>>> GetAll()
        {
            var data = await _userOperationClaimDal.GetAllUserOperationClaimsWithRolesAsync();
            var mappedData = _mapper.Map<List<GetUserOperationClaimDto>>(data);
            return new SuccessDataResult<List<GetUserOperationClaimDto>>(mappedData,Messages.UserOperationClaimListed);
        }

        public async Task<IDataResult<GetUserOperationClaimDto>> GetById(int id)
        {
            var data = await _userOperationClaimDal.GetUserOperationClaimWithRoleAsync(x=> x.Id == id);
            var mappedData = _mapper.Map<GetUserOperationClaimDto>(data);
            return new SuccessDataResult<GetUserOperationClaimDto>(mappedData,Messages.UserOperationClaimListed);
        }

        public async Task<IDataResult<List<GetUserOperationClaimDto>>> GetByUser(int userId)
        {
            var data = await _userOperationClaimDal.GetAllUserOperationClaimsWithRolesAsync(x=> x.UserId == userId);
            var mappedData = _mapper.Map<List<GetUserOperationClaimDto>>(data);
            return new SuccessDataResult<List<GetUserOperationClaimDto>>(mappedData,Messages.UserOperationClaimListed);
        }

        [ValidationAspect(typeof(UpdateUserOperationClaimDtoValidator))]
        public async Task<IResult> Update(UpdateUserOperationClaimDto updateDto)
        {
            var data = _mapper.Map<UserOperationClaim>(updateDto);
            await _userOperationClaimDal.UpdateAsync(data,Messages.UserOperationClaimUpdated);
            return new SuccessResult();
        }
    }
}
