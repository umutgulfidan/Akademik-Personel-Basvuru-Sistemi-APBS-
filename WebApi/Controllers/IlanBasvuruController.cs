﻿using Business.Abstracts;
using Core.Extensions.Claims;
using Entities.Concretes;
using Entities.Dtos.Alan;
using Entities.Dtos.Ilan;
using Entities.Dtos.IlanBasvuru;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IlanBasvuruController : ControllerBase
    {
        private readonly IIlanBasvuruService _ilanBasvuruService;

        public IlanBasvuruController(IIlanBasvuruService ilanBasvuruService)
        {
            _ilanBasvuruService = ilanBasvuruService;
        }

        [HttpPost("Apply")]
        public async Task<IActionResult> Apply([FromForm] ApplyDto applyDto)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                return Unauthorized("User is not authenticated.");
            }

            var userId = HttpContext.User.ClaimUserId();

            if (userId == null)
            {
                return Unauthorized("Invalid or missing user ID in token.");
            }

            var result = await _ilanBasvuruService.Apply(applyDto,userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("IsAppliedBefore")]
        public async Task<IActionResult> IsAppliedBefore(int ilanId)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                return Unauthorized("User is not authenticated.");
            }

            var userId = HttpContext.User.ClaimUserId();

            if (userId == null)
            {
                return Unauthorized("Invalid or missing user ID in token.");
            }

            var result = await _ilanBasvuruService.IsAppliedBefore(userId,ilanId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ilanBasvuruService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyquery")]
        public async Task<IActionResult> GetByQuery([FromQuery]IlanBasvuruQueryDto queryDto)
        {
            var result = await _ilanBasvuruService.GetAllByQuery(queryDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _ilanBasvuruService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ilanBasvuruService.Delete(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateIlanBasvuruDto updateIlanBasvuruDto)
        {
            var result = await _ilanBasvuruService.Update(updateIlanBasvuruDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}
