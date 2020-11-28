using Access.Primitives.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StackUnderflow.EF.Models;
using Access.Primitives.Extensions.ObjectExtensions;
using System.Linq;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion
{
    class CreateQuestionAdapter : Adapter<CreateQuestionCommand, CreateQuestionResult.ICreateQuestionResult>
    {
        public override Task PostConditions(CreateQuestionCommand cmd, CreateQuestionResult.ICreateQuestionResult result, object state)
        {
            return Task.CompletedTask;
        }

        public override async Task<CreateQuestionResult.ICreateQuestionResult> Work(CreateQuestionCommand cmd, object state, object dependencies)
        {
            var idQuestion = new CreateQuestionCommand()
            {
                QuestionId = cmd.QuestionId
            };
            var title = new CreateQuestionCommand()
            {
                Title = cmd.Title
            };
            var tags = new CreateQuestionCommand()
            {
                Tags = cmd.Tags
            };
  
            var questionWriteContext = (QuestionWriteContext)state;
            if (!questionWriteContext.Questions.Any(p => p.Title.Equals(cmd.Title)))
                return new CreateQuestionResult.QuestionNotPublished($"Question with title {cmd.Title} already exist!");

            var question = questionWriteContext.Questions.First(p => p.Title.Equals(p.Title));

            return new CreateQuestionResult.QuestionPublished(Guid.Parse(idQuestion.ToString()), title.ToString(), tags.ToString());
        }
    }
}
