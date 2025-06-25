using DynamoEnterprises.DTOs;
using System.Collections.Generic;

namespace DynamoEnterprises.Repository
{
    public interface IBlogsRepository
    {
        IEnumerable<BlogsModel> GetAllBlogs();
        BlogsModel GetBlogById(int id);
    }
}
