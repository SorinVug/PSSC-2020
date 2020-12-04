using Access.Primitives.IO;
using StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion;
using System;
using System.Collections.Generic;
using System.Text;
using static PortExt;
namespace StackUnderflow.Domain.Core.Contexts.Question
{
    public static class QuestionDomain
    {
        public static Port<CreateQuestionResult.ICreateQuestionResult> CreateQuestion(int questionId,string title, string tags)
            => NewPort<CreateQuestionCommand, CreateQuestionResult.ICreateQuestionResult>(new CreateQuestionCommand(questionId,title, tags));
    }
}
