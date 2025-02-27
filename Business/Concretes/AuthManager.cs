using Business.Dtos;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Concretes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class AuthManager : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenHelper _tokenHelper;

    public AuthManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHelper tokenHelper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHelper = tokenHelper;
    }

    // Kayıt olma işlemi
    public async Task<IDataResult<UserForTokenDto>> Register(UserForRegisterDto userForRegisterDto)
    {
        // Şifre doğrulaması: Şifreler eşleşiyor mu?
        if (userForRegisterDto.Password != userForRegisterDto.ConfirmPassword)
        {
            return new ErrorDataResult<UserForTokenDto>("Şifreler uyuşmuyor.");
        }

        var user = new AppUser
        {
            UserName = userForRegisterDto.Email,
            FirstName = userForRegisterDto.FirstName,
            LastName = userForRegisterDto.LastName,
            Email = userForRegisterDto.Email,
            NationalityId = userForRegisterDto.NationalityId,
            CreatedDate = DateTime.Now,
            Status = true
        };

        var result = await _userManager.CreateAsync(user, userForRegisterDto.Password);

        if (result.Succeeded)
        {
            var userForTokenDto = new UserForTokenDto
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                NationalityId = user.NationalityId
            };

            // Token oluşturma
            var tokenResult = CreateAccessToken(userForTokenDto); // Token oluşturma

            if (tokenResult.IsSuccess)
            {
                userForTokenDto.Token = tokenResult.Data.Token; // Token'ı DTO'ya ekleyelim
            }

            return new SuccessDataResult<UserForTokenDto>(userForTokenDto, "Kayıt başarılı.");
        }

        return new ErrorDataResult<UserForTokenDto>("Kayıt sırasında hata oluştu.");
    }


    public async Task<IDataResult<UserForTokenDto>> Login(UserForLoginDto userForLoginDto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.NationalityId == userForLoginDto.NationalityId);

        if (user == null)
        {
            return new ErrorDataResult<UserForTokenDto>("Kullanıcı bulunamadı.");
        }

        var result = await _signInManager.PasswordSignInAsync(user, userForLoginDto.Password, false, false);

        if (result.Succeeded)
        {
            var userForTokenDto = new UserForTokenDto
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                NationalityId = user.NationalityId
            };

            // Token oluşturma
            var tokenResult = CreateAccessToken(userForTokenDto); // Token oluşturma

            if (tokenResult.IsSuccess)
            {
                userForTokenDto.Token = tokenResult.Data.Token; // Token'ı DTO'ya ekleyelim
            }

            return new SuccessDataResult<UserForTokenDto>(userForTokenDto, "Giriş başarılı.");
        }

        return new ErrorDataResult<UserForTokenDto>("Geçersiz şifre.");
    }


    public IDataResult<AccessToken> CreateAccessToken(UserForTokenDto userForTokenDto)
    {
        // Kullanıcının rollerini almak için userForTokenDto üzerinden erişim sağlıyoruz
        var operationClaims = new List<OperationClaimForTokenDto>();

        // Kullanıcı rolünü ekliyoruz
        // Burada, userForTokenDto üzerinden kullanıcının rollerini alıyoruz.
        // Örnek: userForTokenDto.Roles yerine gerçek roller eklenmeli.
        foreach (var role in userForTokenDto.Roles)
        {
            operationClaims.Add(new OperationClaimForTokenDto { Name = role });
        }

        // Şimdi CreateToken metoduna doğru türde parametre gönderiyoruz
        var accessToken = _tokenHelper.CreateToken(userForTokenDto, operationClaims);

        return new SuccessDataResult<AccessToken>(accessToken, "Token oluşturuldu.");
    }


    // Kullanıcı var mı kontrolü
    public async Task<IResult> UserExists(string nationalityId)
    {
        var user = await _userManager.FindByEmailAsync(nationalityId);
        if (user != null)
        {
            return new ErrorResult("Kullanıcı zaten var.");
        }
        return new SuccessResult("Kullanıcı bulunamadı.");
    }
}
