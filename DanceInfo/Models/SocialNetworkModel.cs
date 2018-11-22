using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanceInfo.Models
{
    public class SocialNetworkModel
    {
        public  string LinkImg { get; set; }
        public  string Link { get; set; }

        public void NewValue(Item getPost)
        {
            Link = getPost.Fields["Link"].Value;
            LinkImg = MediaManager.GetMediaUrl(((ImageField)getPost.Fields["Image"]).MediaItem);
        }
    }

}