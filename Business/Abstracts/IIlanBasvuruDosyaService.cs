using Core.Utilities.Results;
using Entities.Concretes;
using Entities.Dtos.Alan;
using Entities.Dtos.IlanBasvuruDosya;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IIlanBasvuruDosyaService
    {
        Task<IDataResult<List<GetIlanBasvuruDosyaDto>>> GetAll();
        Task<IDataResult<GetIlanBasvuruDosyaDto>> GetById(int id);
        Task<IDataResult<List<GetIlanBasvuruDosyaDto>>> GetByBasvuruId(int ilanBasvuruId);
    }
}
