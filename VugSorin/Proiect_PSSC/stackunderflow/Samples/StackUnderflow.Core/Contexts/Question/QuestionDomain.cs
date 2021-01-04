using Access.Primitives.IO;
using StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion;
using StackUnderflow.Domain.Core.Contexts.Question.ReplyQuestion;
using System;
using static PortExt;
using static StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion.CreateQuestionResult;
using static StackUnderflow.Domain.Core.Contexts.Question.ReplyQuestion.ReplyQuestionResult;

namespace StackUnderflow.Domain.Core.Contexts.Question
{
    public static class QuestionDomain
    {
        public static Port<CreateQuestionResult.ICreateQuestionResult> CreateQuestion(int questionId, string questionText, string title, int tenant, Guid user, string tags)
           => NewPort<CreateQuestionCommand, CreateQuestionResult.ICreateQuestionResult>(new CreateQuestionCommand(questionId, questionText,title, tenant, user,tags));
        public static Port<IReplyQuestionResult> ConfirmQuestion(ReplyQuestionCommand command) => NewPort<ReplyQuestionCommand, IReplyQuestionResult>(command);
    }
}