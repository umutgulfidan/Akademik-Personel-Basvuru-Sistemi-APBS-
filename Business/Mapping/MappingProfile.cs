using AutoMapper;
using Core.Entities.Concrete;
using Core.Utilities.Security.Hashing;
using Entities.Concretes;
using Entities.Dtos;
using Entities.Dtos.Alan;
using Entities.Dtos.Bolum;
using Entities.Dtos.Ilan;
using Entities.Dtos.OperationClaim;
using Entities.Dtos.Pozisyon;
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
            CreateMap<User, GetUserDto>();

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
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true)); // Status alanına default olarak true atıyoruz

            // UpdateIlanDto -> Ilan
            CreateMap<UpdateIlanDto, Ilan>()
                .ForMember(dest => dest.OlusturanId, opt => opt.Ignore()) // OlusturanId burada da manuel ayarlanabilir
                .ForMember(dest => dest.Olusturan, opt => opt.Ignore()) // Kullanıcı objesi
                .ForMember(dest => dest.Pozisyon, opt => opt.Ignore()) // Pozisyon objesi
                .ForMember(dest => dest.Bolum, opt => opt.Ignore()); // Bolum objesi

            // Ilan -> GetIlanDto
            CreateMap<Ilan, GetIlanDto>()
                .ForMember(dest => dest.Olusturan, opt => opt.MapFrom(src => src.Olusturan));

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
