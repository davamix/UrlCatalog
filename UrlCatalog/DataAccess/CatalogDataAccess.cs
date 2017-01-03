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

       

        public IEnumerable<BlogPost> GetBlogPosts()
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();
                return connection.Query<BlogPost>("SELECT Id, Title, Published_Date PublishedDate FROM BlogPost");
            }
        }

        public void Add(BlogPost blogPost)
        {
            using (IDbConnection connection = Connection)
            {
                var query = "INSERT INTO BlogPost(Title, Published_Date) VALUE(@Title, @PublishedDate)";

                RunQuery(connection, query, blogPost);
            }
        }

        public void Update(BlogPost blogPost)
        {
            using (IDbConnection connection = Connection)
            {
                var query = "UPDATE BlogPost SET Title = @Title, Published_Date = @PublishedDate WHERE id = @Id";

                RunQuery(connection, query, blogPost);
            }
        }

        public void Delete(BlogPost blogPost)
        {
            using(IDbConnection connection = Connection)
            {
                var query = "DELETE BlogPost WHERE id = @Id";

                RunQuery(connection, query, blogPost);
            }
        }

        private void RunQuery(IDbConnection connection, string query, BlogPost blogPost)
        {
            connection.Open();
            connection.Execute(query, blogPost);
        }

        #region TEST DATA
        private void AddTestData()
        {
            var bp1 = new BlogPost { Title = "Post test A", PublishedDate = DateTime.UtcNow };
            var bp2 = new BlogPost { Title = "Post test B", PublishedDate = DateTime.UtcNow };

            using (IDbConnection connection = Connection)
            {
                var query = "INSERT INTO BlogPost(Title, Published_Date) Values(@Title, @PublishedDate)";
                connection.Open();

                connection.Execute(query, bp1);
                connection.Execute(query, bp2);
            }
        }
        #endregion
    }
}
