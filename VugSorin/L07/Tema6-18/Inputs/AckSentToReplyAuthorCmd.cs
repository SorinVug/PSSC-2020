using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Tema6-18.Outputs;
namespace Tema6_18.Inputs
{
    class AckSentToReplyAuthorCmd
    {
        public int QuestionId { get; }
        public int ReplyId { get; }
        public string Reply { get; }
        public ReplyPublishedAckSentToReplyAuthorCmd(int questionId,int replyId, string reply)
        {
            QuestionId = questionId;
            ReplyId = replyId;
            Reply = reply;
        }
    }
}
