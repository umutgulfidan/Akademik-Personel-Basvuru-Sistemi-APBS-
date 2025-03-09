using AutoMapper;
using Business.Dtos;
using Business.Dtos.Alan;
using Core.Entities.Concrete;
using Core.Utilities.Security.Hashing;
using Entities.Concretes;
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
            CreateMap<UserForRegisterDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => (DateTime?)null)) // Null atanıyor
                .ForMember(dest => dest.DeletedDate, opt => opt.MapFrom(src => (DateTime?)null)) // Null atanıyor
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true));


            CreateMap<User, GetUserDto>();

            CreateMap<UpdateAlanDto, Alan>();
            CreateMap<AddAlanDto, Alan>()
                .ForMember(dest=> dest.Id, opt=> opt.Ignore());

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
