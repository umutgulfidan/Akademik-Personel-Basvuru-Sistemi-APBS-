using Business.Abstracts;
using Entities.Dtos.OperationClaim;
using Entities.Dtos.Pozisyon;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimController : ControllerBase
    {
        private readonly IOperationClaimService _operationClaimService;

        public OperationClaimController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _operationClaimService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _operationClaimService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _operationClaimService.Delete(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateOperationClaimDto updateOperationClaimDto)
        {
            var result = await _operationClaimService.Update(updateOperationClaimDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AddOperationClaimDto addOperationClaimDto)
        {
            var result = await _operationClaimService.Add(addOperationClaimDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
