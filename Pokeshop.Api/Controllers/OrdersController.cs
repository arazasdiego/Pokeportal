using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokeshop.Api.Infrastructure.Services;
using Pokeshop.Api.Models.Orders;
using Pokeshop.Api.Parameters;
using Pokeshop.Api.Repositories;
using System;
using System.Threading.Tasks;

namespace Pokeshop.Api.Controllers
{
    [ApiController]
    [Route("orders")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICurrentUserService _currentUser;

        public OrdersController(IOrderRepository orderRepository, ICurrentUserService currentUser)
        {
            _orderRepository = orderRepository;
            _currentUser = currentUser;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] OrderParameter parameter)
        {
            try
            {
                var orders = await _orderRepository.GetAllAsync(parameter.PageNumber, parameter.PageSize, parameter.OrderStatus);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{orderCode}")]
        public async Task<IActionResult> Get(string orderCode)
        {
            try
            {
                var order = await _orderRepository.GetByOrderCodeAsync(orderCode);
                if (order == null) return NotFound();

                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetByAllUser([FromQuery] OrderParameter parameter)
        {
            try
            {
                string userId = _currentUser.GetId();
                var orders = await _orderRepository.GetAllByUserAsync(parameter.PageNumber, parameter.PageSize, userId, parameter.OrderStatus);
                
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{orderCode}")]
        public async Task<IActionResult> GetByUser(string orderCode)
        {
            try
            {
                string userId = _currentUser.GetId();
                var order = await _orderRepository.GetByUserAsync(userId, orderCode);
                if (order == null) return NotFound();

                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            try
            {
                string userId = _currentUser.GetId();
                var result = await _orderRepository.PlaceOrderAsync(userId);
                if (result == false) return BadRequest("Server error.");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("pay")]
        public async Task<IActionResult> Pay([FromBody] OrderPayVm model)
        {
            try
            {
                var result = await _orderRepository.PayAsync(model.OrderCode, model.AmountPaid);
                if (result == false) return BadRequest("Server error");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string orderCode)
        {
            try
            {
                var result = await _orderRepository.DeleteAsync(orderCode);
                if (result == 0) return BadRequest("Server error");

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
