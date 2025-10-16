using _BE.Models;
using _BE.Models.Responses;
using _BE.Service.Impl;
using _BE.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Security.Claims;

namespace _BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsArticleService _newsService;

        public NewsController(INewsArticleService newsService)
        {
            _newsService = newsService;
        }

        // GET: api/News
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "1,999")]
        public IQueryable<NewsArticleDto> Get()
        {
            return _newsService.GetArticles();
        }
        [HttpGet("Public")]
        [EnableQuery]
        public IQueryable<NewsArticleDto> GetPublic()
        {
            return _newsService.GetArticles()
                .Where(a => a.NewsStatus == true);
        }


        // GET: api/News/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var article = _newsService.GetArticles().FirstOrDefault(a => a.NewsArticleId == id);
            if (article == null)
                return NotFound(new APIResponse<string>(404, "Article not found"));

            return Ok(new APIResponse<NewsArticleDto>(200, "Success", article));
        }

        // POST: api/News
        [HttpPost]
        [Authorize(Roles = "1,999")]
        public IActionResult Create([FromBody] NewsArticleCreateRequest request)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                    return Unauthorized(new APIResponse<string>(401, "User not authenticated"));

                int currentUserId = int.Parse(userIdClaim);

                var article = new NewsArticle
                {
                    NewsTitle = request.NewsTitle,
                    Headline = request.Headline,
                    NewsContent = request.NewsContent,
                    NewsSource = request.NewsSource,
                    CategoryId = request.CategoryId,
                    NewsStatus = request.NewsStatus
                };

                var created = _newsService.CreateArticle(article, (short)currentUserId, request.TagIds);
                return Ok(new APIResponse<NewsArticle>(200, "Article created", created));
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse<string>(400, ex.Message));
            }
        }


        // PUT: api/News/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "1,999")]
        public IActionResult Update(string id, [FromBody] NewsArticleUpdateRequest request)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                    return Unauthorized(new APIResponse<string>(401, "User not authenticated"));

                int currentUserId = int.Parse(userIdClaim); 

                var article = new NewsArticle
                {
                    NewsTitle = request.NewsTitle,
                    Headline = request.Headline,
                    NewsContent = request.NewsContent,
                    NewsSource = request.NewsSource,
                    CategoryId = request.CategoryId,
                    NewsStatus = request.NewsStatus
                };

                var updated = _newsService.UpdateArticle(id, article, (short)currentUserId, request.TagIds);
                return Ok(new APIResponse<NewsArticle>(200, "Article updated", updated));
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse<string>(400, ex.Message));
            }
        }

        // DELETE: api/News/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "1,999")]
        public IActionResult Delete(string id)
        {
            try
            {
                _newsService.DeleteArticle(id);
                return Ok(new APIResponse<string>(200, "Article deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse<string>(400, ex.Message));
            }
        }

        // POST: api/News/Duplicate/{id}
        [HttpPost("Duplicate/{id}")]
        [Authorize(Roles = "1,999")]
        public IActionResult Duplicate(string id)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                    return Unauthorized(new APIResponse<string>(401, "User not authenticated"));

                int currentUserId = int.Parse(userIdClaim);
                var duplicated = _newsService.DuplicateArticle(id, (short)currentUserId);
                return Ok(new APIResponse<NewsArticle>(200, "Article duplicated", duplicated));
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse<string>(400, ex.Message));
            }
        }

    }

    // DTO Request cho Create
    public class NewsArticleCreateRequest
    {
        public string NewsTitle { get; set; }
        public string Headline { get; set; }
        public string NewsContent { get; set; }
        public string NewsSource { get; set; }
        public short CategoryId { get; set; }
        public bool NewsStatus { get; set; }
        public short CurrentUserId { get; set; }
        public List<int>? TagIds { get; set; }
    }

    // DTO Request cho Update
    public class NewsArticleUpdateRequest
    {
        public string NewsTitle { get; set; }
        public string Headline { get; set; }
        public string NewsContent { get; set; }
        public string NewsSource { get; set; }
        public short CategoryId { get; set; }
        public bool NewsStatus { get; set; }
        public short CurrentUserId { get; set; }
        public List<int>? TagIds { get; set; }
    }
}

