using AutoMapper;
using Business.Abstracts;
using Business.BusinessAspects;
using Business.Constants;
using Business.ValidationRules.Ilan;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Extensions.Claims;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntitiyFramework;
using Entities.Concretes;
using Entities.Dtos;
using Entities.Dtos.Ilan;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Business.Concretes
{
    public class IlanManager : IIlanService
    {
        private readonly IIlanBasvuruService _ilanBasvuruService;
        private readonly IIlanDal _ilanDal;
        private readonly IAlanKriteriDal _alanKriteriDal;
        private readonly IMapper _mapper;
        private readonly ClaimsPrincipal _currentUser;

        public IlanManager(IIlanBasvuruService ilanBasvuruService, IIlanDal ilanDal, IAlanKriteriDal alanKriteriDal, IMapper mapper, ClaimsPrincipal currentUser)
        {
            _ilanBasvuruService = ilanBasvuruService;
            _ilanDal = ilanDal;
            _alanKriteriDal = alanKriteriDal;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        [SecuredOperation("Admin")]
        [CacheRemoveAspect("IIlanService.Get")]
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

        [SecuredOperation("Admin,Yonetici")]
        [ValidationAspect(typeof(AddIlanDtoValidator))]
        [CacheRemoveAspect("IIlanService.Get")]
        public async Task<Core.Utilities.Results.IResult> Add(AddIlanDto dto)
        {
            // JWT'den kullanıcı ID'sini alıyoruz
            var userId = _currentUser.ClaimUserId();
            var ilanToAdd = _mapper.Map<Ilan>(dto);
            ilanToAdd.OlusturanId = userId;
            ilanToAdd.CreatedDate = DateTime.UtcNow;

            await _ilanDal.AddAsync(ilanToAdd);
            return new SuccessResult(Messages.IlanAdded);
        }

        [SecuredOperation("Admin,Yonetici")]
        [CacheRemoveAspect("IIlanService.Get")]
        public async Task<Core.Utilities.Results.IResult> DeactivateIlan(int id)
        {
            var ilan = await _ilanDal.GetAsync(x => x.Id == id);
            if (ilan == null)
            {
                return new ErrorResult(Messages.IlanNotFound);
            }
            ilan.Status = false;
            await _ilanDal.UpdateAsync(ilan);
            return new SuccessResult(Messages.IlanDeactivate);
        }
        [SecuredOperation("Admin,Yonetici")]
        [CacheRemoveAspect("IIlanService.Get")]
        public async Task<Core.Utilities.Results.IResult> Delete(int id)
        {
            await _ilanDal.DeleteByIdAsync(id);
            return new SuccessResult(Messages.IlanDeleted);
        }

        [SecuredOperation("Admin,Yonetici")]
        public async Task<IDataResult<List<GetIlanDto>>> GetAll()
        {
            var results = await _ilanDal.GetAllWithBolumPozisyonOlusturan();
            var mappedResults = _mapper.Map<List<GetIlanDto>>(results);
            return new SuccessDataResult<List<GetIlanDto>>(mappedResults, Messages.IlanListed);
        }


        [CacheAspect(10)]
        public async Task<IDataResult<GetIlanDto>> GetById(int id)
        {
            var result = await _ilanDal.GetWithBolumPozisyon(X=> X.Id == id && X.Status == true);
            var mappedResult = _mapper.Map<GetIlanDto>(result);
            return new SuccessDataResult<GetIlanDto>(mappedResult,Messages.IlanListed);
        }

        public async Task<IDataResult<GetIlanDetailDto>> GetIlanDetail(int id)
        {
            var result = await _ilanDal.GetWithBolumPozisyon(x=> x.Id == id);
            if(result == null)
            {
                return new ErrorDataResult<GetIlanDetailDto>(Messages.IlanNotFound);
            }
            var mappedResult = _mapper.Map<GetIlanDetailDto>(result);
            mappedResult.AlanKriterleri = await _alanKriteriDal.GetAllWithIncludesAsync(x=> x.AlanId == result.Bolum.AlanId && x.PozisyonId == result.PozisyonId);
            return new SuccessDataResult<GetIlanDetailDto>(mappedResult,Messages.IlanListed);
        }

        [CacheAspect(5)]
        public async Task<IDataResult<List<GetIlanDto>>> GetIlansByQuery(UserIlanQueryDto queryDto)
        {
            var results = await _ilanDal.GetIlansByQueryAsync(queryDto);
            var mappedIlans = _mapper.Map<List<GetIlanDto>>(results);
            return new SuccessDataResult<List<GetIlanDto>>(mappedIlans, Messages.IlanListed);
        }

        [SecuredOperation("Admin,Yonetici")]
        [CacheAspect(5)]
        public async Task<IDataResult<List<GetIlanAdminDto>>> GetIlansByQueryForAdmin(AdminIlanQueryDto queryDto)
        {
            var results = await _ilanDal.GetIlansByQueryAsync(queryDto);
            var mappedIlans = _mapper.Map<List<GetIlanAdminDto>>(results);
            return new SuccessDataResult<List<GetIlanAdminDto>>(mappedIlans, Messages.IlanListed);
        }

        [SecuredOperation("Admin,Yonetici")]
        [ValidationAspect(typeof(UpdateIlanDtoValidator))]
        [CacheRemoveAspect("IIlanService.Get")]
        public async Task<Core.Utilities.Results.IResult> Update(UpdateIlanDto dto)
        {
            var ilanToUpdate = _mapper.Map<Ilan>(dto);
            // OlusturanId'yi manuel olarak set etmeden önce mevcut ilanı al
            var existingIlan = await _ilanDal.GetAsync(x=> x.Id == dto.Id);
            if (existingIlan != null)
            {
                ilanToUpdate.OlusturanId = existingIlan.OlusturanId; // mevcut OlusturanId'yi koru
            }
            ilanToUpdate.UpdatedDate = DateTime.UtcNow;
            await _ilanDal.UpdateAsync(ilanToUpdate);
            return new SuccessResult(Messages.IlanUpdated);

        }

        public async Task<IDataResult<List<GetIlanDto>>> GetAppliedIlanByUser(int userId)
        {
            var basvurular = await _ilanBasvuruService.GetByUser(userId);
            var basvurulanIlanIdListesi = basvurular.Data.Select(x=> x.IlanId).ToList();
            var ilanlar = _ilanDal.GetAllReadOnlyAsync(i => basvurulanIlanIdListesi.Contains(i.Id));
            var mappedIlans = _mapper.Map<List<GetIlanDto>>(ilanlar);
            return new SuccessDataResult<List<GetIlanDto>>(mappedIlans);
        }
    }
}
