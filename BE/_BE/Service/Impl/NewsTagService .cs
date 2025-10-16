using _BE.Models;
using _BE.Repositories;
using _BE.Repositories.Impl;
using _BE.Repositories.Interface;
using _BE.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _BE.Service.Impl
{
    public class NewsTagService : INewsTagService
    {
        private readonly IGenericRepository<NewsArticle> _newsRepo;
        private readonly IGenericRepository<Tag> _tagRepo;

        public NewsTagService(INewsArticleRepository newsRepo, IGenericRepository<Tag> tagRepo)
        {
            _newsRepo = newsRepo;
            _tagRepo = tagRepo;
        }


        public List<int> GetTagsByArticle(string newsArticleId)
        {
            var article = _newsRepo.GetById(newsArticleId);
            if (article == null) throw new Exception("Article not found");

            return article.Tags.Select(t => t.TagId).ToList();
        }

        public void UpdateTags(string newsArticleId, List<int> newTagIds)
        {
            var article = _newsRepo.GetById(newsArticleId);
            if (article == null) throw new Exception("Article not found");

            newTagIds = newTagIds.Distinct().ToList();

            // Xóa những tag không còn trong danh sách mới
            var tagsToRemove = article.Tags
                .Where(t => !newTagIds.Contains(t.TagId))
                .ToList();
            foreach (var t in tagsToRemove)
            {
                article.Tags.Remove(t);
            }

            // Thêm những tag mới chưa có
            var existingTagIds = article.Tags.Select(t => t.TagId).ToList();
            var tagsToAddIds = newTagIds
                .Where(tid => !existingTagIds.Contains(tid))
                .ToList();

            foreach (var tagId in tagsToAddIds)
            {
                var tag = _tagRepo.GetById(tagId);
                if (tag != null)
                {
                    article.Tags.Add(tag);
                }
            }

            _newsRepo.Update(article);
            _newsRepo.Save();
        }




    }
}


