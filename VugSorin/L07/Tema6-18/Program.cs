using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;
using LanguageExt;
using Tema6-18;
using Tema6-18.Outputs;
using Access.Primitives.IO;
using Tema6-18.Adapters;
namespace Tema6_18
{
    class Program
    {
        static void Main(string[] args)
        {

            var wf = from createReplyResult in BoundedContextDSL.ValidateReply(100, 1, "InternetOfThings")
                let validReply = (CreateReplyResult.ReplyValid)createReplyResult
                from checkLanguageResult in BoundedContextDSL.CheckLanguage(validReply.Reply.Answer)
                from ownerAck in BoundedContextDSL.SendAckToOwner(checkLanguageResult)
                from authorAck in BoundedContextDSL.SendAckToAuthor(checkLanguageResult)
                select (validReply, checkLanguageResult, ownerAck, authorAck);

            var serviceProvider = new ServiceCollection()
                .AddOperations(typeof(ValidateReplyAdapter).Assembly)
                .AddOperations(typeof(CheckLanguageAdapter).Assembly)
                .AddOperations(typeof(SenAckToQuestionOwnerAdapter).Assembly)
                .AddTransient<IInterpreterAsync>(sp => new LiveInterpreterAsync(sp))
                .BuildServiceProvider();
            Console.WriteLine("Hello World!");
        }
    }
}
