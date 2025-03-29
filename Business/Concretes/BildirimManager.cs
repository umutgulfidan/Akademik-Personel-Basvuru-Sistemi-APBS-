using AutoMapper;
using Business.Abstracts;
using Business.BusinessAspects;
using Business.Constants;
using Business.ValidationRules.Bildirim;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Dtos.Bildirim;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class BildirimManager : IBildirimService
    {
        private readonly IBildirimDal _bildirimDal;
        private readonly IMapper _mapper;
        public BildirimManager(IBildirimDal bildirimDal, IMapper mapper)
        {
            _bildirimDal = bildirimDal;
            _mapper = mapper;
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(AddBildirimDtoValidator))]
        public async Task<IResult> AddAdmin(AddBildirimDto bildirim)
        {
            var mappedBildirim = _mapper.Map<Bildirim>(bildirim);
            await _bildirimDal.AddAsync(mappedBildirim);
            return new SuccessResult(Messages.BildirimAdded);
        }
        [SecuredOperation("Admin")]
        public async Task<IResult> DeleteAdmin(int id)
        {
            var bildirim = await _bildirimDal.GetAsync(b => b.Id == id);
            await _bildirimDal.DeleteAsync(bildirim);
            return new SuccessResult(Messages.BildirimDeleted);

        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(UpdateBildirimDtoValidator))]
        public async Task<IResult> UpdateAdmin(UpdateBildirimDto bildirim)
        {
            var mappedBildirim = _mapper.Map<Bildirim>(bildirim);
            await _bildirimDal.UpdateAsync(mappedBildirim);
            return new SuccessResult(Messages.BildirimAdded);
        }

        [SecuredOperation("Admin")]
        public async Task<IDataResult<List<Bildirim>>> GetAllForAdmin()
        {
            var results = await _bildirimDal.GetAllReadOnlyAsync();
            return new SuccessDataResult<List<Bildirim>>(results,Messages.BildirimListed);
        }
        
        public async Task<IDataResult<List<Bildirim>>> GetAllByUser(int userId)
        {
            var results = await _bildirimDal.GetAllReadOnlyAsync(x=> x.KullaniciId == userId);
            return new SuccessDataResult<List<Bildirim>>(results, Messages.BildirimListed);
        }

        public async Task<IResult> MarkAsRead(int id, int userId)
        {
            var bildirim = await _bildirimDal.GetAsync(x=> x.Id == id && x.KullaniciId==userId);
            if(bildirim == null)
                return new ErrorResult(Messages.BildirimNotFound);

            bildirim.Status = true;
            await _bildirimDal.UpdateAsync(bildirim);
            return new SuccessResult(Messages.BildirimUpdated);
        }

        public async Task<IResult> MarkAsUnread(int id, int userId)
        {
            var bildirim = await _bildirimDal.GetAsync(x => x.Id == id && x.KullaniciId == userId);
            if (bildirim == null)
                return new ErrorResult(Messages.BildirimNotFound);

            bildirim.Status = false;
            await _bildirimDal.UpdateAsync(bildirim);
            return new SuccessResult(Messages.BildirimUpdated);
        }

        public async Task<IResult> DeleteByUser(int id, int userId)
        {
            var bildirim = await _bildirimDal.GetAsync(x => x.Id == id && x.KullaniciId == userId);
            if (bildirim == null)
                return new ErrorResult(Messages.BildirimNotFound);

            await _bildirimDal.DeleteAsync(bildirim);
            return new SuccessResult(Messages.BildirimUpdated);
        }

        public async Task<IResult> MarkAsReadAll(int userId)
        {
            var bildirimler = await _bildirimDal.GetAllAsync(x=>x.KullaniciId==userId && x.Status == false);
            foreach (var item in bildirimler)
            {
                item.Status = true;
                await _bildirimDal.UpdateAsync(item);
            }
            return new SuccessResult(Messages.BildirimUpdated);
        }

        public async Task<IResult> DeleteAllByUser(int userId)
        {
            var bildirimler = await _bildirimDal.GetAllAsync(x => x.KullaniciId == userId);
            foreach (var item in bildirimler)
            {
                await _bildirimDal.DeleteAsync(item);
            }
            return new SuccessResult(Messages.BildirimDeleted);
        }
    }
}
