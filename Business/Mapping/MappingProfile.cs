using AutoMapper;
using Core.Entities.Concrete;
using Core.Utilities.Security.Hashing;
using Entities.Concretes;
using Entities.Dtos;
using Entities.Dtos.Alan;
using Entities.Dtos.AlanKriteri;
using Entities.Dtos.BasvuruDurumu;
using Entities.Dtos.Bildirim;
using Entities.Dtos.Bolum;
using Entities.Dtos.Ilan;
using Entities.Dtos.IlanBasvuru;
using Entities.Dtos.Kriter;
using Entities.Dtos.OperationClaim;
using Entities.Dtos.Pozisyon;
using Entities.Dtos.PuanKriteri;
using Entities.Dtos.UserOperationClaim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User Mapping

            // UserForRegisterDto -> User
            CreateMap<UserForRegisterDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => (DateTime?)null)) // Null atanıyor
                .ForMember(dest => dest.DeletedDate, opt => opt.MapFrom(src => (DateTime?)null)) // Null atanıyor
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true));

            // User -> GetUserDto
            CreateMap<User, GetUserDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

            #endregion

            #region Alan Mapping

            // UpdateAlanDto -> Alan
            CreateMap<UpdateAlanDto, Alan>();

            // AddAlanDto -> Alan
            CreateMap<AddAlanDto, Alan>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Id manuel olarak ayarlanacak

            #endregion

            #region Bolum Mapping

            // UpdateBolumDto -> Bolum
            CreateMap<UpdateBolumDto, Bolum>();

            // AddBolumDto -> Bolum
            CreateMap<AddBolumDto, Bolum>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Id manuel olarak ayarlanacak

            #endregion

            #region Pozisyon Mapping

            // UpdatePozisyonDto -> Pozisyon
            CreateMap<UpdatePozisyonDto, Pozisyon>();

            // AddPozisyonDto -> Pozisyon
            CreateMap<AddPozisyonDto, Pozisyon>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Id manuel olarak ayarlanacak

            #endregion

            #region OperationClaim Mapping

            // UpdateOperationClaimDto -> OperationClaim
            CreateMap<UpdateOperationClaimDto, OperationClaim>();

            // AddOperationClaimDto -> OperationClaim
            CreateMap<AddOperationClaimDto, OperationClaim>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Id manuel olarak ayarlanacak

            #endregion

            #region UserOperationClaim Mapping

            // UserOperationClaim -> GetUserOperationClaimDto
            CreateMap<UserOperationClaim, GetUserOperationClaimDto>()
                .ForMember(dest => dest.OperationClaimName, opt => opt.MapFrom(src => src.OperationClaim.Name));

            // UpdateUserOperationClaimDto -> UserOperationClaim
            CreateMap<UpdateUserOperationClaimDto, UserOperationClaim>();

            // AddUserOperationClaimDto -> UserOperationClaim
            CreateMap<AddUserOperationClaimDto, UserOperationClaim>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Id manuel olarak ayarlanacak

            #endregion

            #region Ilan Mapping

            // AddIlanDto -> Ilan
            CreateMap<AddIlanDto, Ilan>()
                .ForMember(dest => dest.OlusturanId, opt => opt.Ignore()) // OlusturanId genellikle kullanıcıdan alınacak bir değer olabilir, manuel olarak ayarlanabilir
                .ForMember(dest => dest.Olusturan, opt => opt.Ignore()) // Eğer kullanıcı objesini almak istiyorsanız, ekleyebilirsiniz
                .ForMember(dest => dest.Pozisyon, opt => opt.Ignore()) // Pozisyon objesini burada manuel olarak ayarlamak gerekebilir
                .ForMember(dest => dest.Bolum, opt => opt.Ignore()) // Aynı şekilde Bolum objesi de manuel ayarlanabilir
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now)); // Bu alan otomatik olarak şu anki tarihi alacak; // Status alanına default olarak true atıyoruz

            // UpdateIlanDto -> Ilan
            CreateMap<UpdateIlanDto, Ilan>()
                .ForMember(dest => dest.OlusturanId, opt => opt.Ignore()) // OlusturanId burada da manuel ayarlanabilir
                .ForMember(dest => dest.Olusturan, opt => opt.Ignore()) // Kullanıcı objesi
                .ForMember(dest => dest.Pozisyon, opt => opt.Ignore()) // Pozisyon objesi
                .ForMember(dest => dest.Bolum, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now)); // Bu alan otomatik olarak şu anki tarihi alacak; // Bolum objesi

            // Ilan -> GetIlanDto
            CreateMap<Ilan, GetIlanAdminDto>()
                .ForMember(dest => dest.Olusturan, opt => opt.MapFrom(src => src.Olusturan));

            // Ilan -> GetIlanDto
            CreateMap<Ilan, GetIlanDto>()
                .ForMember(dest => dest.Pozisyon, opt => opt.MapFrom(src => src.Pozisyon))
                .ForMember(dest => dest.Bolum, opt => opt.MapFrom(src => src.Bolum));

            // Ilan -> GetIlanDetailDto
            CreateMap<Ilan, GetIlanDetailDto>()
                .ForMember(dest => dest.Pozisyon, opt => opt.MapFrom(src => src.Pozisyon))
                .ForMember(dest => dest.Bolum, opt => opt.MapFrom(src => src.Bolum))
                .ForMember(dest => dest.AlanKriterleri, opt => opt.Ignore());

            #endregion

            #region Bildirim
            CreateMap<AddBildirimDto, Bildirim>()
                      .ForMember(dest => dest.OlusturmaTarihi, opt => opt.MapFrom(src => DateTime.Now)) // Bu alan otomatik olarak şu anki tarihi alacak
                      .ForMember(dest => dest.Status, opt => opt.MapFrom(src => false)); // Bildirim başlangıçta okunmamış olarak ayarlanacak

            CreateMap<AddBildirimDto, BildirimDto>()
                .ForMember(dest => dest.Baslik, opt => opt.MapFrom(src => src.Baslik))
                .ForMember(dest => dest.Aciklama, opt => opt.MapFrom(src => src.Aciklama))
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon))
                .ForMember(dest => dest.Renk, opt => opt.MapFrom(src => src.Renk));

            // Bildirim güncelleme DTO'sunu Bildirim modeline maple
            CreateMap<UpdateBildirimDto, Bildirim>();

            #endregion

            #region Kriter
            CreateMap<UpdateKriterDto, Kriter>();

            CreateMap<AddKriterDto, Kriter>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Id manuel olarak ayarlanacak
            #endregion

            #region Alan Kriteri
            CreateMap<UpdateAlanKriteriDto, AlanKriteri>()
                .ForMember(dest => dest.Alan, opt => opt.Ignore())
                .ForMember(dest => dest.Pozisyon, opt => opt.Ignore())
                .ForMember(dest => dest.Kriter, opt => opt.Ignore());

            CreateMap<AddAlanKriteriDto, AlanKriteri>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Alan, opt => opt.Ignore())
                .ForMember(dest => dest.Pozisyon, opt => opt.Ignore())
                .ForMember(dest => dest.Kriter, opt => opt.Ignore());
            #endregion

            #region Puan Kriteri
            CreateMap<UpdatePuanKriteriDto, PuanKriteri>()
                .ForMember(dest => dest.Alan, opt => opt.Ignore())
                .ForMember(dest => dest.Pozisyon, opt => opt.Ignore())
                .ForMember(dest => dest.Kriter, opt => opt.Ignore());

            CreateMap<AddPuanKriteriDto, PuanKriteri>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Alan, opt => opt.Ignore())
                .ForMember(dest => dest.Pozisyon, opt => opt.Ignore())
                .ForMember(dest => dest.Kriter, opt => opt.Ignore());
            #endregion

            #region Basvuru Durumu

            // UpdateAlanDto -> Alan
            CreateMap<UpdateBasvuruDurumuDto, BasvuruDurumu>();

            // AddAlanDto -> Alan
            CreateMap<AddBasvuruDurumuDto, BasvuruDurumu>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Id manuel olarak ayarlanacak
            #endregion

            #region Başvuru
            // ApplyDto => IlanBasvuru mapping
            CreateMap<ApplyDto, IlanBasvuru>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Id DB tarafından atanacak
                .ForMember(dest => dest.BasvuruDurumuId, opt => opt.MapFrom(src => 1)) // Varsayılan başvuru durumu (örnek: 1 = "Başvuru Alındı")
                .ForMember(dest => dest.Ilan, opt => opt.Ignore())
                .ForMember(dest => dest.Basvuran, opt => opt.Ignore())
                .ForMember(dest => dest.BasvuranId, opt => opt.Ignore())
                .ForMember(dest => dest.BasvuruDurumu, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now)).ForMember(dest => dest.BasvuruTarihi, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<UpdateIlanBasvuruDto, IlanBasvuru>()
           .ForMember(dest => dest.Ilan, opt => opt.Ignore()) // Navigation property, sadece ID atanıyor
           .ForMember(dest => dest.Basvuran, opt => opt.Ignore()) // Navigation property
           .ForMember(dest => dest.BasvuruDurumu, opt => opt.Ignore()) // Navigation property
           .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) // Genellikle değiştirilmez
           .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
           .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now)); ; // Güncellemede kullanılmaz


            #endregion

            //CreateMap<UserForRegisterDto, User>().ConstructUsing(
            //    (src,context) =>
            //    {
            //        byte[] passwordHash, passwordSalt;
            //        HashingHelper.CreatePasswordHash(src.Password, out passwordHash, out passwordSalt);

            //        return new User
            //        {
            //            NationalityId = src.NationalityId,
            //            FirstName = src.FirstName,
            //            LastName = src.LastName,
            //            Email = src.Email,
            //            DateOfBirth = src.DateOfBirth,
            //            PasswordHash = passwordHash,
            //            PasswordSalt = passwordSalt,
            //            Status = true,
            //            CreatedDate = DateTime.Now,
            //            DeletedDate = null,
            //            UpdatedDate = null
            //        };
            //    }

            //    );


        }
    }
}
