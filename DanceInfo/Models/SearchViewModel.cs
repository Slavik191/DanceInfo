using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanceInfo.Models
{
    public class SearchViewModel
    {
        public List<PostsViewModel> Products { get; set; }
        public List<FacetViewModel> Categories { get; set; }
        public List<FacetViewModel> Tags { get; set; }
        public int QuantityPage { get; set; }
        public int Page { get; set; }

        public SearchViewModel()
        {
            Products = new List<PostsViewModel>();
            Categories = new List<FacetViewModel>();
            Tags = new List<FacetViewModel>();
        }
    }

    public class PostsViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlItem { get; set; }
        public string UrlImg { get; set; }
    }

    public class FacetViewModel
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public int Count { get; set; }
    }
}