using Core.Utilities.Results;
using Entities.Concretes;
using Entities.Dtos.AlanKriteri;
using Entities.Dtos.PuanKriteri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IPuanKriteriService
    {
        Task<IDataResult<List<PuanKriteri>>> GetAll();
        Task<IDataResult<PuanKriteri>> GetById(int id);

        Task<IResult> Update(UpdatePuanKriteriDto kriterDto);
        Task<IResult> Delete(int id);
        Task<IResult> Add(AddPuanKriteriDto kriterDto);
    }
}
