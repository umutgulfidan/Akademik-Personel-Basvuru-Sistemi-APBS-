using AutoMapper;
using Business.Abstracts;
using Business.BusinessAspects;
using Business.Constants;
using Business.ValidationRules.Ilan;
using Core.Aspects.Autofac.Validation;
using Core.Extensions.Claims;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntitiyFramework;
using Entities.Concretes;
using Entities.Dtos.Ilan;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Business.Concretes
{
    public class IlanManager : IIlanService
    {
        private readonly IIlanDal _ilanDal;
        private readonly IMapper _mapper;
        private readonly ClaimsPrincipal _currentUser;

        public IlanManager(IIlanDal ilanDal, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _ilanDal = ilanDal;
            _mapper = mapper;
            _currentUser = httpContextAccessor.HttpContext.User;
        }

        public async Task<Core.Utilities.Results.IResult> ActivateIlan(int id)
        {
            var ilan = await _ilanDal.GetAsync(x => x.Id == id);
            if (ilan == null)
            {
                return new ErrorResult(Messages.IlanNotFound);
            }
            ilan.Status = true;
            await _ilanDal.UpdateAsync(ilan);
            return new SuccessResult(Messages.IlanActivate);
        }

        [ValidationAspect(typeof(AddIlanDtoValidator))]
        public async Task<Core.Utilities.Results.IResult> Add(AddIlanDto dto)
        {
            // JWT'den kullanıcı ID'sini alıyoruz
            var userId = _currentUser.ClaimUserId();
            var ilanToAdd = _mapper.Map<Ilan>(dto);
            ilanToAdd.OlusturanId = userId;

            await _ilanDal.AddAsync(ilanToAdd);
            return new SuccessResult(Messages.IlanAdded);
        }

        public async Task<Core.Utilities.Results.IResult> DeactivateIlan(int id)
        {
            var ilan = await _ilanDal.GetAsync(x => x.Id == id);
            if (ilan == null)
            {
                return new ErrorResult(Messages.IlanNotFound);
            }
            ilan.Status = false;
            await _ilanDal.UpdateAsync(ilan);
            return new SuccessResult(Messages.IlanActivate);
        }

        public async Task<Core.Utilities.Results.IResult> Delete(int id)
        {
            await _ilanDal.DeleteByIdAsync(id);
            return new SuccessResult(Messages.IlanDeleted);
        }

        [SecuredOperation("Admin")]
        public async Task<IDataResult<List<Ilan>>> GetAll()
        {
            var results = await _ilanDal.GetAllAsync();
            return new SuccessDataResult<List<Ilan>>(results,Messages.IlanListed);
        }

        public async Task<IDataResult<List<Ilan>>> GetAllActiveIlans()
        {
            var results = await _ilanDal.GetAllAsync(x=> x.Status == true && x.BitisTarihi > DateTime.Now);
            return new SuccessDataResult<List<Ilan>>(results,Messages.IlanListed);
        }
        public async Task<IDataResult<List<Ilan>>> GetAllExpiredIlans()
        {
            var results = await _ilanDal.GetAllAsync(x => x.Status == true && x.BitisTarihi < DateTime.Now);
            return new SuccessDataResult<List<Ilan>>(results, Messages.IlanListed);
        }
        public async Task<IDataResult<Ilan>> GetById(int id)
        {
            var result = await _ilanDal.GetAsync(X=> X.Id == id);
            return new SuccessDataResult<Ilan>(result,Messages.IlanListed);
        }

        [ValidationAspect(typeof(UpdateIlanDtoValidator))]
        public async Task<Core.Utilities.Results.IResult> Update(UpdateIlanDto dto)
        {
            var ilanToUpdate = _mapper.Map<Ilan>(dto);
            await _ilanDal.UpdateAsync(ilanToUpdate);
            return new SuccessResult(Messages.IlanUpdated);

        }
    }
}
