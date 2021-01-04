using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion
{
    public struct CreateQuestionCommand
    {
        [Required]
        public int  QuestionId { get;  set; }
        [Required]
        public string  QuestionText { get;  set; }
        [Required]
        public string Title { get;  set; }
        [Required]
        public int TenantId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public string Tags { get;  set; }
        public CreateQuestionCommand(int questionId, string questionText,string title, int tenant, Guid user, string tags)
        {
            QuestionId = questionId;
            QuestionText = questionText;
            Title = title;
            TenantId = tenant;
            UserId = user;
            Tags = tags;
        }
    }
}
