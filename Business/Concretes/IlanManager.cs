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

        [SecuredOperation("Admin")]
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

        [SecuredOperation("Admin")]
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
        [SecuredOperation("Admin")]
        public async Task<Core.Utilities.Results.IResult> Delete(int id)
        {
            await _ilanDal.DeleteByIdAsync(id);
            return new SuccessResult(Messages.IlanDeleted);
        }

        [SecuredOperation("Admin")]
        public async Task<IDataResult<List<GetIlanDto>>> GetAll()
        {
            var results = await _ilanDal.GetAllWithBolumAndPozisyon();
            var mappedResults = _mapper.Map<List<GetIlanDto>>(results);
            return new SuccessDataResult<List<GetIlanDto>>(mappedResults, Messages.IlanListed);
        }

        public async Task<IDataResult<List<GetIlanDto>>> GetAllActiveIlans()
        {
            var results = await _ilanDal.GetAllWithBolumAndPozisyon(x=> x.Status == true && x.BitisTarihi > DateTime.Now);
            var mappedResults = _mapper.Map<List<GetIlanDto>>(results);

            return new SuccessDataResult<List<GetIlanDto>>(mappedResults, Messages.IlanListed);
        }
        public async Task<IDataResult<List<GetIlanDto>>> GetAllExpiredIlans()
        {
            var results = await _ilanDal.GetAllWithBolumAndPozisyon(x => x.Status == true && x.BitisTarihi < DateTime.Now);
            var mappedResults = _mapper.Map<List<GetIlanDto>>(results);
            return new SuccessDataResult<List<GetIlanDto>>(mappedResults, Messages.IlanListed);
        }

        public async Task<IDataResult<GetIlanDto>> GetById(int id)
        {
            var result = await _ilanDal.GetWithBolumAndPozisyon(X=> X.Id == id && X.Status == true);
            var mappedResult = _mapper.Map<GetIlanDto>(result);
            return new SuccessDataResult<GetIlanDto>(mappedResult,Messages.IlanListed);
        }

        public async Task<IDataResult<List<GetIlanDto>>> GetIlansByQuery(UserIlanQueryDto queryDto)
        {
            var results = await _ilanDal.GetIlansByQueryAsync(queryDto);
            var mappedIlans = _mapper.Map<List<GetIlanDto>>(results);
            return new SuccessDataResult<List<GetIlanDto>>(mappedIlans, Messages.UserListed);
        }

        public async Task<IDataResult<List<GetIlanDto>>> GetIlansByQuery(AdminIlanQueryDto queryDto)
        {
            var results = await _ilanDal.GetIlansByQueryAsync(queryDto);
            var mappedIlans = _mapper.Map<List<GetIlanDto>>(results);
            return new SuccessDataResult<List<GetIlanDto>>(mappedIlans, Messages.UserListed);
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(UpdateIlanDtoValidator))]
        public async Task<Core.Utilities.Results.IResult> Update(UpdateIlanDto dto)
        {
            var ilanToUpdate = _mapper.Map<Ilan>(dto);
            // OlusturanId'yi manuel olarak set etmeden önce mevcut ilanı al
            var existingIlan = await _ilanDal.GetAsync(x=> x.Id == dto.Id);
            if (existingIlan != null)
            {
                ilanToUpdate.OlusturanId = existingIlan.OlusturanId; // mevcut OlusturanId'yi koru
            }
            await _ilanDal.UpdateAsync(ilanToUpdate);
            return new SuccessResult(Messages.IlanUpdated);

        }
    }
}
