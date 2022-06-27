using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using ReviewIT.DataAccess;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace ReviewIT
{
    public class DataService
    {
         private readonly DataContext dataContext;
        public DataService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public List<Product> GetProducts()
        {
            return dataContext.Products
            .AsNoTracking()
            .ToList();
        }

        public Product GetProduct(int id)
        {
            return dataContext.Products
            .AsNoTracking()
            .FirstOrDefault(x => x.ProductId == id);
        }

        public List<Post> GetPosts()
        {
            return dataContext.Posts
            .AsNoTracking()
            .Where(x => !x.IsArchived)
            .Include(x=>x.CreatorUser)
            .ToList();
        }

        public List<Post> GetAllPosts()
        {
            return dataContext.Posts
            .AsNoTracking()
            .ToList();
        }

        public Post GetPost(int id)
        {
            return dataContext.Posts
            .AsNoTracking()
            .FirstOrDefault(x => x.PostId == id);
        }

        public List<PostVote> GetPostVotes()
        {
            return dataContext.PostVotes
            .AsNoTracking()
            .ToList();
        }

        public Post AddPost(Post post)
        {
            dataContext.Posts.Add(post);
            dataContext.SaveChanges();

            return post;
        }

        public List<Comment> GetComments()
        {
            return dataContext.Comments
            .AsNoTracking()
            .Include(x=>x.CreatorUser)
            .OrderBy(x=>x.CreationDate)
            .ToList();
        }
        
        public Comment GetComment(int id)
        {
            return dataContext.Comments
            .AsNoTracking()
            .FirstOrDefault(x => x.CommentId == id);
        }

        public List<CommentVote> GetCommentVotes()
        {
            return dataContext.CommentVotes
            .AsNoTracking()
            .ToList();
        }

        public Comment AddComment(Comment comment)
        {
            dataContext.Comments.Add(comment);
            dataContext.SaveChanges();

            return comment;
        }

        // This is used to find a user with any kinds of input(email, id, or nickname)
        public User GetUser(string target)
        {
            User result = dataContext.Users
                .AsNoTracking()
                .Include(x => x.Posts)
                .Include(x => x.Comments)
                .FirstOrDefault(x => x.EmailAddress == target);
            if (result == null)
            {
                result = dataContext.Users
                    .AsNoTracking()
                    .Include(x => x.Posts)
                    .Include(x => x.Comments)
                    .FirstOrDefault(x => x.UserId == target);
            }
            if (result == null)
            {           
                result = dataContext.Users
                    .AsNoTracking()
                    .Include(x => x.Posts)
                    .Include(x => x.Comments)
                    .FirstOrDefault(x => x.NickName == target);
            }


            
            return result;
        }

        // This is used to find a user when loggin in(email address or id)
        public User GetLoginUser(string target)
        {
            User result = dataContext.Users
                .AsNoTracking()
                .FirstOrDefault(x => x.EmailAddress == target);
            if (result == null)
            {
                result = dataContext.Users
                    .AsNoTracking()
                    .FirstOrDefault(x => x.UserId == target);
            }
            
            return result;
        }

        public User AddUser(User user)
        {
            dataContext.Users.Add(user);
            dataContext.SaveChanges();

            return user;
        }

        public void UpdateComment(Comment comment)
        {
            comment.CreatorUser = null;
            dataContext.Comments.Update(comment);
            dataContext.SaveChanges();
        }

        public void UpdatePost(Post post)
        {
            post.CreatorUser = null;
            dataContext.Posts.Update(post);
            dataContext.SaveChanges();
        }
        public void DeletePost(Post post)
        {
            //soft delete
            post.IsArchived = true;
            dataContext.Posts.Update(post);
            dataContext.SaveChanges();
        }

        public List<Post> GetDeletedPosts()
        {
            return dataContext.Posts
                .AsNoTracking()
                .Where(p => p.IsArchived)
                .ToList();
        }

        public void RestorePost(int postId)
        {
            Post post = dataContext.Posts
                .Where(x => x.PostId == postId)
                .FirstOrDefault();
            //undo the soft delete
            post.IsArchived = false;

            dataContext.SaveChanges();
        }

        public void DeleteComment(int commentId)
        {
            Comment comment = dataContext.Comments.FirstOrDefault(x=>x.CommentId == commentId);

            comment.IsArchived = true;

            dataContext.SaveChanges();
        }

        public List<Product> GetProductsByBrand(int id)
        {
            return dataContext.Products
                .AsNoTracking()
                .Where(x => x.BrandId == id && !x.IsArchived)
                .ToList();
        }

        public List<Post> GetPostByProduct(int id)
        {
            return dataContext.Posts
                .AsNoTracking()
                .Where(x=>x.ProductId == id && !x.IsArchived)
                .ToList();
        }

        public List<Post> GetPostByBrand(int brandid)
        {
            return dataContext.Posts.Include(x=> x.Product).Where(x => x.Product.BrandId == brandid).ToList();
        }

        public PostVote AddPostVote(PostVote pv)
        {
            dataContext.PostVotes.Add(pv);
            dataContext.SaveChanges();

            return pv;
        }

        public List<Brand> GetBrands()
        {
            return dataContext.Brands
                .AsNoTracking()
                .ToList();
        }

        public PostVote GetPostVote(string userId, int postId)
        {
            return dataContext.PostVotes
                .AsNoTracking()
                .Where(pv => pv.UserId == userId && pv.PostId == postId)
                .FirstOrDefault();
        }

        public void UpdatePostVote(PostVote pv)
        {
            dataContext.PostVotes.Update(pv);
            dataContext.SaveChanges();
        }

        public void DeletePostVote(string userId, int postId)
        {
            PostVote pv = dataContext.PostVotes
                .FirstOrDefault(x=>x.UserId == userId && x.PostId == postId);

            dataContext.PostVotes.Remove(pv);
            dataContext.SaveChanges();
        }


        public CommentVote AddCommentVote(CommentVote cv)
        {
            dataContext.CommentVotes.Add(cv);
            dataContext.SaveChanges();

            return cv;
        }

        public CommentVote GetCommentVote(string userId, int commentId)
        {
            return dataContext.CommentVotes
                .AsNoTracking()
                .Where(cv => cv.UserId == userId && cv.CommentId == commentId)
                .FirstOrDefault();
        }

        public void UpdateCommentVote(CommentVote cv)
        {
            dataContext.CommentVotes.Update(cv);
            dataContext.SaveChanges();

            
        }

        public void DeleteCommentVote(string userId, int commentId)
        {
            CommentVote cv = dataContext.CommentVotes
                .Where(cv => cv.UserId == userId && cv.CommentId == commentId)
                .FirstOrDefault();

            dataContext.CommentVotes.Remove(cv);
            dataContext.SaveChanges();
        }

        public void UpdateUser(User u)
        {
        dataContext.Users.Update(u);
        dataContext.SaveChanges();
        }
    }
}