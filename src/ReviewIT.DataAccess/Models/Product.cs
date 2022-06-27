using System;

namespace ReviewIT.DataAccess
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public DateTime ReleaseDate { get; set; }

        public double AverageRating { get; set; }
        public Brand Brand { get; set; }

        public int BrandId{get; set;}

        public bool IsArchived { get; set; }
    }
}