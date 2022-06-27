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
    [Authorize]
    [Route("User", Name = "User")]
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;

         private readonly AppConfig _appConfig;

         private readonly DataService _dataService;

        public UserController(ILogger<HomeController> logger, IOptions<AppConfig> appConfig, DataContext dataContext)
        {
            _logger = logger;
            _appConfig= appConfig.Value;
            _dataService = new DataService(dataContext);
        }

        [HttpPost("addComment")]
        public IActionResult AddComment(PostViewModel model)
        {
            Comment comment = model.NewComment;
            int postId = model.NewComment.PostId;
            comment.CreatorUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            comment.CreationDate = DateTime.Now;

            if(!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Post), "Home", new {postId = postId});
            }

            if(comment.CreatorUserId == null){
                ModelState.AddModelError("Error", "Please sign-in to write comment.");

                return RedirectToAction(nameof(Post), "Home", new {postId = postId});
            }
            
            // Gets creator id of new comment
            User currentUser = _dataService.GetUser(comment.CreatorUserId);

            // Check if the user is not found in the database.
            if(currentUser == null)
            {
                return RedirectToAction(nameof(Post), "Home", new {postId = postId});
            }

            _dataService.AddComment(comment);
            
            return RedirectToAction(nameof(Post), "Home", new {postId = postId});
        }

        [HttpGet("editcomment/{commentId:int}")]
        public IActionResult EditComment([FromRoute] int commentId)
        {
            if(!ModelState.IsValid)
            {
                return View(commentId);
            }

            Comment comment = _dataService.GetComment(commentId);

            return View(comment);
        }

        [HttpPost("editComment/{commentId:int}")]
        public IActionResult EditComment(Comment comment)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if(!ModelState.IsValid)
            {
                return View(comment.CommentId);
            }

            comment.CreatorUserId = userId;
            comment.CreationDate = DateTime.Now;
            
            if(userId == comment.CreatorUserId || User.IsInRole("Admin"))
            {
                _dataService.UpdateComment(comment);
            }

            return RedirectToAction(nameof(Post), "Home", new{postId = comment.PostId});
        }

        [HttpGet("addpost")]
        public IActionResult AddPost()
        {
            AddPostViewModel model = new AddPostViewModel();
            model.Products = _dataService.GetProducts();

            return View(model);
        }

        [HttpPost("addpost")]
        public IActionResult AddPost(AddPostViewModel model)
        {
            string currentUserId =  User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.Post.CreatorUserId = currentUserId;
            if (!ModelState.IsValid)
            {
                model = new AddPostViewModel();
                model.Products = _dataService.GetProducts();

                return View(model);
            }

            User currentUser = _dataService.GetUser(currentUserId);

            if(currentUser == null){
                ModelState.AddModelError("Error", "Please sign-in to write comment.");

                return View();
            }
            
            model.Post.CreationDate = DateTime.Now;
            
            _dataService.AddPost(model.Post);

            return RedirectToAction(nameof(Post), "Home", new { postId = model.Post.PostId });
        }
        
         [HttpGet("editpost/{postId:int}")]
        public IActionResult EditPost(int postId)
        {
            Post post = _dataService.GetPost(postId);
            AddPostViewModel pvm = new AddPostViewModel(){Post = post, Products = _dataService.GetProducts()};
            return View(pvm);
        }

        [HttpPost("editPost/{postId:int}")]
        public IActionResult EditPost(AddPostViewModel apv)
        {
            Post post = apv.Post;
            string currentUserId =  User.FindFirst(ClaimTypes.NameIdentifier).Value;
            bool securityCheck = (currentUserId == post.CreatorUserId) || User.IsInRole("Admin");
            if(!ModelState.IsValid && securityCheck)
            {
                AddPostViewModel pvm = new AddPostViewModel(){Post = post, Products = _dataService.GetProducts()};
                return View(pvm);
            }
            post.CreatorUserId = currentUserId;
            post.CreationDate = DateTime.Now;

            _dataService.UpdatePost(post);

            return RedirectToAction(nameof(Post), "Home", new {postId = post.PostId});
        }
        
        [HttpGet("delete-post/{id:int}")]
        public IActionResult DeletePostConfirm(int id)
        {
            Post post = _dataService.GetPost(id);

            return View(post);
        }

        [HttpPost("delete-post/{id:int}")]
        public IActionResult DeletePost(int id)
        {
            Post post = _dataService.GetPost(id);

            if(!ModelState.IsValid)
            {
                return View(post.PostId);
            }

            string currentUserId =  User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if(currentUserId == post.CreatorUserId || User.IsInRole("Admin"))
            {
                _dataService.DeletePost(post);
            }
            
            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpGet("delete-comment/{id:int}")]
        public IActionResult DeleteCommentConfirm(int id)
        {
            Comment comment = _dataService.GetComment(id);

            return View(comment);
        }

        [HttpPost("delete-comment/{id:int}")]
        public IActionResult DeleteComment(int id)
        {
            Comment comment = _dataService.GetComment(id);

            if(!ModelState.IsValid)
            {
                return View(id);
            }
            string currentUserId =  User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if(currentUserId == comment.CreatorUserId || User.IsInRole("Admin"))
            {
                _dataService.DeleteComment(id);
            }
            
            return RedirectToAction(nameof(Post), "Home", new {postId = comment.PostId});
        }

        [HttpPost("addpostvote")]
        public IActionResult AddPostVote(PostViewModel pvm)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            PostVote pv = pvm.NewPostVote;
            pv.UserId = userId;
            pv.CreationDate = DateTime.Now;

            PostVote existingPv = _dataService.GetPostVote(userId, pv.PostId);

            // check if user already have voted.
            if(existingPv != null)
            {
                if(existingPv.IsUp == pv.IsUp)
                {
                    _dataService.DeletePostVote(userId, pv.PostId);
                }
                else
                {
                    _dataService.DeletePostVote(userId, pv.PostId);
                    _dataService.AddPostVote(pv);
                }
            }
            else
            {
                _dataService.AddPostVote(pv);
            }

            return RedirectToAction(nameof(Post), "Home", new {postId = pv.PostId});
        }

        [HttpPost("addcommentvote")]
        public IActionResult AddCommentVote(PostViewModel pvm)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            CommentVote cv = pvm.NewCommentVote;
            cv.UserId = userId;
            cv.CreationDate = DateTime.Now;

            CommentVote existingCv = _dataService.GetCommentVote(userId, cv.CommentId);

            // check if user already have voted.
            if(existingCv != null)
            {
                if(existingCv.IsUp == cv.IsUp)
                {
                    _dataService.DeleteCommentVote(userId, cv.CommentId);
                }
                else
                {
                    _dataService.DeleteCommentVote(userId, cv.CommentId);
                    _dataService.AddCommentVote(cv);
                }
            }
            else
            {
                _dataService.AddCommentVote(cv);
            }

            return RedirectToAction(nameof(Post), "Home", new {postId = pvm.Post.PostId});
        }

    }
}