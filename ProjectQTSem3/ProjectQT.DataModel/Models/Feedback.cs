using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectQT.DataModel.Models
{
    public class Feedback
    {
        [Key]
        public int Id { set; get; }
        public DateTime CreateAt { set; get; }
        public int UserId { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]

        public string Title { get; set; }
        [Required]

        public string Content { get; set; }
        public float FeedbackStar { get; set; }
        public int ProductId { get; set; }
        public bool existOrder { set; get; }
        public bool Status { set; get; }

        [ForeignKey("UserId")]
        public User Users { get; set; }

    }
}
