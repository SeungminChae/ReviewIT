using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReviewIT.DataAccess
{
    public class Comment
    {
        public int CommentId { get; set; }
        
        // The id of post that contain this comment.
        public int PostId { get; set; }

        // The id of user who created this comment.
        public string CreatorUserId { get; set; }
        
        [Required]
        [MinLength(0, ErrorMessage = "The description must be longer than 0 characters." )]
        [DataType(DataType.MultilineText)]
        public string CommentContent { get; set; }

        public DateTime CreationDate { get; set; }
        
        // the id of comment that this comment is replying to.
        public int? ParentCommentId { get; set; }
        
        public Post Post {get; set;}

        public List<CommentVote> CommentVotes { get; set; }

        public List<Comment> ChildComments { get; set; }

        public User CreatorUser { get; set; }

        public bool IsArchived { get; set; }
    }
}