using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;

namespace DanceInfo.ContentSearch.SearchTypes
{
    public class ProductSearchResultItem: SearchResultItem
    {
        [IndexField("title")]
        public virtual string Title { get; set; }

        [IndexField("description")]
        public virtual string Description { get; set; }

        [IndexField("category")]
        public virtual string Category { get; set; }

        [IndexField("tags")]
        public virtual string[] Tags { get; set; }

        [IndexField("urlItem")]
        public virtual string UrlItem { get; set; }

        [IndexField("urlImg")]
        public virtual string UrlImg { get; set; }
    }
}