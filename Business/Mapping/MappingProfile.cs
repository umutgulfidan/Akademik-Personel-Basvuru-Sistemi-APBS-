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
using Entities.Dtos.IlanBasvuruDosya;
using Entities.Dtos.IlanJuri;
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
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.OperationClaims, opt => opt.Ignore()); 

            // User -> GetUserDto
            CreateMap<User, GetUserDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.OperationClaims,
                opt => opt.MapFrom(src => src.OperationClaims));


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

            // Ilan -> GetIlanAdminDto
            CreateMap<Ilan, GetIlanAdminDto>()
                .ForMember(dest => dest.Pozisyon, opt => opt.MapFrom(src => src.Pozisyon))  // Pozisyon bilgisini ekliyoruz
                .ForMember(dest => dest.Bolum, opt => opt.MapFrom(src => src.Bolum))  // Bolum bilgisini ekliyoruz
                .ForMember(dest => dest.Olusturan, opt => opt.MapFrom(src => src.Olusturan));  // Olusturan bilgisi

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
                .ForMember(dest => dest.Ilan, opt => opt.Ignore()) // Ignore navigation property 'Ilan'
                .ForMember(dest => dest.Basvuran, opt => opt.Ignore()) // Ignore navigation property 'Basvuran'
                .ForMember(dest => dest.BasvuruDurumu, opt => opt.Ignore()) // Ignore navigation property 'BasvuruDurumu'
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) // Ignore 'CreatedDate', doesn't change on update
                .ForMember(dest => dest.DeletedDate, opt => opt.Ignore()) // Ignore 'DeletedDate', doesn't change on update
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now)) // Set 'UpdatedDate' to current time
                .ForMember(dest => dest.BasvuruDurumuId, opt => opt.MapFrom(src => src.BasvuruDurumuId)); // Map 'BasvuruDurumuId' directly



            #endregion

            #region Ilan Basvuru Dosya
            // Entity'den DTO'ya dönüşüm
            CreateMap<IlanBasvuruDosya, GetIlanBasvuruDosyaDto>()
                .ForMember(dest => dest.DosyaYolu, opt => opt.MapFrom(src => src.DosyaYolu))  // DosyaYolu eşlemesi
                .ForMember(dest => dest.YuklenmeTarihi, opt => opt.MapFrom(src => src.YuklenmeTarihi)) // YuklenmeTarihi eşlemesi
                .ForMember(dest => dest.DosyaUrl, opt => opt.Ignore()); // DosyaUrl'yi ignore ediyoruz, çünkü bunu ayrı olarak alacağız
            #endregion

            #region İlan Jüri
            // AddAlanDto -> Alan
            CreateMap<AddIlanJuriDto, IlanJuri>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Kullanici, opt => opt.Ignore());

            // IlanJuri -> GetIlanJuriDto
            CreateMap<IlanJuri, GetIlanJuriDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.KullaniciId, opt => opt.MapFrom(src => src.KullaniciId))
                .ForMember(dest => dest.IlanId, opt => opt.MapFrom(src => src.IlanId))
                .ForMember(dest => dest.Juri, opt => opt.MapFrom(src => src.Kullanici)); // Mapping Kullanici to Juri

            // Mapping from User (Kullanici) to IlanJuriUserDto
            CreateMap<User, IlanJuriUserDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));
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
