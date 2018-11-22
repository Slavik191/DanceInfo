using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DanceInfo.Controllers
{
    public class CommentsController : Controller
    {
        [HttpPost]
        public bool AddComment(string comment, string id)
        {
            using (new Sitecore.SecurityModel.SecurityDisabler())
            {            
                Database master = Database.GetDatabase("master");              
                TemplateItem template = master.GetItem("/sitecore/templates/DanceInfo/Comment");
                Item parentItem = master.GetItem(new ID(id));
                Item newItem = parentItem.Add("Comment_" + Sitecore.DateUtil.IsoNow, template);

                newItem.Editing.BeginEdit();
                try
                {
                    newItem.Fields["Author"].Value = "Author";
                    newItem.Fields["Text"].Value = comment;
                    newItem.Fields["Date"].Value = DateTime.Now.ToString();
                    newItem.Editing.EndEdit();
                    PublishItem(newItem);
                    return true;
                }
                catch (System.Exception ex)
                {
                    newItem.Editing.CancelEdit();
                    return false;
                }
            }
        }

        private void PublishItem(Item item)
        {
            Sitecore.Publishing.PublishOptions publishOptions =
              new Sitecore.Publishing.PublishOptions(item.Database,
                                                     Database.GetDatabase("web"),
                                                     Sitecore.Publishing.PublishMode.SingleItem,
                                                     item.Language,
                                                     System.DateTime.Now);  
            Sitecore.Publishing.Publisher publisher = new Sitecore.Publishing.Publisher(publishOptions);
            publisher.Options.RootItem = item;
            publisher.Options.Deep = true;
            publisher.Publish();
        }
    }
}