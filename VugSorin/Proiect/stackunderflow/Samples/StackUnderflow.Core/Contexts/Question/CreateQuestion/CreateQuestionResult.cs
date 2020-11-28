using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion
{
    [AsChoice]
    public static partial class CreateQuestionResult
    {
        public interface ICreateQuestionResult { }
        public class QuestionPublished : ICreateQuestionResult
        {
            public Guid QuestionId { get; private set; }
            public string Title { get; private set; }
            public string Tags { get; private set; }
           

            public QuestionPublished(Guid questionId, string category, string title)
            {
                QuestionId = questionId;
                Title = title;
                Tags = category;
            }
        }

        public class QuestionNotPublished : ICreateQuestionResult
        {
            public string Reason { get; set; }
            public QuestionNotPublished(string reason)
            {
                Reason = reason;
            }
        }

        public class QuestionValidationFailed : ICreateQuestionResult
        {
            public IEnumerable<string> ValidationErrors { get; private set; }

            public QuestionValidationFailed(IEnumerable<string> errors)
            {
                ValidationErrors = errors.AsEnumerable();
            }
        }
    }
}
