using System.Diagnostics.Contracts;
using System.Collections.Generic;
using ReviewIT.DataAccess;

namespace ReviewIT
{
    public class IndexViewModel
    {
        public List<Post> Posts { get; set; }

        public List<Product> Products { get; set; }

        public List<Brand> Brands { get; set; }

        public PaginatedList<Post> PL{get; set;}
    }
}