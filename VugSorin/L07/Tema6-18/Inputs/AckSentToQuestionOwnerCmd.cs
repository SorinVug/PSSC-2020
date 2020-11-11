using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Tema6-18.Outputs;
namespace Tema6_18.Inputs
{
    class AckSentToQuestionOwnerCmd
    {
        public int QuestionId { get; }
        public int AuthorId { get; }
        public string Reply { get; }
        public ReplyReceivedAckSentToQuestionOwnerCmd(int questionId,int authorId, string reply)
        {
            QuestionId = questionId;
            AuthorId = authorId;
            Reply = reply;
        }
    }
}
