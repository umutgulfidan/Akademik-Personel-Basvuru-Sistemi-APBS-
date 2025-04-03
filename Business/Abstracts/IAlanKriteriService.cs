using Core.Utilities.Results;
using Entities.Concretes;
using Entities.Dtos.AlanKriteri;
using Entities.Dtos.Kriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IAlanKriteriService
    {
        Task<IDataResult<List<AlanKriteri>>> GetAll();
        Task<IDataResult<AlanKriteri>> GetById(int id);

        Task<IResult> Update(UpdateAlanKriteriDto kriterDto);
        Task<IResult> Delete(int id);
        Task<IResult> Add(AddAlanKriteriDto kriterDto);
    }
}
