using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlCatalog.DataAccess;
using UrlCatalog.Models;

namespace UrlCatalog.Services
{
    public class CatalogService
    {
        private readonly CatalogDataAccess _dataAccess;

        public CatalogService()
        {
            _dataAccess = new CatalogDataAccess();
        }

        public IEnumerable<BlogPost> GetBlogPosts()
        {
            return _dataAccess.GetBlogPosts();
        }
    }
}
