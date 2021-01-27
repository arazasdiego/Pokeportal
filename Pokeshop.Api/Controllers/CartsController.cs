using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokeshop.Api.Dtos.CartDtos;
using Pokeshop.Api.Infrastructure.Services;
using Pokeshop.Api.Models.Carts;
using Pokeshop.Api.Repositories;
using System;
using System.Threading.Tasks;

namespace Pokeshop.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("carts")]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICurrentUserService _currentUser;

        public CartsController(ICartRepository cartRepository, ICurrentUserService currentUser)
        {
            _cartRepository = cartRepository;
            _currentUser = currentUser;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                string userId = _currentUser.GetId();
                var cart = await _cartRepository.GetAsync(userId);
                if (cart == null) return NotFound();

                return Ok(cart);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CartAddDto model)
        {
            try
            {
                string userId = _currentUser.GetId();
                var result = await _cartRepository.AddAsync(model, userId);
                if (result == 0) return BadRequest("Server error.");

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CartUpdateDto model)
        {
            try
            {
                string userId = _currentUser.GetId();
                var result = await _cartRepository.UpdateAsync(model, userId);
                if (result == 0) return BadRequest("Server error.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] CartDeleteVm model)
        {
            try
            {
                string userId = _currentUser.GetId();
                await _cartRepository.DeleteAsync(model.ProductIds, userId);
   
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
