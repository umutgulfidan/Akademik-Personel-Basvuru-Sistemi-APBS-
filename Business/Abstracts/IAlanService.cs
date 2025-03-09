using Core.Utilities.Results;
using Entities.Concretes;
using Entities.Dtos.Alan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IAlanService
    {
        Task<IDataResult<List<Alan>>> GetAll();
        Task<IDataResult<Alan>> GetById(int id);

        Task<IResult> Update(UpdateAlanDto alan);
        Task<IResult> Delete(int id);
        Task<IResult> Add(AddAlanDto alan);
    }
}
