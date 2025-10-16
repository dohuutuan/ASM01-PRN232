using _BE.Models;
using _BE.Service.Impl;

namespace _BE.Service.Interface
{
    public interface INewsArticleService
    {
        IQueryable<NewsArticleDto> GetArticles(); 
        NewsArticle CreateArticle(NewsArticle article, short currentUserId, List<int>? tagIds);
        NewsArticle UpdateArticle(string id, NewsArticle article, short currentUserId, List<int>? tagIds);
        void DeleteArticle(string id);
        NewsArticle DuplicateArticle(string id, short currentUserId);

    }
}
