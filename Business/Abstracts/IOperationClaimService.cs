using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concretes;
using Entities.Dtos.Alan;
using Entities.Dtos.OperationClaim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IOperationClaimService
    {
        Task<IDataResult<List<OperationClaim>>> GetAll();
        Task<IDataResult<OperationClaim>> GetById(int id);

        Task<IResult> Update(UpdateOperationClaimDto operationClaimDto);
        Task<IResult> Delete(int id);
        Task<IResult> Add(AddOperationClaimDto operationClaimDto);
    }
}
