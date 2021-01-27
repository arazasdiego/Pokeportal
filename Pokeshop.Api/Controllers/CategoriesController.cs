using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pokeshop.Api.Dtos.CategoryDtos;
using Pokeshop.Api.Parameters;
using Pokeshop.Api.Repositories;
using System;
using System.Threading.Tasks;

namespace Pokeshop.Api.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                if (categories == null) return NotFound();

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetByPage([FromQuery] CategoryParameter parameter)
        {
            try
            {
                var categories = await _categoryRepository.GetPageAsync(parameter.PageNumber, parameter.PageSize);
                if (categories == null) return NotFound();

                var metadata = new
                {
                    categories.TotalCount,
                    categories.PageSize,
                    categories.CurrentPage,
                    categories.TotalPages,
                    categories.HasNext,
                    categories.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(categories);
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
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null) return NotFound();

                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto model)
        {
            try
            {
                var result = await _categoryRepository.AddAsync(model);
                if (result == 0) return BadRequest("Server error.");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateDto model)
        {
            try
            {
                var result = await _categoryRepository.UpdateAsync(model);
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
                var result = await _categoryRepository.DeleteAsync(id);
                if (result == 0) return BadRequest("Server error.");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
