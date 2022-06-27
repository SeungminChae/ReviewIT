using System.Collections.Generic;
using ReviewIT.DataAccess;

namespace ReviewIT
{
    public class PostViewModel
    {
        public Post Post{ get; set; }

        public List<Comment> Comments{ get; set; }

        public List<PostVote> PostVotes { get; set; }

        public List<CommentVote> CommentVotes { get; set; }

        public Comment NewComment { get; set; }

        public Product Product { get; set; }

        public User User { get; set; }

        public List<User> Users { get; set; }

        public PostVote NewPostVote { get; set; }

        public CommentVote NewCommentVote { get; set; }
    }
}