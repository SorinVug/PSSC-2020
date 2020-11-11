using System;
using System.Collections.Generic;
using System.Text;
using Tema6-18.Inputs;
using Tema6-18.Outputs;
using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using LanguageExt;
using System.Threading.Tasks;

namespace Tema6_18.Adapters
{
    class SendAckToReplyAuthorAdapter : Adapter<SendAckToReplyAuthorCmd, SendAckToReplyAuthorResult.ISendAskToReplyAuthorResult, Unit>
    {
        public override Task PostConditions(SendAckToReplyAuthorCmd cmd, SendAckToReplyAuthorResult.ISendAskToReplyAuthorResult result, Unit state)
        {
            return Task.CompletedTask;
        }

        public override async Task<SendAckToReplyAuthorResult.ISendAskToReplyAuthorResult> Work(SendAckToReplyAuthorCmd cmd, Unit state)
        {
            var wf = from isValid in cmd.TryValidate()
                     from authorAck in AuthorAckResult(cmd, state)
                     select authorAck;
            return await wf.Match(
                  Succ: author => author,
                  Fail: ex => new SendAckToReplyAuthorResult.InvalidReplyPublished(ex.ToString()));
        }
        private TryAsync<SendAckToReplyAuthorResult.ISendAskToReplyAuthorResult> AuthorAckResult(SendAckToReplyAuthorCmd cmd, Unit state)
        {

            return TryAsync<SendAckToReplyAuthorResult.ISendAskToReplyAuthorResult>(async () =>
            {
                return new SendAckToReplyAuthorResult.ReplyPublished(cmd.Reply);
            });
        }
    }
}
