using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Links;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanceInfo.Models
{
    public class PostViewModel
    {
        public string LinkItem { get; set; }
        public string LinkImg { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<Item> Comments { get; set; }

        public void NewValue(Item getPost, bool allText)
        {
            LinkItem = LinkManager.GetItemUrl(getPost);
            LinkImg = MediaManager.GetMediaUrl(((ImageField)getPost.Fields["Images"]).MediaItem);
            Title = getPost.Fields["Title"].ToString();
            Description = getPost.Fields["Description"].ToString().Length > 91 && !allText ? getPost.Fields["Description"].ToString().Substring(0, 90).Trim() + "..." : getPost.Fields["Description"].ToString();
           
            Item[] getComments = getPost.GetChildren().ToArray();

            var filterComments = new List<Item>();
            foreach (Item comment in getComments)
            {
                if (!String.IsNullOrEmpty(comment.Fields["Text"].Value))
                    filterComments.Add(comment);
            }

            Comments = filterComments;

            //Comments = getPost.Axes.SelectItems(".//*[@@templatename='Comment']");
        }
    }
}