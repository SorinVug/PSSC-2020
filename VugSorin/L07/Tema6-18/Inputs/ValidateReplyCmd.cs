using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tema6_18.Inputs
{
    public class ValidateReplyCmd
    {
        [Required]
        public int AuthorId { get; }

        [Required]
        public int QuestionId { get; }

        [Required]
        [StringRange(10, 500)]
        public string Reply { get; }

        public ValidateReplyCmd(int authorId, int questionId, string reply)
        {
            AuthorId = authorId;
            QuestionId = questionId;
            Reply = reply;
        }
    }
}
