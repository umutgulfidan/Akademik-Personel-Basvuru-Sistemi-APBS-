using Core.Utilities.Results;
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
    }
}
