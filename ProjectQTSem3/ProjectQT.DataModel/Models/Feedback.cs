using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectQT.DataModel.Models
{
    public class Feedback : BaseEntity
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UpdateBy { get; set; }
        public float FeedbackStar { get; set; }

        [ForeignKey("UserId")]
        public User Users { get; set; }
    }
}
