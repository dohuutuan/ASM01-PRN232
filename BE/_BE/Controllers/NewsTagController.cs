
using _BE.Models.Responses;
using _BE.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "1,999")]
    public class NewsTagController : ControllerBase
    {
        private readonly INewsTagService _newsTagService;

        public NewsTagController(INewsTagService newsTagService)
        {
            _newsTagService = newsTagService;
        }

        [HttpGet("{newsArticleId}")]
        public IActionResult GetTags(string newsArticleId)
        {
            try
            {
                var tagIds = _newsTagService.GetTagsByArticle(newsArticleId);
                return Ok(new APIResponse<List<int>>(200, "Tags retrieved successfully", tagIds));
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse<string>(400, ex.Message));
            }
        }

        [HttpPut("{newsArticleId}")]
        public IActionResult UpdateTags(string newsArticleId, [FromBody] NewsTagUpdateDto dto)
        {
            if (dto.NewsArticleId != newsArticleId)
            {
                return BadRequest(new APIResponse<string>(400, "Article ID mismatch"));
            }

            try
            {
                _newsTagService.UpdateTags(newsArticleId, dto.TagIds);
                return Ok(new APIResponse<string>(200, "Tags updated successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse<string>(400, ex.Message));
            }
        }
    }
    public class NewsTagUpdateDto
    {
        public string NewsArticleId { get; set; } = null!;
        public List<int> TagIds { get; set; } = new List<int>();
    }
}

