using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Business.ValidationRules.Auth;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using MernisService;
using System;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly IMapper _mapper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IMapper mapper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
        }

        // Kullanıcıyı login yapma (TC Kimlik No ile)
        [ValidationAspect(typeof(LoginValidator))]
        public async Task<IDataResult<User>> LoginAsync(UserForLoginDto userForLoginDto)
        {
            var userToCheck = await _userService.GetByNationalityIdAsync(userForLoginDto.NationalityId);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if (userToCheck.Data.Status == false)
            {
                return new ErrorDataResult<User>(Messages.UserPassiveAccount);
            }
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.UserPasswordError);
            }
            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
        }

        // Kullanıcıyı kayıt etme (TC Kimlik No ile)

        [ValidationAspect(typeof(RegisterValidator))]
        public async Task<IDataResult<User>> RegisterAsync(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash;
            byte[] passwordSalt;

            if (!CheckPerson(userForRegisterDto))
            {
                return new ErrorDataResult<User>(Messages.InvalidUser);
            }


            if (await UserExistsAsync(userForRegisterDto.NationalityId))
            {
                return new ErrorDataResult<User>(Messages.UserAlreadyExists);
            }


            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);

            var user = _mapper.Map<User>(userForRegisterDto);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _userService.AddAsync(user);

            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        // Kullanıcı var mı kontrol etme (TC Kimlik No ile)
        public async Task<bool> UserExistsAsync(string nationalityId)
        {
            var user = await _userService.GetByNationalityIdAsync(nationalityId);
            return user.Data != null;
        }

        // Erişim token'ı oluşturma
        public async Task<IDataResult<AccessToken>> CreateAccessTokenAsync(User user)
        {
            var claims = await _userService.GetClaimsAsync(user); // Claims alınıyor
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.TokenCreated);
        }

        private bool CheckPerson(UserForRegisterDto user)
        {
            KPSPublicSoapClient client = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);

            // TCKimlikNoDogrulaAsync metoduna doğru parametreler ile çağrı yapıyoruz
            return client.TCKimlikNoDogrulaAsync(
                Convert.ToInt64(user.NationalityId),  // TC Kimlik Numarası
                user.FirstName,                       // Ad
                user.LastName,                        // Soyad
                user.DateOfBirth.Year                 // Doğum Yılı
            ).Result.Body.TCKimlikNoDogrulaResult;
        }
    }
}
