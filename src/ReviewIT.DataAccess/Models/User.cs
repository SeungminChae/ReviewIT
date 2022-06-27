using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReviewIT.DataAccess
{
    public class User
    {
        // Authentication
        public string UserId { get; set; }

        // Encrypted string of password.
        public string PasswordHash { get; set;}
        public string EmailAddress { get; set; }
        public string NickName { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsArchived { get; set; }

        public List<CommentVote> CommentVotes { get; set; }

        public List<PostVote> PostVotes { get; set; }

        public List<Post> Posts { get; set; }

        public List<Comment> Comments { get; set; }
    }
}