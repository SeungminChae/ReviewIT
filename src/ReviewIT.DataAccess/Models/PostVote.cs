using System;

namespace ReviewIT.DataAccess
{
    public class PostVote
    {
        public int PostVoteId { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }

        // true if the vote is upvote and false if the vote is downvote.
        public bool IsUp { get; set; }

        public DateTime CreationDate { get; set; }

        public Post Post { get; set; }

        public User User { get; set; }

        public bool IsArchived { get; set; }
    }
}