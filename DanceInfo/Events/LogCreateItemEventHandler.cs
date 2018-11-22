using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Events;
using Sitecore.Links;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DanceInfo.Events
{
    public class LogCreateItemEventHandler
    {
        private void SaveToFile(Item item)
        {
            var options = LinkManager.GetDefaultUrlOptions();
            options.AlwaysIncludeServerUrl = true;

            var resultString = String.Format("Author Name: {0}, Post Title: {1}, Link to the project: {2}, Created Date: {3};",
            item.Statistics.UpdatedBy, item.Fields["Name"].Value, LinkManager.GetItemUrl(item, options), DateTime.Now.ToString());
            try
            {
                File.AppendAllText(Sitecore.IO.FileUtil.MapPath("~/App_Data/log.txt"), resultString + Environment.NewLine);
            }
            catch (Exception e)
            {
                Sitecore.Diagnostics.Log.Info(e.Message, this);
            }
        }

        protected void OnCreateNewProject(object sender, EventArgs args)
        {
            if ((args != null))
            {
                Item item = Event.ExtractParameter<Item>(args, 0);
                if (item != null && item.Database.Name.Equals("master") &&
                item.TemplateID.Equals(new ID("{2815263A-4FD8-4F25-8207-6B7556698E1E}")))
                {
                    SaveToFile(item);
                }
            }
        }
    }
}