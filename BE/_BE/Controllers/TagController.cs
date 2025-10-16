using _BE.Models;
using _BE.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        // GET: api/Tag
        [HttpGet]
        public IActionResult Get([FromQuery] string? tagName)
        {
            var tags = _tagService.GetTags(tagName);
            return Ok(new { StatusCode = 200, Message = "Success", Data = tags });
        }

        // GET: api/Tag/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var tag = _tagService.GetTagById(id);
                return Ok(new { StatusCode = 200, Message = "Success", Data = tag });
            }
            catch (Exception ex)
            {
                return NotFound(new { StatusCode = 404, Message = ex.Message });
            }
        }

        // POST: api/Tag
        [HttpPost]
        [Authorize(Roles = "1,999")]
        public IActionResult Create([FromBody] Tag tag)
        {
            try
            {
                var created = _tagService.CreateTag(tag);
                return Ok(new { StatusCode = 200, Message = "Tag created", Data = created });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
        }

        // PUT: api/Tag/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "1,999")]
        public IActionResult Update(int id, [FromBody] Tag tag)
        {
            try
            {
                var updated = _tagService.UpdateTag(id, tag);
                return Ok(new { StatusCode = 200, Message = "Tag updated", Data = updated });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
        }

        // DELETE: api/Tag/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "1,999")]
        public IActionResult Delete(int id)
        {
            try
            {
                _tagService.DeleteTag(id);
                return Ok(new { StatusCode = 200, Message = "Tag deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
        }

        // GET: api/Tag/{id}/Articles
        [HttpGet("{id}/Articles")]
        public IActionResult GetArticles(int id)
        {
            try
            {
                var articles = _tagService.GetArticlesByTag(id);
                return Ok(new { StatusCode = 200, Message = "Success", Data = articles });
            }
            catch (Exception ex)
            {
                return NotFound(new { StatusCode = 404, Message = ex.Message });
            }
        }
    }
}
