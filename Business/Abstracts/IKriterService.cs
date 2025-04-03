using Core.Utilities.Results;
using Entities.Concretes;
using Entities.Dtos.Bolum;
using Entities.Dtos.Kriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IKriterService
    {
        Task<IDataResult<List<Kriter>>> GetAll();
        Task<IDataResult<Kriter>> GetById(int id);

        Task<IResult> Update(UpdateKriterDto kriter);
        Task<IResult> Delete(int id);
        Task<IResult> Add(AddKriterDto kriter);
    }
}
