using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReviewIT.DataAccess
{
    public class Post
    {

        public int PostId { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "The description must be longer than 1 characters." )]
        [DataType(DataType.MultilineText)]
        public string Title { get; set; }

        // Id of device that the post is reviewing. 0 if post is not about a specific device.
        public int ProductId { get; set; }

        // Id of the user who created the post.
        public string CreatorUserId { get; set;}
        [Required]
        [MinLength(20, ErrorMessage = "The description must be longer than 20 characters." )]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Content")]
        public string PostContent { get; set; }

        // Post Creator's rating 1 ~ 5. 0 means the creator did not rate it.
        public int RatingForDevice { get; set; }
        public DateTime CreationDate { get; set; }

        public List<Comment> Comments {get; set;}

        public List<PostVote> PostVotes { get; set; }

        public User CreatorUser { get; set; }

        public bool IsArchived { get; set; }

        public Product Product{ get; set; }

        
    }
}