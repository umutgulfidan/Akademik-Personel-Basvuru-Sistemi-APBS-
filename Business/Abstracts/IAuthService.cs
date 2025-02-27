using Business.Dtos;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;

public interface IAuthService
{
    Task<IDataResult<UserForTokenDto>> Register(UserForRegisterDto userForRegisterDto); // Register method updated to include password
    Task<IDataResult<UserForTokenDto>> Login(UserForLoginDto userForLoginDto); // Login method remains unchanged
    IDataResult<AccessToken> CreateAccessToken(UserForTokenDto userForTokenDto); // Access token creation remains unchanged
    Task<IResult> UserExists(string nationalityId); // User exists check remains unchanged
}
