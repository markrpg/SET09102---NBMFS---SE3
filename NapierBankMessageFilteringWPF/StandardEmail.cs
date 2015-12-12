using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessageFilteringWPF
{
    /// <summary>
    /// StandardEmail class
    /// 40200606
    /// </summary>
    public class StandardEmail : Email
    {
        /// <summary>
        /// Default constructor for StandardEmail class
        /// </summary>
        /// <param name="subject">Sets the email subject.</param>
        /// <param name="messageID">Sets message ID.</param>
        /// <param name="messageText">Sets message text.</param>
        /// <param name="sender">Sets message sender.</param>
        /// <param name="sanitizingBehaviour">Sets the sanitizing behaviour</param>
        public StandardEmail(string subject, int messageID, string messageText, string sender, Sanitizing sanitizingBehaviour) : base(subject,messageID,messageText,sender, sanitizingBehaviour)
        {
        }
    }
}
