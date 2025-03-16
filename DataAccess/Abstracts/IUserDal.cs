using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Entities.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstracts
{
    public interface IUserDal : IEntityRepository<User>
    {
        Task<List<OperationClaim>> GetClaimsAsync(User user);

        Task<List<User>> GetUsersByQueryAsync(UserQueryDto query);



    }
}
