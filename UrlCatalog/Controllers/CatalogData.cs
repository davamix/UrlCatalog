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
    }
}
