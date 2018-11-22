using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Fields;
using Sitecore.Diagnostics;
using Sitecore.Resources.Media;

namespace DanceInfo.ContentSearch.Fields
{
    public class UrlImgComputedField : IComputedIndexField
    {
        public string FieldName { get; set; }
        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            Assert.ArgumentNotNull(indexable, nameof(indexable));

            if (indexable is SitecoreIndexableItem indexableItem)
            {
                string imageUrl = MediaManager.GetMediaUrl(((ImageField)indexableItem.Item.Fields["Images"]).MediaItem);
                return imageUrl; 
            }
            return null;

        }
    }
}