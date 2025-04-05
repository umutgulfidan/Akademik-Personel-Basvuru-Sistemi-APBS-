using Core.Utilities.Results;
using Entities.Concretes;
using Entities.Dtos.Alan;
using Entities.Dtos.BasvuruDurumu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IBasvuruDurumuService
    {
        Task<IDataResult<List<BasvuruDurumu>>> GetAll();
        Task<IDataResult<BasvuruDurumu>> GetById(int id);

        Task<IResult> Update(UpdateBasvuruDurumuDto basvuruDurumuDto);
        Task<IResult> Delete(int id);
        Task<IResult> Add(AddBasvuruDurumuDto basvuruDurumuDto);
    }
}
