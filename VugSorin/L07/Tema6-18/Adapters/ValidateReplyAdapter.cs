﻿using System;
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
    public class ValidateReplyAdapter : Adapter<ValidateReplyCmd, ValidateReplyResult.IValidateReplyResult, QuestionWriteContext>
    {
        public override Task PostConditions(ValidateReplyCmd cmd, ValidateReplyResult.IValidateReplyResult result, QuestionWriteContext state)
        {
            return Task.CompletedTask;
        }

        public override async Task<ValidateReplyResult.IValidateReplyResult> Work(ValidateReplyCmd cmd, QuestionWriteContext state)
        {
            var wf = from isValid in cmd.TryValidate()
                     from validateReply in ValidateReply(cmd, state)
                     select validateReply;
            return await wf.Match(
                  Succ: reply => reply,
                  Fail: ex => new ValidateReplyResult.InvalidRequest(ex.ToString()));
        }
        private TryAsync<ValidateReplyResult.IValidateReplyResult> ValidateReply(ValidateReplyCmd cmd, QuestionWriteContext state)
        {
            return TryAsync<ValidateReplyResult.IValidateReplyResult>(async () =>
            {
                if (!state.AuthorIds.Any(p => p == cmd.AuthorId))
                    return new ValidateReplyResult.InvalidReply("The provided AuthorId does not exist");
                if (!state.QuestionIds.Any(p => p == cmd.QuestionId))
                    return new ValidateReplyResult.InvalidReply($"The provided QUestionId [{cmd.QuestionId}] does not exist");

                return new ValidateReplyResult.ReplyValidated(new Reply(1, cmd.QuestionId, cmd.AuthorId, cmd.Reply));
            });

        }
    }
}
