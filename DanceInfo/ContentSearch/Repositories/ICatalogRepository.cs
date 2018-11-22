using DanceInfo.ContentSearch.Queries;
using DanceInfo.ContentSearch.SearchTypes;
using Sitecore.ContentSearch.Linq;

namespace DanceInfo.ContentSearch.Repositories
{
    public interface ICatalogRepository
    {
        SearchResults<ProductSearchResultItem> Get(CatalogQueryArgs args);
    }
}
