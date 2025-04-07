using AutoMapper;
using Business.Abstracts;
using Business.BusinessAspects;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Dtos.IlanBasvuruDosya;

public class IlanBasvuruDosyaManager : IIlanBasvuruDosyaService
{
    IIlanBasvuruDosyaDal _ilanBasvuruDosyaDal;
    IFileService _fileService;
    IMapper _mapper;

    public IlanBasvuruDosyaManager(IIlanBasvuruDosyaDal ilanBasvuruDosyaDal, IFileService fileService, IMapper mapper)
    {
        _ilanBasvuruDosyaDal = ilanBasvuruDosyaDal;
        _fileService = fileService;
        _mapper = mapper;
    }

    [SecuredOperation("Admin,Yonetici,Juri")]
    public async Task<IDataResult<List<GetIlanBasvuruDosyaDto>>> GetAll()
    {
        var result = await _ilanBasvuruDosyaDal.GetAllReadOnlyAsync();

        // Map the entity list to DTO list
        var mappedResult = _mapper.Map<List<GetIlanBasvuruDosyaDto>>(result);

        // For each DTO, fetch the file URL and set it
        foreach (var item in mappedResult)
        {
            item.DosyaUrl = await _fileService.GetPreSignedUrlAsync(item.DosyaYolu,15);
        }

        return new SuccessDataResult<List<GetIlanBasvuruDosyaDto>>(mappedResult, Messages.IlanBasvuruDosyaListed);
    }

    [SecuredOperation("Admin,Yonetici,Juri")]
    public async Task<IDataResult<GetIlanBasvuruDosyaDto>> GetById(int id)
    {
        var result = await _ilanBasvuruDosyaDal.GetReadOnlyAsync(x => x.Id == id);

        // Map the entity to DTO
        var mappedResult = _mapper.Map<GetIlanBasvuruDosyaDto>(result);

        // Fetch the file URL and set it
        mappedResult.DosyaUrl = await _fileService.GetPreSignedUrlAsync(mappedResult.DosyaYolu,15);

        return new SuccessDataResult<GetIlanBasvuruDosyaDto>(mappedResult, Messages.IlanBasvuruDosyaListed);
    }

    [SecuredOperation("Admin,Yonetici,Juri")]
    public async Task<IDataResult<List<GetIlanBasvuruDosyaDto>>> GetByBasvuruId(int basvuruId)
    {
        var result = await _ilanBasvuruDosyaDal.GetAllReadOnlyAsync(x => x.BasvuruId == basvuruId);

        // Map the entity list to DTO list
        var mappedResult = _mapper.Map<List<GetIlanBasvuruDosyaDto>>(result);

        // For each DTO, fetch the file URL and set it
        foreach (var item in mappedResult)
        {
            item.DosyaUrl = await _fileService.GetPreSignedUrlAsync(item.DosyaYolu, 15);
        }

        return new SuccessDataResult<List<GetIlanBasvuruDosyaDto>>(mappedResult, Messages.IlanBasvuruDosyaListed);
    }
}
