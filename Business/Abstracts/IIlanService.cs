using Core.Utilities.Results;
using Entities.Concretes;
using Entities.Dtos.Alan;
using Entities.Dtos.Ilan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IIlanService
    {
        Task<IDataResult<List<Ilan>>> GetAll();
        Task<IDataResult<List<Ilan>>> GetAllActiveIlans();
        Task<IDataResult<List<Ilan>>> GetAllExpiredIlans();
        Task<IDataResult<Ilan>> GetById(int id);

        Task<IResult> Update(UpdateIlanDto dto);
        Task<IResult> Delete(int id);
        Task<IResult> Add(AddIlanDto dto);
        Task<IResult> ActivateIlan(int id);
        Task<IResult> DeactivateIlan(int id);
    }
}
