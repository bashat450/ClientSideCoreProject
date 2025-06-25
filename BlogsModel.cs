using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoEnterprises.DTOs
{
    public class BlogsModel
    {
        public int BlogId { get; set; }  // Always required
        public string? Title { get; set; }
        public string? Category { get; set; }
        public DateTime? PublishDate { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? ImageUrl { get; set; }
    }
}
