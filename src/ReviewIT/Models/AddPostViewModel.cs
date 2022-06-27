using System.Collections.Generic;
using ReviewIT.DataAccess;

namespace ReviewIT
{
    public class AddPostViewModel
    {
        public Post Post{ get; set; }

        public List<Product> Products { get; set; }

        public User User { get; set; }
    }
}