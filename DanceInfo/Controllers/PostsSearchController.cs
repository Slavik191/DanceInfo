using DanceInfo.ContentSearch.Queries;
using DanceInfo.ContentSearch.Repositories;
using DanceInfo.Models;
using Sitecore.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DanceInfo.Controllers
{
    public class PostsSearchController : Controller
    {
        private const string Query = "query";
        private const string Tags = "tags";
        private const string Category = "category";
        private const string Page = "page";
        private const string PageSize = "size";

        private readonly ICatalogRepository _repository;

        public PostsSearchController(ICatalogRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Products()
        {
            var query = WebUtil.GetQueryString(Query);
            var category = WebUtil.GetQueryString(Category);
            var tag = WebUtil.GetQueryString(Tags);
            var page = WebUtil.GetQueryString(Page);
            var size = WebUtil.GetQueryString(PageSize);

            int.TryParse(page, out var pageInt);
            int.TryParse(size, out var sizeInt);

            var args = new CatalogQueryArgs
            {
                Query = query,
                Category = string.IsNullOrEmpty(category) ? new List<String>() : new List<String>() { category },
                Tag = string.IsNullOrEmpty(tag) ? new List<String>() : new List<String>() { tag },
                Page = pageInt == 0 ? 1 : pageInt,
                Size = sizeInt == 0 ? 5 : sizeInt
            };

            SearchViewModel model = GetPosts(args);

            return View("~/Views/Renderings/ListPosts.cshtml", model);
        }

        [HttpPost]
        public ActionResult GetNewPosts(int page, string query, List<String> categories, List<String> tags)
        {
            var args = new CatalogQueryArgs
            {
                Query = query,
                Category = categories,
                Page = page == 0 ? 1 : page,
                Tag = tags,
                Size = 5 
            };

            SearchViewModel model = GetPosts(args);

       

            return PartialView("~/Views/Renderings/SearchResult.cshtml", model);
        }

        private SearchViewModel GetPosts(CatalogQueryArgs args)
        {
            var results = _repository.Get(args);

            var model = new SearchViewModel
            {
                Products = results.Select(x => new PostsViewModel
                {
                    Title = x.Document.Title,
                    Description = x.Document.Description.Length > 91 ? x.Document.Description.Substring(0, 90).Trim() + "..." : x.Document.Description,
                    UrlImg = x.Document.UrlImg,
                    UrlItem = x.Document.UrlItem             
                }).ToList()
            };


            var categoryFacets = results.Facets.Categories.FirstOrDefault(x => x.Name == "category");
            if (categoryFacets != null)
            {
                model.Categories = categoryFacets.Values.Select(x => new FacetViewModel
                {
                    Title = x.Name,
                    Count = x.AggregateCount
                }).ToList();
            }

            var tagsFacets = results.Facets.Categories.FirstOrDefault(x => x.Name == "tags");
            if (tagsFacets != null)
            {
                model.Tags = tagsFacets.Values.Select(x => new FacetViewModel
                {
                    Title = x.Name,
                    Count = x.AggregateCount
                }).ToList();
            }

            model.QuantityPage = (int)Math.Ceiling((double)results.TotalSearchResults / args.Size);
            model.Page = args.Page;
            return model;
        }
    }
}