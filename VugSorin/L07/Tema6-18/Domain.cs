using System;
using System.Collections.Generic;
using System.Text;
using Access.Primitives.IO;
using Tema6_18.Inputs;
using Tema6_18.Outputs;
using static PortExt;

namespace Tema6_18
{
    public static class BoundedContext
    {
        public static Port<ValidateReplyResult.IValidateReplyResult> ValidateReply(int authorId, int questionId,
            string answer)
            => NewPort<ValidateReplyCmd, ValidateReplyResult.IValidateReplyResult>(
                new ValidateReplyCmd(authorId, questionId, answer));

        public static Port<CheckLanguageResult.ICheckLanguageResult> CheckLanguage(string text)
            => NewPort<CheckLanguageCmd, CheckLanguageResult.ICheckLanguageResult>(new CheckLanguageCmd(text));

    }
}
