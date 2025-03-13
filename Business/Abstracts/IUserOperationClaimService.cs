using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concretes;
using Entities.Dtos.Pozisyon;
using Entities.Dtos.UserOperationClaim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IUserOperationClaimService
    {
        Task<IDataResult<List<GetUserOperationClaimDto>>> GetAll();
        Task<IDataResult<GetUserOperationClaimDto>> GetById(int id);
        Task<IDataResult<List<GetUserOperationClaimDto>>> GetByUser(int userId);

        Task<IResult> Update(UpdateUserOperationClaimDto updateDto);
        Task<IResult> Delete(int id);
        Task<IResult> Add(AddUserOperationClaimDto addDto);
    }
}
