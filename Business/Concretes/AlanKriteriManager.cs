using AutoMapper;
using Business.Abstracts;
using Business.BusinessAspects;
using Business.Constants;
using Business.ValidationRules.AlanKriteri;
using Business.ValidationRules.Kriter;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntitiyFramework;
using Entities.Concretes;
using Entities.Dtos.AlanKriteri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class AlanKriteriManager : IAlanKriteriService
    {
        private readonly IAlanKriteriDal _alanKriteriDal;
        private readonly IMapper _mapper;

        public AlanKriteriManager(IAlanKriteriDal alanKriteriDal, IMapper mapper)
        {
            _alanKriteriDal = alanKriteriDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(AddAlanKriteriDtoValidator))]
        [SecuredOperation("Admin")]
        public async Task<IResult> Add(AddAlanKriteriDto kriterDto)
        {
            var kriter = _mapper.Map<AlanKriteri>(kriterDto);
            await _alanKriteriDal.AddAsync(kriter);
            return new SuccessResult(Messages.AlanKriteriAdded);
        }
        [SecuredOperation("Admin")]
        public async Task<IResult> Delete(int id)
        {
            if (await _alanKriteriDal.GetReadOnlyAsync(x => x.Id == id) == null) return new ErrorResult(Messages.AlanKriteriNotFound);

            await _alanKriteriDal.DeleteByIdAsync(id);
            return new SuccessResult(Messages.AlanKriteriDeleted);
        }

        public async Task<IDataResult<List<AlanKriteri>>> GetAll()
        {
            var result = await _alanKriteriDal.GetAllWithIncludesAsync();
            return new SuccessDataResult<List<AlanKriteri>>(result, Messages.AlanKriteriListed);
        }

        public async Task<IDataResult<AlanKriteri>> GetById(int id)
        {
            var result = await _alanKriteriDal.GetWithIncludesAsync(x => x.Id == id);
            return new SuccessDataResult<AlanKriteri>(result, Messages.AlanKriteriListed);
        }

        [ValidationAspect(typeof(UpdateAlanKriteriDtoValidator))]
        [SecuredOperation("Admin")]
        public async Task<IResult> Update(UpdateAlanKriteriDto kriterDto)
        {
            var kriter = _mapper.Map<AlanKriteri>(kriterDto);
            if (await _alanKriteriDal.GetAsync(x => x.Id == kriterDto.Id) == null) return new ErrorResult(Messages.AlanKriteriNotFound);

            await _alanKriteriDal.UpdateAsync(kriter);
            return new SuccessResult(Messages.AlanKriteriUpdated);
        }
    }
}
