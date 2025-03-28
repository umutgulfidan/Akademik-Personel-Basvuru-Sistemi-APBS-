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
        Task<IDataResult<List<GetIlanDto>>> GetAll();
        Task<IDataResult<List<GetIlanDto>>> GetAllActiveIlans();
        Task<IDataResult<List<GetIlanDto>>> GetAllExpiredIlans();
        Task<IDataResult<GetIlanDto>> GetById(int id);

        Task<IResult> Update(UpdateIlanDto dto);
        Task<IResult> Delete(int id);
        Task<IResult> Add(AddIlanDto dto);
        Task<IResult> ActivateIlan(int id);
        Task<IResult> DeactivateIlan(int id);
        Task<IDataResult<List<GetIlanDto>>> GetIlansByQuery(UserIlanQueryDto queryDto);
        Task<IDataResult<List<GetIlanDto>>> GetIlansByQuery(AdminIlanQueryDto queryDto);
    }
}
