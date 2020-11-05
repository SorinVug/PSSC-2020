using System;
using LanguageExt;
using Tema6.Outputs;

namespace Tema6
{
    class Program
    {
        static void Main(string[] args)
        {
            var wf = from createReplyResult in Domain.ValidateReply(1, 1, "test")
                     let validReply = (CreateReplyResult.ReplyValid)createReplyResult
                     from checkLanguageResult in Domain.CheckLanguage(validReply.Reply.Answer)
                     from ownerAck in Domain.SendAckToOwner(checkLanguageResult)
                     from authorAck in Domain.SendAckToAuthor(checkLanguageResult)
                     select (validReply, checkLanguageResult, ownerAck, authorAck);


            Console.WriteLine("Hello World!");
        }

    }

    internal interface IReplyPosted
    {
    }
}
