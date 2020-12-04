using Access.Primitives.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StackUnderflow.EF.Models;
using Access.Primitives.Extensions.ObjectExtensions;
using System.Linq;
using static StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion.CreateQuestionResult;
using Access.Primitives.IO.Mocking;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion
{
    class CreateQuestionAdapter : Adapter<CreateQuestionCommand, CreateQuestionResult.ICreateQuestionResult>
    {
        private readonly IExecutionContext _ex;
        public CreateQuestionAdapter(IExecutionContext ex)
        {
            _ex = ex;
        }
        public override Task PostConditions(CreateQuestionCommand cmd, CreateQuestionResult.ICreateQuestionResult result, object state)
        {
            return Task.CompletedTask;
        }

        public override async Task<CreateQuestionResult.ICreateQuestionResult> Work(CreateQuestionCommand cmd, object state, object dependencies)
        {
            var questionWriteContext = (QuestionWriteContext)state;

            var workflow = from valid in cmd.TryValidate()
                           let t = AddQuestionIfMissing(questionWriteContext, CreateQuestionFromCommand(cmd))
                           select t;

            var result = await workflow.Match(
               Succ: r => r,
               Fail: ex => new InvalidRequest(ex.ToString()));

            return result;
        }
        public ICreateQuestionResult AddQuestionIfMissing(QuestionWriteContext state, Post question)
        {
            if (state.Questions.Any(p => p.Title.Equals(question.Title)))
                return new QuestionNotPublished($"Question with title {question.Title} already exist!");

            if (state.Questions.All(p => p.PostId == question.PostId))
                state.Questions.Add(question);

            return new QuestionPublished(question.PostId, question.Title, question.PostText);
        }
        private Post CreateQuestionFromCommand(CreateQuestionCommand cmd)
        {
            var question = new Post()
            {
                PostId = cmd.QuestionId,
                Title = cmd.Title,
                PostText = cmd.Tags.ToString(),
            };
            return question;
        }
    }
}
