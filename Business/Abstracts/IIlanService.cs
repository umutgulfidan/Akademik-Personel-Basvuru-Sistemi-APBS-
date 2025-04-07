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
        Task<IDataResult<GetIlanDto>> GetById(int id);

        Task<IDataResult<GetIlanDetailDto>> GetIlanDetail(int id);

        Task<IResult> Update(UpdateIlanDto dto);
        Task<IResult> Delete(int id);
        Task<IResult> Add(AddIlanDto dto);
        Task<IResult> ActivateIlan(int id);
        Task<IResult> DeactivateIlan(int id);
        Task<IDataResult<List<GetIlanDto>>> GetIlansByQuery(UserIlanQueryDto queryDto);
        Task<IDataResult<List<GetIlanAdminDto>>> GetIlansByQueryForAdmin(AdminIlanQueryDto queryDto);

        Task<IDataResult<List<GetIlanDto>>> GetAppliedIlanByUser(int userId);
    }
}
