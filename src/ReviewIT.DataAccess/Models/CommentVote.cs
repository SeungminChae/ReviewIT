using System;

namespace ReviewIT.DataAccess
{
    public class CommentVote
    {
        public int CommentVoteId { get; set; }
        public int CommentId { get; set; }
        public string UserId { get; set; }

        // true if the vote is upvote and false if the vote is downvote.
        public bool IsUp { get; set; }

        public DateTime CreationDate { get; set; }

        public Comment Comment { get; set; }

        public User User { get; set; }

        public bool IsArchived { get; set; }
    }
}