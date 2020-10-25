using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DomainQuestion.CreateQuestionWorkflow
{
    public struct CreateQuestionCommand
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public IEnumerable<string> Tags { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Question { get; set; }

        public CreateQuestionCommand(string title,string category,List<string> tags,string question)
        {
            Title = title;
            Category = category;
            Tags = tags.AsEnumerable();
            Question = question;
        }
    };
    
}
