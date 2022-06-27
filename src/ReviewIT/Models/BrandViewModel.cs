using System.Collections.Generic;
using ReviewIT.DataAccess;

namespace ReviewIT
{
    public class BrandViewModel
    {
        public Brand Brand{get; set;}

        public List<Brand> Brands{get; set;}

        public PostViewModel PostViewModel{get; set;}
    }
}