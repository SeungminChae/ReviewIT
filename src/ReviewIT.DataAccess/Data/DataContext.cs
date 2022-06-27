using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;

namespace ReviewIT.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set;}

        public DbSet<PostVote> PostVotes { get; set; }

        public DbSet<CommentVote> CommentVotes { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Brand> Brands { get; set;}


        // the code to retrieve data from csv file is copied from
        // https://stackoverflow.com/questions/6542996/how-to-split-csv-whose-columns-may-contain
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>()
            .HasOne(x=>x.CreatorUser)
            .WithMany(x=>x.Comments)
            .HasForeignKey(x=>x.CreatorUserId);
            
            modelBuilder.Entity<Comment>()
            .HasOne(x => x.Post)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.PostId);

            modelBuilder.Entity<Post>()
            .HasOne(x => x.CreatorUser)
            .WithMany(x => x.Posts)
            .HasForeignKey(x => x.CreatorUserId);

            modelBuilder.Entity<CommentVote>()
            .HasOne(x => x.Comment)
            .WithMany(x => x.CommentVotes)
            .HasForeignKey(x => x.CommentId);

            modelBuilder.Entity<CommentVote>()
            .HasOne(x => x.User)
            .WithMany(x => x.CommentVotes)
            .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<PostVote>()
            .HasOne(x => x.Post)
            .WithMany(x => x.PostVotes)
            .HasForeignKey(x => x.PostId);

            modelBuilder.Entity<PostVote>()
            .HasOne(x => x.User)
            .WithMany(x => x.PostVotes)
            .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Product>()
            .HasOne(x => x.Brand)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.BrandId);

            modelBuilder.Entity<Product>().HasData(
            new Product{ ProductId = 1, Name = "iPhone 12 Pro Max", Description = "This is fluff for now", Price = 1249.99m, ImagePath = "/iphone12max&pro.jpg", BrandId = 1 },
            new Product{ ProductId = 2, Name = "iPhone 12 Pro", Description = "This is fluff for now", Price = 1149.99m, ImagePath = "/iphone12max&pro.jpg" , BrandId = 1},
            new Product{ ProductId = 3, Name = "iPhone 12", Description = "This is fluff for now", Price = 1049.99m, ImagePath = "/iphone12&mini.jpg" , BrandId = 1},
            new Product{ ProductId = 4, Name = "iPhone 12 mini", Description = "This is fluff for now", Price = 999.99m, ImagePath = "/iphone12&mini.jpg" , BrandId = 1},
            new Product{ ProductId = 5, Name = "iPhone SE", Description = "This is fluff for now", Price = 999.99m, ImagePath = "/iphone_SE.jpg" , BrandId = 1},
            new Product{ ProductId = 6, Name = "iPhone 11 Pro Max", Description = "This is fluff for now", Price = 999.99m, ImagePath = "/iphone11pro&max.jpg" , BrandId = 1},
            new Product{ ProductId = 7, Name = "iPhone 11 Pro", Description = "This is fluff for now", Price = 849.99m, ImagePath = "/iphone11pro&max.jpg" , BrandId = 1},
            new Product{ ProductId = 8, Name = "iPhone 11", Description = "This is fluff for now", Price = 799.99m, ImagePath = "/iphone11.jpg" , BrandId = 1},
            new Product{ ProductId = 9, Name = "iPhone XR", Description = "This is fluff for now", Price = 749.99m, ImagePath = "/iphoneXR.jpg" , BrandId = 1},
            new Product{ ProductId = 10, Name = "Samsung Galaxy Note 09", Description = "This is fluff for now", Price = 949.99m, ImagePath = "/galaxyNote09.jpg" , BrandId = 2},
            new Product{ ProductId = 11, Name = "Samsung Galaxy Note 10", Description = "This is fluff for now", Price = 1049.99m, ImagePath = "/galaxyNote10.jpg" , BrandId = 2},
            new Product{ ProductId = 12, Name = "Samsung Galaxy Note 20", Description = "This is fluff for now", Price = 1149.99m, ImagePath = "/galaxyNote20.jpg" , BrandId = 2},
            new Product{ ProductId = 13, Name = "Samsung Galaxy S10", Description = "This is fluff for now", Price = 849.99m, ImagePath = "/galaxyS10.jpg" , BrandId = 2},
            new Product{ ProductId = 14, Name = "Samsung Galaxy S20", Description = "This is fluff for now", Price = 1049.99m, ImagePath = "/galaxyS20.jpg" , BrandId = 2},
            new Product{ ProductId = 15, Name = "Samsung Galaxy S21", Description = "This is fluff for now", Price = 1249.99m, ImagePath = "/galaxyS21.jpg" , BrandId = 2},
            new Product{ ProductId = 16, Name = "Pixel 3", Description = "This is fluff for now", Price = 799.99m, ImagePath = "/pixel3.jpg" , BrandId = 3},
            new Product{ ProductId = 17, Name = "Pixel 4", Description = "This is fluff for now", Price = 869.99m, ImagePath = "/pixel4.jpg" , BrandId = 3},
            new Product{ ProductId = 18, Name = "Pixel 5", Description = "This is fluff for now", Price = 929.99m, ImagePath = "/pixel5.jpg" , BrandId = 3}
            );

            modelBuilder.Entity<Brand>().HasData(
            new Brand{BrandId = 1, BrandName = "Apple", Description = "Who is  the superior brand?", ImagePath = "/Brand_Apple.png" },
            new Brand{BrandId = 2, BrandName = "Samsung", Description = "Obviously, I am the superior brand", ImagePath = "/Brand_Samsung.jpg" },
            new Brand{BrandId = 3, BrandName = "Google", Description = "Nope, I am obviously better.", ImagePath = "/Brand_Google.jpg" }
            );


            /* Retrieve comment data */
            TextFieldParser commentParser = new TextFieldParser(@"..\\ReviewIT.DataAccess\\DbMockFiles\\Comment_Mock_Data.csv");
            commentParser.HasFieldsEnclosedInQuotes = true;
            commentParser.SetDelimiters(",");

            // Reads first line to get rid of headers.
            string[] commentfields = commentParser.ReadFields();

            // Loop through the data rows until the end.
            while (!commentParser.EndOfData)
            {
                // takes a line of data.
                commentfields = commentParser.ReadFields();
                
                // assign values and add the data.
                try{ // try parsing string value of parentid to int value.
                    modelBuilder.Entity<Comment>().HasData(new Comment{ CommentId = Int32.Parse(commentfields[0]), PostId = Int32.Parse(commentfields[1]), CreatorUserId = commentfields[2], CommentContent = commentfields[3], CreationDate=DateTime.Parse(commentfields[4]), ParentCommentId = Int32.Parse(commentfields[5]), IsArchived = commentfields[6] == "1"});
                }
                catch{ // if parentId is null, leave the variable with default value.
                    modelBuilder.Entity<Comment>().HasData(new Comment{ CommentId = Int32.Parse(commentfields[0]), PostId = Int32.Parse(commentfields[1]), CreatorUserId = commentfields[2], CommentContent = commentfields[3], CreationDate=DateTime.Parse(commentfields[4]), IsArchived = commentfields[6] == "1"});
                }
            }
            commentParser.Close();
            
            /* Retrieve post data */
            TextFieldParser postParser = new TextFieldParser(@"..\\ReviewIT.DataAccess\\DbMockFiles\\Post_Mock_Data.csv");
            postParser.HasFieldsEnclosedInQuotes = true;
            postParser.SetDelimiters(",");

            // Reads first line to get rid of headers.
            string[] postfields = postParser.ReadFields();

            // Loop through the data rows until the end.
            while (!postParser.EndOfData)
            {
                // takes a line of data.
                postfields = postParser.ReadFields();
                
                // assign values and add the data.
                modelBuilder.Entity<Post>().HasData(new Post{ PostId = Int32.Parse(postfields[0]), Title =postfields[1], ProductId = Int32.Parse(postfields[2]), CreatorUserId = postfields[3], PostContent = postfields[4], RatingForDevice = Int32.Parse(postfields[5]), CreationDate=DateTime.Parse(postfields[6]), IsArchived = postfields[7] == "1"});
            }
            postParser.Close();

            /* Retrieve postvote data */
            TextFieldParser postVoteParser = new TextFieldParser(@"..\\ReviewIT.DataAccess\\DbMockFiles\\PostVote_Mock_Data.csv");
            postVoteParser.HasFieldsEnclosedInQuotes = true;
            postVoteParser.SetDelimiters(",");

            // Reads first line to get rid of headers.
            string[] fields = postVoteParser.ReadFields();

            // Loop through the data rows until the end.
            while (!postVoteParser.EndOfData)
            {
                // takes a line of data.
                fields = postVoteParser.ReadFields();
                
                // assign values and add the data.
                modelBuilder.Entity<PostVote>().HasData(new PostVote{ PostVoteId = Int32.Parse(fields[0]), PostId = Int32.Parse(fields[1]), UserId = fields[2], IsUp = bool.Parse(fields[3]), CreationDate=DateTime.Parse(fields[4]), IsArchived = fields[5] == "1"});
            }
            postVoteParser.Close();

            /* Retrieve commentVote data */
            TextFieldParser commentVoteParser = new TextFieldParser(@"..\\ReviewIT.DataAccess\\DbMockFiles\\CommentVote_Mock_Data.csv");
            commentVoteParser.HasFieldsEnclosedInQuotes = true;
            commentVoteParser.SetDelimiters(",");

            // Reads first line to get rid of headers.
            fields = commentVoteParser.ReadFields();

            // Loop through the data rows until the end.
            while (!commentVoteParser.EndOfData)
            {
                // takes a line of data.
                fields = commentVoteParser.ReadFields();
                
                // assign values and add the data.
                modelBuilder.Entity<CommentVote>().HasData(new CommentVote{ CommentVoteId = Int32.Parse(fields[0]), CommentId = Int32.Parse(fields[1]), UserId = fields[2], IsUp = bool.Parse(fields[3]), CreationDate=DateTime.Parse(fields[4]), IsArchived = fields[5] == "1"});
            }
            commentVoteParser.Close();

            /* Retrieve User data */
            TextFieldParser userParser = new TextFieldParser(@"..\\ReviewIT.DataAccess\\DbMockFiles\\User_Mock_Data.csv");
            userParser.HasFieldsEnclosedInQuotes = true;
            userParser.SetDelimiters(",");

            // Reads first line to get rid of headers.
            fields = userParser.ReadFields();

            // Loop through the data rows until the end.
            while (!userParser.EndOfData)
            {
                // takes a line of data.
                fields = userParser.ReadFields();
                
                // assign values and add the data.
                modelBuilder.Entity<User>().HasData(new User{ UserId = fields[0], PasswordHash = fields[1], EmailAddress = fields[2], NickName = fields[3], CreationDate=DateTime.Parse(fields[4]), IsAdmin = fields[5] == "1", IsArchived = fields[6] == "1"});
            }
            userParser.Close();
            
        }
    }
}