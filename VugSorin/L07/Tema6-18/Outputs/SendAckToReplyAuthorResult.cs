using System;
using System.Collections.Generic;
using System.Text;

namespace Tema6_18.Outputs
{
    public static partial class SendAckToReplyAuthorResult
    {
        public interface ISendAskToReplyAuthorResult { };
        public class InvalidReplyPublished : ISendAskToReplyAuthorResult
        {
            public string ErrorMessage { get; }
            public InvalidReplyPublished(string errormessage)
            {
                ErrorMessage = errormessage;
            }
        }
        public class ReplyPublished : ISendAskToReplyAuthorResult
        {
            public string ConfirmationMessage { get; }
            public ReplyPublished(string confirmationMessage)
            {
                ConfirmationMessage = confirmationMessage;
            }
        }
    }
}
