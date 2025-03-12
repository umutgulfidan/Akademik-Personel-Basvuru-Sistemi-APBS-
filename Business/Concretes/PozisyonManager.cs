using AutoMapper;
using Business.Abstracts;
using Business.ValidationRules.Pozisyon;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concretes;
using Entities.Dtos.Pozisyon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return new SuccessResult();
        }

        public async Task<IResult> Delete(int id)
        {
            await _pozisyonDal.DeleteByIdAsync(id);
            return new SuccessResult();
        }

        public async Task<IDataResult<List<Pozisyon>>> GetAll()
        {
            var data = await _pozisyonDal.GetAllAsync();
            return new SuccessDataResult<List<Pozisyon>>(data);
        }

        public async Task<IDataResult<Pozisyon>> GetById(int id)
        {
            var data = await _pozisyonDal.GetAsync(x=> x.Id == id);
            return new SuccessDataResult<Pozisyon>(data);
        }
        [ValidationAspect(typeof(UpdatePozisyonDtoValidator))]
        public async Task<IResult> Update(UpdatePozisyonDto updatePozisyonDto)
        {
            var data = _mapper.Map<Pozisyon>(updatePozisyonDto);
            await _pozisyonDal.UpdateAsync(data);
            return new SuccessResult();
        }
    }
}
