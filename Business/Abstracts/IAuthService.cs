using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<User>> LoginAsync(UserForLoginDto userForLoginDto); // TC Kimlik ile login
        Task<IDataResult<User>> RegisterAsync(UserForRegisterDto userForRegisterDto); // TC Kimlik ile kayıt
        Task<bool> UserExistsAsync(string nationalityId); // Kullanıcı var mı kontrolü
        Task<IDataResult<AccessToken>> CreateAccessTokenAsync(User user); // Erişim token'ı oluşturma
    }
}
