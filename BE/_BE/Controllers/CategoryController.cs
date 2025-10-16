using _BE.Models;
using _BE.Models.Responses;
using _BE.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetCategories(string? name, string? description)
        {
            var categories = _categoryService.GetCategories(name, description);
            return Ok(new APIResponse<IEnumerable<CategoryWithCount>>
            {
                StatusCode = 200,
                Message = "Categories retrieved successfully",
                Data = categories
            });
        }

        [HttpPost]
        [Authorize(Roles = "1,999")]
        public IActionResult CreateCategory(Category category)
        {
            try
            {
                var created = _categoryService.CreateCategory(category);
                return Ok(new APIResponse<Category>
                {
                    StatusCode = 201,
                    Message = "Category created successfully",
                    Data = created
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse<string>
                {
                    StatusCode = 400,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1,999")]
        public IActionResult UpdateCategory(short id, Category category)
        {
            try
            {
                var updated = _categoryService.UpdateCategory(id, category);
                return Ok(new APIResponse<Category>
                {
                    StatusCode = 200,
                    Message = "Category updated successfully",
                    Data = updated
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse<string>
                {
                    StatusCode = 400,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "1,999")]
        public IActionResult DeleteCategory(short id)
        {
            try
            {
                _categoryService.DeleteCategory(id);
                return Ok(new APIResponse<string>
                {
                    StatusCode = 200,
                    Message = "Category deleted successfully",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse<string>
                {
                    StatusCode = 400,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPatch("{id}/toggle")]
        [Authorize(Roles = "1,999")]
        public IActionResult ToggleCategoryVisibility(short id, [FromQuery] bool isActive)
        {
            try
            {
                _categoryService.ToggleCategoryVisibility(id, isActive);
                return Ok(new APIResponse<string>
                {
                    StatusCode = 200,
                    Message = "Category visibility updated",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse<string>
                {
                    StatusCode = 400,
                    Message = ex.Message,
                    Data = null
                });
            }
        }
    }

}
