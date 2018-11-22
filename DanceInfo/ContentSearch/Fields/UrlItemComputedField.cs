using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;

namespace DanceInfo.ContentSearch.Fields
{
    public class UrlItemComputedField : IComputedIndexField
    {
        public string FieldName { get; set; }
        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            Assert.ArgumentNotNull(indexable, nameof(indexable));

            if (indexable is SitecoreIndexableItem indexableItem)
            {

                //Item item = indexableItem.Item;
                //var urlOptions = LinkManager.GetDefaultUrlOptions();
                //urlOptions.Language = item.Language;
                //urlOptions.AlwaysIncludeServerUrl = false;

                //string itemUrl = LinkManager.GetItemUrl(item, urlOptions);
                //itemUrl = itemUrl.Substring(3);
                //itemUrl = itemUrl.Substring(itemUrl.IndexOf('/'));
                //itemUrl = itemUrl.Replace(item.Language.Name, "");
                //itemUrl = itemUrl.Remove(itemUrl.IndexOf('/'), 2);

                Item item = indexableItem.Item;
                string getItmUrl = Sitecore.Links.LinkManager.GetItemUrl(item);
                string itemUrl = getItmUrl.Substring(getItmUrl.IndexOf("Home") - 1);
                //string itemUrl = Sitecore.Links.LinkManager.GetItemUrl(item);
                return itemUrl;
            }
            return null;

        }
    }
}