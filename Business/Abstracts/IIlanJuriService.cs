using Core.Utilities.Results;
using Entities.Concretes;
using Entities.Dtos.Alan;
using Entities.Dtos.IlanJuri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IIlanJuriService
    {
        Task<IDataResult<List<IlanJuri>>> GetAll();
        Task<IDataResult<IlanJuri>> GetById(int id);

        Task<IDataResult<List<GetIlanJuriDto>>> GetByIlanId(int ilanId);

        //Task<IResult> Update(UpdateIlanJuriDto alan);
        Task<IResult> Delete(int id);
        Task<IResult> Add(AddIlanJuriDto ilanJuri);
    }
}
