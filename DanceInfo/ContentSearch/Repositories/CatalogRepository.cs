using System.Linq;
using DanceInfo.ContentSearch.Queries;
using DanceInfo.ContentSearch.SearchTypes;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;

namespace DanceInfo.ContentSearch.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly string IndexName = getIndexName();

        private ISearchIndex _index;

        private ISearchIndex Index => _index ?? (_index = ContentSearchManager.GetIndex(IndexName));

        private IProviderSearchContext _context;

        private IProviderSearchContext Context => _context ?? (_context = Index.CreateSearchContext());

        public SearchResults<ProductSearchResultItem> Get(CatalogQueryArgs args)
        {
            var searchPredicate = PredicateBuilder.True<ProductSearchResultItem>();

            if (!string.IsNullOrEmpty(args.Query))
            {
                searchPredicate = searchPredicate.And(x => x.Title.Contains(args.Query) || x.Description.Contains(args.Query));
            }

            if (args.Category != null)
            {
                foreach (string category in args.Category)
                {
                    searchPredicate = searchPredicate.Or(x => x.Category.Equals(category));
                }
            }

            if (args.Tag != null)
            {
                foreach(string tag in args.Tag)
                {
                    searchPredicate = searchPredicate.And(x => x.Tags.Contains(tag));
                }
            }

            var result = Context.GetQueryable<ProductSearchResultItem>()
                .Where(searchPredicate)
                .FacetOn(x => x.Category, 0)
                .FacetOn(x => x.Tags, 0)
                .Page(args.Page - 1, args.Size)
                .GetResults();

            return result; 
        }

        public static string getIndexName()
        {
            switch (Sitecore.Context.Site.Name)
            {
                case "DanceInfo":
                    return $"di_{Sitecore.Context.Database.Name.ToLower()}_index";
                case "DanceInfoV2":
                    return $"div2_{Sitecore.Context.Database.Name.ToLower()}_index";
                default:
                    return null;
            }
        }
    }
}