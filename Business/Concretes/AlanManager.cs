using AutoMapper;
using Business.Abstracts;
using Business.ValidationRules.Alan;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concretes;
using Entities.Dtos.Alan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class AlanManager : IAlanService
    {
        private readonly IAlanDal _alanDal;
        private readonly IMapper _mapper;

        public AlanManager(IAlanDal alanDal, IMapper mapper)
        {
            _alanDal = alanDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(AddAlanDtoValidator))]
        public async Task<IResult> Add(AddAlanDto addAlanDto)
        {
            var alan = _mapper.Map<Alan>(addAlanDto);
            await _alanDal.AddAsync(alan);
            return new SuccessResult();
        }

        public async Task<IResult> Delete(int id)
        {
            if(_alanDal.Get(x=> x.Id == id) == null) return new ErrorResult();

            await _alanDal.DeleteByIdAsync(id);
            return new SuccessResult();
        }

        public async Task<IDataResult<List<Alan>>> GetAll()
        {
            var result = await _alanDal.GetAllAsync();
            return new SuccessDataResult<List<Alan>>(result);
        }

        public async Task<IDataResult<Alan>> GetById(int id)
        {
            var result = await _alanDal.GetAsync(x=> x.Id == id);
            return new SuccessDataResult<Alan>(result);
        }

        [ValidationAspect(typeof(UpdateAlanDtoValidator))]
        public async Task<IResult> Update(UpdateAlanDto updateAlanDto)
        {
            var alan = _mapper.Map<Alan>(updateAlanDto);

            await _alanDal.UpdateAsync(alan);
            return new SuccessResult();
        }
    }
}
