using Access.Primitives.Extensions.Cloning;
using CSharp.Choices;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion
{
    [AsChoice]
    public static partial class CreateQuestionResult
    {
        public interface ICreateQuestionResult : IDynClonable { }

        public class QuestionPublished : ICreateQuestionResult
        {
            public Post Question { get; }
            public int QuestionId { get; private set; }
            public string Title { get; private set; }
            public string Tags { get; private set; }
           

            public QuestionPublished(int questionId, string category, string title)
            {
                QuestionId = questionId;
                Title = title;
                Tags = category;
            }
            public object Clone() => this.ShallowClone();
        }

        public class QuestionNotPublished : ICreateQuestionResult
        {
            public string Reason { get; set; }
            public QuestionNotPublished(string reason)
            {
                Reason = reason;
            }
            public object Clone() => this.ShallowClone();
        }

        public class QuestionValidationFailed : ICreateQuestionResult
        {
            public IEnumerable<string> ValidationErrors { get; private set; }

            public QuestionValidationFailed(IEnumerable<string> errors)
            {
                ValidationErrors = errors.AsEnumerable();
            }
            public object Clone() => this.ShallowClone();
        }
        public class InvalidRequest : ICreateQuestionResult
        {
            public string Message { get; }

            public InvalidRequest(string message)
            {
                Message = message;
            }

            ///TODO
            public object Clone() => this.ShallowClone();
        }
    }
}
