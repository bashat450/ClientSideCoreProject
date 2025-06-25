using DynamoEnterprises.DTOs;
using DynamoEnterprises.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoEnterprises.Repository
{
    public class BlogsRepository : IBlogsRepository
    {
        private readonly IConfiguration _configuration;

        public BlogsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string ConnectionString => _configuration.GetConnectionString("DyanamoEnterprises_DB");

        public IEnumerable<BlogsModel> GetAllBlogs()
        {
            var blogs = new List<BlogsModel>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Dynamo.SP_ManageBlogs", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Action", "GET");

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    // Only include published blogs for client
                    if (dr["Published"] != DBNull.Value && Convert.ToBoolean(dr["Published"]))
                    {
                        blogs.Add(new BlogsModel
                        {
                            BlogId = Convert.ToInt32(dr["BlogId"]),
                            Title = dr["Title"]?.ToString(),
                            Category = dr["Category"]?.ToString(),
                            PublishDate = dr["PublishDate"] != DBNull.Value ? Convert.ToDateTime(dr["PublishDate"]) : (DateTime?)null,
                            Description = dr["Description"]?.ToString(),
                            Author = dr["Author"]?.ToString(),
                            ImageUrl = dr["ImageUrl"]?.ToString()
                        });
                    }
                }
            }

            return blogs;
        }

        public BlogsModel GetBlogById(int id)
        {
            BlogsModel blog = null;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Dynamo.SP_ManageBlogs", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Action", "GETBYID");
                cmd.Parameters.AddWithValue("@BlogId", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["Published"] != DBNull.Value && Convert.ToBoolean(dr["Published"]))
                    {
                        blog = new BlogsModel
                        {
                            BlogId = Convert.ToInt32(dr["BlogId"]),
                            Title = dr["Title"]?.ToString(),
                            Category = dr["Category"]?.ToString(),
                            PublishDate = dr["PublishDate"] != DBNull.Value ? Convert.ToDateTime(dr["PublishDate"]) : (DateTime?)null,
                            Description = dr["Description"]?.ToString(),
                            Author = dr["Author"]?.ToString(),
                            ImageUrl = dr["ImageUrl"]?.ToString()
                        };
                    }
                }
            }

            return blog;
        }
    }
}
