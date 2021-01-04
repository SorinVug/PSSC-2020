using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using Access.Primitives.IO.Mocking;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion.CreateQuestionResult;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion
{
    public partial class CreateQuestionAdapter : Adapter<CreateQuestionCommand, ICreateQuestionResult, QuestionWriteContext, QuestionDependencies>
    {
        private readonly IExecutionContext ex_;
        public CreateQuestionAdapter(IExecutionContext ex)
        {
            ex_ = ex;
        }

        public override async Task<ICreateQuestionResult> Work(CreateQuestionCommand cmd, QuestionWriteContext state, QuestionDependencies dependencies)
        {
            var workflow = from valid in cmd.TryValidate()
                           let t = AddQuestionIfMissing(state, CreateQuestionFromCommand(cmd))
                           select t;

            var result = await workflow.Match(
                Succ: r => r,
                Fail: ex => new InvalidRequest(ex.ToString()));

            return result;
        }

        public ICreateQuestionResult AddQuestionIfMissing(QuestionWriteContext state, Post question)
        {
            if (state.Questions.Any(p => p.Title.Equals(question.Title)))
                return new QuestionNotCreated($"Question with title {question.Title} already exist!"); 

            if (state.Questions.All(p => p.PostId != question.PostId))
                state.Questions.Add(question);

            return new QuestionCreated(question);
        }

        public Post CreateQuestionFromCommand(CreateQuestionCommand cmd)
        {
            
            var question = new Post()
            {
                Title = cmd.Title,
                PostText = cmd.QuestionText,
                PostTypeId = 1,
                PostedBy = cmd.UserId,
                TenantId = cmd.TenantId
            };
            //question.PostView.Add(new PostView()
            //{
            //    TenantId = cmd.TenantId,
            //    UserId = cmd.UserId,
            //    PostId = cmd.QuestionId
            //});
            //question.PostTag.Add(new PostTag()
            //{
            //    TenantId = cmd.TenantId,
            //    QuestionId = cmd.QuestionId
            //});
            return question;
        }

        public override Task PostConditions(CreateQuestionCommand cmd, ICreateQuestionResult result, QuestionWriteContext state)
        {
            return Task.CompletedTask;
        }
    }
}