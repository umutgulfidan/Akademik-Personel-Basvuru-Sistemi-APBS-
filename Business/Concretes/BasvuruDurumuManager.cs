using AutoMapper;
using Business.Abstracts;
using Business.BusinessAspects;
using Business.Constants;
using Business.ValidationRules.BasvuruDurumu;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntitiyFramework;
using Entities.Concretes;
using Entities.Dtos.Alan;
using Entities.Dtos.BasvuruDurumu;
using Entities.Dtos.Ilan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class BasvuruDurumuManager : IBasvuruDurumuService
    {
        private readonly IBasvuruDurumuDal _basvuruDurumuDal;
        private readonly IMapper _mapper;

        public BasvuruDurumuManager(IBasvuruDurumuDal basvuruDurumuDal, IMapper mapper)
        {
            _basvuruDurumuDal = basvuruDurumuDal;
            _mapper = mapper;
        }

        [SecuredOperation("Admin,Yonetici")]
        [ValidationAspect(typeof(AddBasvuruDurumuDtoValidator))]
        public async Task<IResult> Add(AddBasvuruDurumuDto basvuruDurumuDto)
        {
            var alan = _mapper.Map<BasvuruDurumu>(basvuruDurumuDto);
            await _basvuruDurumuDal.AddAsync(alan);
            return new SuccessResult(Messages.BasvuruDurumuAdded);
        }
        [SecuredOperation("Admin,Yonetici")]
        public async Task<IResult> Delete(int id)
        {
            if (_basvuruDurumuDal.Get(x => x.Id == id) == null) return new ErrorResult(Messages.BasvuruDurumuNotFound);

            await _basvuruDurumuDal.DeleteByIdAsync(id);
            return new SuccessResult(Messages.BasvuruDurumuDeleted);
        }

        public async Task<IDataResult<List<BasvuruDurumu>>> GetAll()
        {
            var result = await _basvuruDurumuDal.GetAllReadOnlyAsync();
            return new SuccessDataResult<List<BasvuruDurumu>>(result, Messages.BasvuruDurumuListed);
        }

        public async Task<IDataResult<BasvuruDurumu>> GetById(int id)
        {
            var result = await _basvuruDurumuDal.GetReadOnlyAsync(x => x.Id == id);
            return new SuccessDataResult<BasvuruDurumu>(result, Messages.BasvuruDurumuListed);
        }

        [SecuredOperation("Admin,Yonetici")]
        [ValidationAspect(typeof(UpdateBasvuruDurumuDtoValidator))]
        public async Task<IResult> Update(UpdateBasvuruDurumuDto basvuruDurumuDto)
        {
            var alan = _mapper.Map<BasvuruDurumu>(basvuruDurumuDto);

            if (_basvuruDurumuDal.Get(x => x.Id == alan.Id) == null) return new ErrorResult(Messages.BasvuruDurumuNotFound);

            await _basvuruDurumuDal.UpdateAsync(alan);
            return new SuccessResult(Messages.BasvuruDurumuUpdated);
        }
    }
}
