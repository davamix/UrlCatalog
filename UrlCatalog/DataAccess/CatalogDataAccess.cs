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
    internal class BlogPostTable
    {
        public string DbName => "UrlCatalogDB";
        public string TableName => "BlogPost";
        public Dictionary<string, string> Columns => new Dictionary<string, string>
        {
            {"Id", "INTERGER PRIMARY KEY NOT NULL"},
            {"Title", "TEXT"},
            {"Published_Date", "TEXT"},
            {"Url", "TEXT"},
            {"Comments", "TEXT"},
            {"Read_Date", "TEXT"}
        };
    }

    public class CatalogDataAccess
    {
        private string _connectionString;
        private readonly BlogPostTable _blogPostTable;

        public IDbConnection Connection => new SqliteConnection(_connectionString);

        public CatalogDataAccess()
        {
            _blogPostTable = new BlogPostTable();

            _connectionString = $"Data Source={_blogPostTable.DbName}";
            CreateDataTables();

            //AddTestData();
        }

        private void CreateDataTables()
        {
            var columns = _blogPostTable.Columns.Select(x => x.Key + " " + x.Value).Aggregate((c, v) => c + ", " + v);
            var query = $"CREATE TABLE IF NOT EXISTS {_blogPostTable.TableName}({columns})";

            using (IDbConnection connection = Connection)
            {
                connection.Open();
                connection.Execute(query);
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
                var query = $"INSERT INTO {_blogPostTable.TableName}(Title, Published_Date) VALUE(@Title, @PublishedDate)";

                RunQuery(connection, query, blogPost);
            }
        }

        public void Update(BlogPost blogPost)
        {
            using (IDbConnection connection = Connection)
            {
                var query = $"UPDATE {_blogPostTable.TableName} SET Title = @Title, Published_Date = @PublishedDate WHERE id = @Id";

                RunQuery(connection, query, blogPost);
            }
        }

        public void Delete(BlogPost blogPost)
        {
            using (IDbConnection connection = Connection)
            {
                var query = $"DELETE {_blogPostTable.TableName} WHERE id = @Id";

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
                var query = $"INSERT INTO {_blogPostTable.DbName}(Title, Published_Date) Values(@Title, @PublishedDate)";
                connection.Open();

                connection.Execute(query, bp1);
                connection.Execute(query, bp2);
            }
        }
        #endregion
    }
}
