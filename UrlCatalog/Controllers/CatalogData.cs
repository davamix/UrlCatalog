using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlCatalog.Models;
using UrlCatalog.Services;

namespace UrlCatalog.Controllers
{
    [Route("api/[controller]")]
    public class CatalogData : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<BlogPost> BlogPosts()
        {
            var service = new CatalogService();

            return service.GetBlogPosts();
        }

        [HttpPost("[action]")]
        public void Add(BlogPost blogPost)
        {
            var service = new CatalogService();

            service.Save(blogPost);
        }

        [HttpPut("[action]")]
        public void Update(BlogPost blogPost)
        {
            var service = new CatalogService();

            service.Save(blogPost);
        }

        public void Delete(BlogPost blogPost)
        {
            var service = new CatalogService();

            service.Delete(blogPost);
        }
    }
}
