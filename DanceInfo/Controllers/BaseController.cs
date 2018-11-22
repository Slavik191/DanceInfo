using DanceInfo.Models;
using Glass.Mapper.Sc.IoC;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace DanceInfo.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public ActionResult Header()
        {
            var getListNanigation = Context.Database.GetItem(new Sitecore.Data.ID(RenderingContext.Current.Rendering.DataSource)).GetChildren().ToArray();
            List<Item> listNanigation = new List<Item>();
            foreach (var item in getListNanigation)
            {
                if (item.Fields["Show in header"].ToString() == "1")
                {
                    listNanigation.Add(item);
                }
            }
            return View("~/Views/Renderings/Header.cshtml", listNanigation);
        }

        public ActionResult Footer()
        {
            List<SocialNetworkModel> socialNetworks = null;
            if (!String.IsNullOrEmpty(RenderingContext.Current.Rendering.DataSource))
            {
                Item[] getSocialNetworks = Context.Database.GetItem(new Sitecore.Data.ID(RenderingContext.Current.Rendering.DataSource)).GetChildren().ToArray();

                socialNetworks = new List<SocialNetworkModel>();
                foreach (var item in getSocialNetworks)
                {
                    SocialNetworkModel socialNetwork = new SocialNetworkModel();
                    socialNetwork.NewValue(item);
                    socialNetworks.Add(socialNetwork);
                }

            }
            return View("~/Views/Renderings/Footer.cshtml", socialNetworks);
        }

        public ActionResult HeroBanner()
        {
            Item images = Context.Database.GetItem(new Sitecore.Data.ID(RenderingContext.Current.Rendering.DataSource));
            Item heroImageItem = ((MultilistField)images.Fields["Images"]).GetItems()[0];
            HeroBannerModel model = new HeroBannerModel();
            model.LinkImg = MediaManager.GetMediaUrl(((ImageField)heroImageItem.Fields["Image"]).MediaItem);
            return View("~/Views/Renderings/HeroBanner.cshtml", model);
        }

        public ActionResult Slider()
        {
            //Thread.Sleep(1000);
            //throw new Exception("Slava");
            Item contentItem = null;
            var database = Context.Database;
            if (database != null)
            {
                if (!String.IsNullOrEmpty(RenderingContext.Current.Rendering.DataSource))
                {
                    contentItem = database.GetItem(new Sitecore.Data.ID(RenderingContext.Current.Rendering.DataSource));
                }
            }

            var model = new SliderViewModel();
            var heroImagesField = (MultilistField)contentItem.Fields["Images"];
            if (heroImagesField != null)
            {
                var items = heroImagesField.GetItems();
                var imageItems = items.Select(x => (ImageField)x.Fields["Image"]);
                model.Images = imageItems.Select(x => Sitecore.Resources.Media.MediaManager.GetMediaUrl(x.MediaItem)).ToList();
                model.Speed = System.Convert.ToInt32(Context.Database.GetItem(RenderingContext.CurrentOrNull.Rendering.Parameters["Speed"]).Name) * 1000;
                model.ItemId = RenderingContext.Current.Rendering.DataSource;
            }

            return View("~/Views/Renderings/Slider.cshtml", model);
        }
    }
}