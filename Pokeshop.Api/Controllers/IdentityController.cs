using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokeshop.Api.Dtos.IdentityDtos;
using Pokeshop.Api.Models.Identity;
using Pokeshop.Api.Parameters;
using Pokeshop.Api.Repositories;
using System;
using System.Threading.Tasks;

namespace Pokeshop.Api.Controllers
{
    [ApiController]
    [Route("identity")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityRepository _identityRepository;

        public IdentityController(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            try
            {
                var response = await _identityRepository.RegisterAsync(model);
                if (response == 0) return BadRequest("Server error");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginVm model)
        {
            try
            {
                var response = await _identityRepository.LoginAsync(model);
                if (response.Success == false) return BadRequest("Invalid email/password");
                var token = _identityRepository.GenerateJwtToken(response.UserId, response.Email, response.Secret);           

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetByPage([FromQuery] IdentityParameter parameter)
        {
            try
            {
                var users = await _identityRepository.GetByPageAsync(parameter.PageNumber, parameter.PageSize);
                if (users == null) return NotFound();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("id")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var user = await _identityRepository.GetByIdAsync(id);
                if (user == null) return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(IdentityUpdateDto model)
        {
            try
            {
                var result = await _identityRepository.UpdateAsync(model);
                if (result == 0) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _identityRepository.DeleteAsync(id);
                if (result == 0) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
