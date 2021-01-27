using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pokeshop.Api.Dtos.ProductDtos;
using Pokeshop.Api.Models.Products;
using Pokeshop.Api.Parameters;
using Pokeshop.Api.Repositories;
using System;
using System.Threading.Tasks;

namespace Pokeshop.Api.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProductParameter parameter)
        {
            try
            {
                var products = await _productRepository.GetByPageAsync(parameter.PageNumber, parameter.PageSize);
                if (products == null) return NotFound();

                var metadata = new
                {
                    products.TotalCount,
                    products.PageSize,
                    products.CurrentPage,
                    products.TotalPages,
                    products.HasNext,
                    products.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null) return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductAddDto model)
        {
            try
            {
                var result = await _productRepository.AddAsync(model);
                if (result == 0) return BadRequest("Server error.");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateDto model)
        {
            try
            {
                var result = await _productRepository.UpdateAsync(model);
                if (result == 0) return BadRequest("Server error.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add_stock")]
        public async Task<IActionResult> AddStock([FromBody] ProductAddStockVm model)
        {
            try
            {
                var result = await _productRepository.AddStockAsync(model);
                if (result == 0) return BadRequest("Server error.");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _productRepository.DeleteAsync(id);
                if (result == 0) return BadRequest("Server error.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
