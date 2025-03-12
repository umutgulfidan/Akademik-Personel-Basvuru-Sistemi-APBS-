using Core.Utilities.Results;
using Entities.Concretes;
using Entities.Dtos.Pozisyon;

namespace Business.Abstracts
{
    public interface IPozisyonService
    {
        Task<IDataResult<List<Pozisyon>>> GetAll();
        Task<IDataResult<Pozisyon>> GetById(int id);

        Task<IResult> Update(UpdatePozisyonDto updatePozisyonDto);
        Task<IResult> Delete(int id);
        Task<IResult> Add(AddPozisyonDto addPozisyonDto);
    }
}
