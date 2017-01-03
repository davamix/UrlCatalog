using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using UrlCatalog.Models;

namespace UrlCatalog.DataAccess
{
    public class CatalogDataAccess
    {
        private string _connectionString;

        public IDbConnection Connection=>new SqliteConnection(_connectionString);

        public CatalogDataAccess()
        {
            _connectionString = @"Data Source=UrlCatalogDB";
            CreateDataTables();

            //AddTestData();
        }

        private void CreateDataTables()
        {
            using (IDbConnection connection = Connection)
            {
                //BlogPost table (Id, Title, Published_Date)
                connection.Execute("CREATE TABLE IF NOT EXISTS BlogPost(Id INTEGER PRIMARY KEY NOT NULL, Title TEXT, Published_Date INTEGER)");
            }
        }

        //private void AddTestData()
        //{
        //    var bp1 = new BlogPost {Title = "Post test A", PublishedDate = DateTime.UtcNow};
        //    var bp2 = new BlogPost { Title = "Post test B", PublishedDate = DateTime.UtcNow };

        //    using (IDbConnection connection = Connection)
        //    {
        //        var query = "INSERT INTO BlogPost(Title, Published_Date) Values(@Title, @PublishedDate)";
        //        connection.Open();

        //        connection.Execute(query, bp1);
        //        connection.Execute(query, bp2);
        //    }
        //}

        public IEnumerable<BlogPost> GetBlogPosts()
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();
                return connection.Query<BlogPost>("SELECT Title, Published_Date PublishedDate FROM BlogPost");
            }
        }
        
    }
}
