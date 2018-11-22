using DanceInfo.ContentSearch.Queries;
using DanceInfo.ContentSearch.Repositories;
using DanceInfo.Models;
using DanceInfo.Models.Items;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using Sitecore.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DanceInfo.Controllers
{
    public class PostsController : GlassController
    {
        public ActionResult TopPosts()
        {
            Item itemTopPosts = Context.Database.GetItem(new Sitecore.Data.ID(RenderingContext.Current.Rendering.DataSource));
            Item[] getPosts = ((MultilistField)itemTopPosts.Fields["Posts"]).GetItems();
            List<PostViewModel> posts = Posts(getPosts);
            return View("~/Views/Renderings/TopPosts.cshtml", posts);
        }

        public ActionResult ListPosts()
        {
            Item[] getPosts = Context.Database.GetItem(new Sitecore.Data.ID(RenderingContext.Current.Rendering.DataSource)).GetChildren().ToArray();
            Array.Reverse(getPosts);
            List<PostViewModel> posts = Posts(getPosts);            
            return View("~/Views/Renderings/ListPosts.cshtml", posts);
        }

        public ActionResult PostDetails()
        {
            PostViewModel post = new PostViewModel();
            post.NewValue(Context.Item, true);
            return View("~/Views/Renderings/PostDetails.cshtml", post);
        }

        //[HttpPost]
        //public ActionResult PostsFilter(string filter)
        //{
        //    Item[] getPosts = Context.Item.Axes.SelectItems($".//*[@@templatename='Post' and contains(@Title, '{filter}')]");
        //    if(getPosts != null)
        //        Array.Reverse(getPosts);
        //    List<PostViewModel> posts = Posts(getPosts);
        //    return PartialView("~/Views/Renderings/SearchResult.cshtml", posts);
        //}

        private List<PostViewModel> Posts(Item[] getPosts)
        {
            List<PostViewModel> posts = new List<PostViewModel>();
            if (getPosts != null)
            {
                foreach (var getPost in getPosts)
                {
                    PostViewModel post = new PostViewModel();
                    post.NewValue(getPost, false);
                    posts.Add(post);
                }
            }
            else
            {
                posts = null;
            }
            return posts;
        }
    }
}