using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessageFilteringWPF
{
    /// <summary>
    /// SMSMessage class
    /// 40200606
    /// </summary>
    public class SMSMessage : Message
    {
        /// <summary>
        /// Default constructor for SMSMessage class
        /// </summary>
        /// <param name="messageId">Sets message ID.</param>
        /// <param name="messageText">Sets message text.</param>
        /// <param name="sender">Sets message sender.</param>
        /// <param name="sanitizingBehaviour">Sets the sanitizing behaviour</param>
        public SMSMessage(int messageId, string messageText, string sender, Sanitizing sanitizingBehaviour) : base(messageId,messageText,sender,sanitizingBehaviour)
        {
        }
    }
}
