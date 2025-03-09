
using Core.Utilities.Results;
using Entities.Concretes;
using Entities.Dtos.Bolum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IBolumService
    {
        Task<IDataResult<List<Bolum>>> GetAll();
        Task<IDataResult<Bolum>> GetById(int id);

        Task<IResult> Update(UpdateBolumDto updateBolumDto);
        Task<IResult> Delete(int id);
        Task<IResult> Add(AddBolumDto addBolumDto);
    }
}
