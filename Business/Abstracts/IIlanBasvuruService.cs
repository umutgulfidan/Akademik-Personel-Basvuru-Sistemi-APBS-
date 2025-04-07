using Core.Utilities.Results;
using Entities.Concretes;
using Entities.Dtos.Alan;
using Entities.Dtos.IlanBasvuru;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IIlanBasvuruService
    {
        Task<IResult> Apply(ApplyDto applyDto,int userId);
        Task<IDataResult<bool>> IsAppliedBefore(int userId,int ilanId);

        Task<IDataResult<List<IlanBasvuru>>> GetAll();
        Task<IDataResult<IlanBasvuru>> GetById(int id);
        Task<IDataResult<List<IlanBasvuru>>> GetByUser(int userId);
        Task<IResult> Update(UpdateIlanBasvuruDto alan);
        Task<IResult> Delete(int id);
    }
}
