using StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question
{
    public class QuestionWriteContext
    {
        public ICollection<CreateQuestionCommand> Questions { get; }
        public QuestionWriteContext(ICollection<CreateQuestionCommand> questions)
        {
            Questions = questions ?? new List<CreateQuestionCommand>(0);
        }

    }
}
