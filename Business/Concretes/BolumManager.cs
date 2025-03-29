using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.ValidationRules.Alan;
using Business.ValidationRules.Bolum;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntitiyFramework;
using Entities.Concretes;
using Entities.Dtos.Bolum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Concretes
{
    public class BolumManager : IBolumService
    {
        private readonly IMapper _mapper;
        private readonly IBolumDal _bolumDal;

        public BolumManager(IMapper mapper, IBolumDal bolumDal)
        {
            _mapper = mapper;
            _bolumDal = bolumDal;
        }

        [ValidationAspect(typeof(AddBolumDtoValidator))]
        [CacheRemoveAspect("IBolumService.Get")]
        public async Task<IResult> Add(AddBolumDto addBolumDto)
        {
            var bolum = _mapper.Map<Bolum>(addBolumDto);
            await _bolumDal.AddAsync(bolum);
            return new SuccessResult(Messages.BolumAdded);
        }
        [CacheRemoveAspect("IBolumService.Get")]
        public async Task<IResult> Delete(int id)
        {
            if (_bolumDal.Get(x => x.Id == id) == null) return new ErrorResult(Messages.BolumNotFound);

            await _bolumDal.DeleteByIdAsync(id);
            return new SuccessResult(Messages.BolumDeleted);
        }
        [CacheAspect(30)]
        public async Task<IDataResult<List<Bolum>>> GetAll()
        {
            var result = await _bolumDal.GetAllBolumsWithAlanAsync();
            return new SuccessDataResult<List<Bolum>>(result,Messages.BolumListed);
        }
        public async Task<IDataResult<Bolum>> GetById(int id)
        {
            var result = await _bolumDal.GetBolumWithAlanAsync(x => x.Id == id);
            return new SuccessDataResult<Bolum>(result, Messages.BolumListed);
        }
        [ValidationAspect(typeof(UpdateBolumDtoValidator))]
        [CacheRemoveAspect("IBolumService.Get")]
        public async Task<IResult> Update(UpdateBolumDto updateBolumDto)
        {
            var bolum = _mapper.Map<Bolum>(updateBolumDto);
            if (_bolumDal.Get(x => x.Id == bolum.Id) == null) return new ErrorResult(Messages.BolumNotFound);
            await _bolumDal.UpdateAsync(bolum);
            return new SuccessResult(Messages.BolumUpdated);
        }
    }
}
