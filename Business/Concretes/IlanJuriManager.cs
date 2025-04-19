using AutoMapper;
using Business.Abstracts;
using Business.BusinessAspects;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntitiyFramework;
using Entities.Concretes;
using Entities.Dtos.Alan;
using Entities.Dtos.IlanJuri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class IlanJuriManager : IIlanJuriService
    {
        private readonly IIlanJuriDal _ilanJuriDal;
        private readonly IMapper _mapper;

        public IlanJuriManager(IIlanJuriDal ilanJuriDal, IMapper mapper)
        {
            _ilanJuriDal = ilanJuriDal;
            _mapper = mapper;
        }

        [SecuredOperation("Admin,Yonetici")]
        public async Task<IResult> Add(AddIlanJuriDto ilanJuri)
        {
            var mappedEntity = _mapper.Map<IlanJuri>(ilanJuri);
            await _ilanJuriDal.AddAsync(mappedEntity);
            return new SuccessResult(Messages.IlanJuriAdded);
        }

        [SecuredOperation("Admin,Yonetici")]
        public async Task<IResult> Delete(int id)
        {
            if (_ilanJuriDal.Get(x => x.Id == id) == null) return new ErrorResult(Messages.IlanJuriNotFound);

            await _ilanJuriDal.DeleteByIdAsync(id);
            return new SuccessResult(Messages.IlanJuriDeleted);
        }

        [SecuredOperation("Admin,Yonetici")]
        public async Task<IDataResult<List<IlanJuri>>> GetAll()
        {
            var result = await _ilanJuriDal.GetAllReadOnlyAsync();
            return new SuccessDataResult<List<IlanJuri>>(result, Messages.IlanJuriListed);
        }

        [SecuredOperation("Admin,Yonetici")]
        public async Task<IDataResult<IlanJuri>> GetById(int id)
        {
            var result = await _ilanJuriDal.GetReadOnlyAsync(x => x.Id == id);
            return new SuccessDataResult<IlanJuri>(result, Messages.IlanJuriListed);
        }

        public async Task<IDataResult<List<GetIlanJuriDto>>> GetByIlanId(int ilanId)
        {
            var results = await _ilanJuriDal.GetAllWithIncludes(x => x.IlanId == ilanId);
            var mappedResults = _mapper.Map<List<GetIlanJuriDto>>(results);
            return new SuccessDataResult<List<GetIlanJuriDto>>(mappedResults, Messages.IlanJuriListed);
        }
    }
}
