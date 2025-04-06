using AutoMapper;
using Business.Abstracts;
using Business.BusinessAspects;
using Business.Constants;
using Business.ValidationRules.IlanBasvuru;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concretes;
using Entities.Dtos.IlanBasvuru;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class IlanBasvuruManager : IIlanBasvuruService
    {
        private readonly IIlanBasvuruDal _ilanBasvuruDal;
        private readonly IIlanBasvuruDosyaDal _ilanBasvuruDosyaDal;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public IlanBasvuruManager(IIlanBasvuruDal ilanBasvuruDal, IIlanBasvuruDosyaDal ilanBasvuruDosyaDal, IMapper mapper, IFileService fileService)
        {
            _ilanBasvuruDal = ilanBasvuruDal;
            _ilanBasvuruDosyaDal = ilanBasvuruDosyaDal;
            _mapper = mapper;
            _fileService = fileService;
        }


        [TransactionScopeAspect]
        [AuthenticatedOperation]
        [ValidationAspect(typeof(ApplyDtoValidator))]
        public async Task<IResult> Apply(ApplyDto applyDto, int userId)
        {
            var mappedIlanBasvuru = _mapper.Map<IlanBasvuru>(applyDto);
            mappedIlanBasvuru.BasvuranId = userId;

            await _ilanBasvuruDal.AddAsync(mappedIlanBasvuru);

            int basvuruId = mappedIlanBasvuru.Id;

            // 2. Dosyalar işleniyor
            foreach (var fileDto in applyDto.Files)
            {
                var filePath = await _fileService.UploadFileAsync(fileDto.File,Paths.AwsApplyFileFolder);
                foreach (var kriterId in fileDto.KriterIds)
                {

                    // dosya yolu sonradan atanaca
                    var basvuruDosya = new IlanBasvuruDosya
                    {
                        BasvuruId = basvuruId,
                        KriterId = kriterId,
                        YuklenmeTarihi = DateTime.Now,
                        DosyaYolu = filePath
                    };

                    await _ilanBasvuruDosyaDal.AddAsync(basvuruDosya);
                }
            }

            return new SuccessResult(Messages.BasvuruAdded);
        }



        public async Task<IDataResult<bool>> IsAppliedBefore(int userId, int ilanId)
        {
            var result = await _ilanBasvuruDal.GetReadOnlyAsync(x => x.BasvuranId == userId && x.IlanId==ilanId);
            if (result == null)
            {
                return new SuccessDataResult<bool>(false, Messages.NotAppliedYetMessage);
            }
            return new SuccessDataResult<bool>(true, Messages.AlreadyAppliedMessage);
        }
    }
}
