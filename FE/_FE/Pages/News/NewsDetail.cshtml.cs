using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _FE.Pages.News
{
    public class NewsDetailModel : PageModel
    {
        public string NewsId { get; set; }

        public void OnGet(string id)
        {
            NewsId = id;
        }
    }
}
