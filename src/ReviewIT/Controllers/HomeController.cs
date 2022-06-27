using System.ComponentModel.Design;
using System.Reflection.Metadata;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using ReviewIT.DataAccess;
using Microsoft.Extensions.Logging;

namespace ReviewIT
{
    [AllowAnonymous]
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppConfig _appConfig;

        private readonly DataService _dataService;

        public HomeController(ILogger<HomeController> logger, IOptions<AppConfig> appConfig, DataContext dataContext)
        {
            _logger = logger;
            _appConfig= appConfig.Value;
            _dataService = new DataService(dataContext);
        }

        [Route("")]
        public IActionResult Index(string filter, int? pageNumber)
        {
            const int pageSize = 5;

            IndexViewModel ivm = new IndexViewModel();


            //New Code
            PaginatedList<Post> postList = PaginatedList<Post>.Create(_dataService.GetPosts().ToList(), pageNumber ?? 1, pageSize);

            

            // Filters posts by filter parameter and search for keywords in titles and post contents.
            if(!String.IsNullOrEmpty(filter))
            {
                // ivm.Posts = _dataService.GetPosts()
                //     .Where(p => p.Title.ToLower().Contains(filter.ToLower())
                //             || p.PostContent.ToLower().Contains(filter.ToLower()))
                //     .ToList();

                    ivm.Posts = PaginatedList<Post>.Create(_dataService.GetPosts()
                    .Where(p => p.Title.ToLower().Contains(filter.ToLower())
                            || p.PostContent.ToLower().Contains(filter.ToLower()))
                    .ToList(), pageNumber ?? 1, pageSize);
            }
            else{
                // Previous code =>> ivm.Posts = _dataService.GetPosts().ToList();
                ivm.Posts = postList;
            }
            ivm.Products = _dataService.GetProducts();
            ivm.Brands = _dataService.GetBrands();
            ivm.PL = postList;

            
            return View(ivm);
        }

        [HttpGet("/{id:int}")]
        public IActionResult Index(int id, int? pageNumber)
        {
            const int pageSize = 5;

            PaginatedList<Post> postList = null;

            IndexViewModel ivm = new IndexViewModel();
            

           // //New Code
            postList = PaginatedList<Post>.Create(_dataService.GetPostByBrand((int)id), pageNumber ?? 1, pageSize);
            List<Product> products = _dataService.GetProductsByBrand(id);
            // foreach(Product p in products)
            // {
            //     ivm.Posts.AddRange(_dataService.GetPostByProduct(p.ProductId));
            // }

            

            ivm.Brands = _dataService.GetBrands();
            ivm.Products = _dataService.GetProducts();
            ivm.Posts = postList;
            ivm.PL = postList;

            return View(ivm);
        }

        [HttpGet("post/{postId:int}")]
        public IActionResult Post([FromRoute] int postId)
        {
            PostViewModel model = this.ConstructPostViewModel(postId);
            
            return View(model);
        }

    [Route("product/{id:int}")]
         public IActionResult Product(int id)
        {
            ProductViewModel pvm = new ProductViewModel();
            pvm.Product = _dataService.GetProduct(id);
            pvm.Posts = _dataService.GetPostByProduct(id);

            return View(pvm);
        }

         [Route("products")]
         public IActionResult Products()
        {
            List<Product> model = _dataService.GetProducts();

            return View(model);
        }

        private PostViewModel ConstructPostViewModel(int postId)
        {
            PostViewModel model = new PostViewModel();

            // Get post that matches post id.
            model.Post = _dataService.GetPost(postId);

            // Get list of comments for the post.
            List<Comment> tempComments = _dataService.GetComments().FindAll(x => x.PostId == postId);
            model.Comments = new List<Comment>();

            // Get all of the postvotes that matches the post.
            model.PostVotes = _dataService.GetPostVotes().FindAll(x => x.PostId == postId);

            // Get all of the comment votes for comments in the post page.
            model.CommentVotes = new List<CommentVote>();
            foreach(Comment c in tempComments)
            {
                model.CommentVotes.AddRange(_dataService.GetCommentVotes().FindAll(x => x.CommentId == c.CommentId));
                if(c.ParentCommentId == null)
                {
                    model.Comments.Add(c);
                }
            }

            foreach(Comment c in model.Comments)
            {
                c.ChildComments = new List<Comment>();
                this.ConstructComments(tempComments, c.CommentId);
            }

            model.NewComment = new Comment();

            // Get list of users who have interacted with this post page.
            model.User = _dataService.GetUser(model.Post.CreatorUserId);

            List<User> currentUsers = new List<User>();

            model.Users = currentUsers;

            if(User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                model.NewPostVote = _dataService.GetPostVote(userId, postId);
            }

            model.Product = _dataService.GetProduct(model.Post.ProductId);
                
            return model;    
        }

        //output is list of comments with empty list of children comments.
        //intput is list of comments unassigned.
        private void ConstructComments(List<Comment> input, int parentCommentId)
        {
            Comment c = input.Find(x=>x.CommentId == parentCommentId);
            if(c != null)
            {
                List<Comment> childComments = input.FindAll(x=>x.ParentCommentId == parentCommentId);
                c.ChildComments = childComments;
                foreach(Comment co in c.ChildComments)
                {
                    co.ChildComments = new List<Comment>();
                    this.ConstructComments(input, co.CommentId);
                }
            }
        }
    }
}
