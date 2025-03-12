using AutoMapper;
using Business.Abstracts;
using Business.ValidationRules.OperationClaim;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Dtos.OperationClaim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;
        private readonly IMapper _mapper;

        public OperationClaimManager(IOperationClaimDal operationClaimDal, IMapper mapper)
        {
            _operationClaimDal = operationClaimDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(AddOperationClaimDtoValidator))]
        public async Task<IResult> Add(AddOperationClaimDto operationClaimDto)
        {
            var data = _mapper.Map<OperationClaim>(operationClaimDto);
            await _operationClaimDal.AddAsync(data);
            return new SuccessResult();
        }

        public async Task<IResult> Delete(int id)
        {
            await _operationClaimDal.DeleteByIdAsync(id);
            return new SuccessResult();
        }

        public async Task<IDataResult<List<OperationClaim>>> GetAll()
        {
            var data = await _operationClaimDal.GetAllAsync();
            return new SuccessDataResult<List<OperationClaim>>(data);
        }

        public async Task<IDataResult<OperationClaim>> GetById(int id)
        {
            var data = await _operationClaimDal.GetAsync(x=> x.Id == id);
            return new SuccessDataResult<OperationClaim>(data);
        }
        [ValidationAspect(typeof(UpdateOperationClaimDtoValidator))]
        public async Task<IResult> Update(UpdateOperationClaimDto operationClaimDto)
        {
            var data = _mapper.Map<OperationClaim>(operationClaimDto);
            await _operationClaimDal.UpdateAsync(data);
            return new SuccessResult();
        }
    }
}
