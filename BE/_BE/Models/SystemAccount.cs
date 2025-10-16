using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _BE.Models
{
    public partial class SystemAccount
    {
        public short AccountId { get; set; }

        [Required(ErrorMessage = "AccountName is required")]
        public string AccountName { get; set; } = null!;

        [Required(ErrorMessage = "AccountEmail is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string AccountEmail { get; set; } = null!;

        [Required(ErrorMessage = "AccountRole is required")]
        public int AccountRole { get; set; }

        [Required(ErrorMessage = "AccountPassword is required")]
        public string AccountPassword { get; set; } = null!;

        public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
    }
}
