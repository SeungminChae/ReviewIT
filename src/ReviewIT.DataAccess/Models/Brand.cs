using System.Collections.Generic;

namespace ReviewIT.DataAccess
{
    public class Brand
    {
        public int BrandId{get; set;}

        public string BrandName{get; set;}

        public List<Product> Products{get ; set ;}

        public string Description{get; set;}

        public string ImagePath { get; set; }



    }
}