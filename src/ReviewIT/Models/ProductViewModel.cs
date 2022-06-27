using System.Collections.Generic;
using ReviewIT.DataAccess;

namespace ReviewIT
{
    public class ProductViewModel
    {
        public Product Product { get; set; }

        public List<Post> Posts { get; set; }
    }
}