using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.ValidationRules.Alan;
using Business.ValidationRules.Kriter;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntitiyFramework;
using Entities.Concretes;
using Entities.Dtos.Kriter;


namespace Business.Concretes
{
    public class KriterManager : IKriterService
    {
        private readonly IKriterDal _kriterDal;
        private readonly IMapper _mapper;

        public KriterManager(IKriterDal kriterDal, IMapper mapper)
        {
            _kriterDal = kriterDal;
            _mapper = mapper;
        }
        [ValidationAspect(typeof(AddKriterDtoValidator))]
        public async Task<IResult> Add(AddKriterDto kriterDto)
        {
            var kriter = _mapper.Map<Kriter>(kriterDto);
            await _kriterDal.AddAsync(kriter);
            return new SuccessResult(Messages.KriterAdded);
        }

        public async Task<IResult> Delete(int id)
        {
            if (await _kriterDal.GetAllReadOnlyAsync(x => x.Id == id) == null) return new ErrorResult(Messages.KriterNotFound);

            await _kriterDal.DeleteByIdAsync(id);
            return new SuccessResult(Messages.KriterDeleted);
        }

        public async Task<IDataResult<List<Kriter>>> GetAll()
        {
            var result = await _kriterDal.GetAllReadOnlyAsync();
            return new SuccessDataResult<List<Kriter>>(result,Messages.KriterListed);
        }

        public async Task<IDataResult<Kriter>> GetById(int id)
        {
            var result = await _kriterDal.GetReadOnlyAsync(x=> x.Id == id);
            return new SuccessDataResult<Kriter>(result, Messages.KriterListed);
        }
        [ValidationAspect(typeof(UpdateKriterDtoValidator))]
        public async Task<IResult> Update(UpdateKriterDto kriterDto)
        {
            var kriter = _mapper.Map<Kriter>(kriterDto);
            if ( await _kriterDal.GetAsync(x => x.Id == kriterDto.Id) == null) return new ErrorResult(Messages.KriterNotFound);

            await _kriterDal.UpdateAsync(kriter);
            return new SuccessResult(Messages.KriterUpdated);
        }
    }
}
