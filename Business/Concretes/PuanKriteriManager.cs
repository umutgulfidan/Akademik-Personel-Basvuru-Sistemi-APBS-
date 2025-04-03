using AutoMapper;
using Business.Abstracts;
using Business.BusinessAspects;
using Business.Constants;
using Business.ValidationRules.PuanKriteri;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntitiyFramework;
using Entities.Concretes;
using Entities.Dtos.PuanKriteri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class PuanKriteriManager : IPuanKriteriService
    {
        private readonly IPuanKriteriDal _puanKriteriDal;
        private readonly IMapper _mapper;

        public PuanKriteriManager(IPuanKriteriDal puanKriteriDal, IMapper mapper)
        {
            _puanKriteriDal = puanKriteriDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(AddPuanKriteriDtoValidatior))]
        [SecuredOperation("Admin")]
        public async Task<IResult> Add(AddPuanKriteriDto kriterDto)
        {
            var kriter = _mapper.Map<PuanKriteri>(kriterDto);
            await _puanKriteriDal.AddAsync(kriter);
            return new SuccessResult(Messages.PuanKriteriAdded);
        }

        [SecuredOperation("Admin")]
        public async Task<IResult> Delete(int id)
        {
            if (await _puanKriteriDal.GetReadOnlyAsync(x => x.Id == id) == null) return new ErrorResult(Messages.PuanKriteriNotFound);

            await _puanKriteriDal.DeleteByIdAsync(id);
            return new SuccessResult(Messages.PuanKriteriDeleted);
        }

        public async Task<IDataResult<List<PuanKriteri>>> GetAll()
        {
            var result = await _puanKriteriDal.GetAllWithIncludesAsync();
            return new SuccessDataResult<List<PuanKriteri>>(result, Messages.AlanKriteriListed);
        }

        public async Task<IDataResult<PuanKriteri>> GetById(int id)
        {
            var result = await _puanKriteriDal.GetWithIncludesAsync(x => x.Id == id);
            return new SuccessDataResult<PuanKriteri>(result, Messages.AlanKriteriListed);
        }

        [ValidationAspect(typeof(UpdatePuanKriteriDtoValidatior))]
        [SecuredOperation("Admin")]
        public async Task<IResult> Update(UpdatePuanKriteriDto kriterDto)
        {
            var kriter = _mapper.Map<PuanKriteri>(kriterDto);
            if (await _puanKriteriDal.GetAsync(x => x.Id == kriterDto.Id) == null) return new ErrorResult(Messages.PuanKriteriNotFound);

            await _puanKriteriDal.UpdateAsync(kriter);
            return new SuccessResult(Messages.PuanKriteriUpdated);
        }
    }
}
