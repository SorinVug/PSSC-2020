using System;
using System.ComponentModel.DataAnnotations;

namespace DomainQuestion.CreateQuestionWorkflow
{
    public struct CreateQuestionCommand
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Tags { get; set; }
        public string Topic { get; set; }

        public CreateQuestionCommand(string title,string category,string tags,string topic)
        {
            Title = title;
            Category = category;
            Tags = tags;
            Topic = topic;
        }
    };
    
}
