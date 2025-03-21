using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.ValidationRules.Pozisyon;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntitiyFramework;
using Entities.Concretes;
using Entities.Dtos.Pozisyon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business.Concretes
{
    public class PozisyonManager : IPozisyonService
    {
        private readonly IPozisyonDal _pozisyonDal;
        private readonly IMapper _mapper;

        public PozisyonManager(IPozisyonDal pozisyonDal, IMapper mapper)
        {
            _pozisyonDal = pozisyonDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(AddPozisyonDtoValidator))]
        public async Task<IResult> Add(AddPozisyonDto addPozisyonDto)
        {
            var data = _mapper.Map<Pozisyon>(addPozisyonDto);
            await _pozisyonDal.AddAsync(data);
            return new SuccessResult(Messages.PozisyonAdded);
        }

        public async Task<IResult> Delete(int id)
        {
            if (_pozisyonDal.Get(x => x.Id == id) == null) return new ErrorResult(Messages.PozisyonNotFound);
            await _pozisyonDal.DeleteByIdAsync(id);
            return new SuccessResult(Messages.PozisyonDeleted);
        }

        public async Task<IDataResult<List<Pozisyon>>> GetAll()
        {
            var data = await _pozisyonDal.GetAllAsync();
            return new SuccessDataResult<List<Pozisyon>>(data,Messages.PozisyonListed);
        }

        public async Task<IDataResult<Pozisyon>> GetById(int id)
        {
            var data = await _pozisyonDal.GetAsync(x=> x.Id == id);
            return new SuccessDataResult<Pozisyon>(data,Messages.PozisyonListed);
        }
        [ValidationAspect(typeof(UpdatePozisyonDtoValidator))]
        public async Task<IResult> Update(UpdatePozisyonDto updatePozisyonDto)
        {
            var data = _mapper.Map<Pozisyon>(updatePozisyonDto);
            if (_pozisyonDal.Get(x => x.Id == data.Id) == null) return new ErrorResult(Messages.PozisyonNotFound);
            await _pozisyonDal.UpdateAsync(data);
            return new SuccessResult(Messages.PozisyonUpdated);
        }
    }
}
