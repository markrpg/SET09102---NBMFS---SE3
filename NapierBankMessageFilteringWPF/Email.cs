using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessageFilteringWPF
{
    /// <summary>
    /// Abstract Email class
    /// 40200606
    /// </summary>
    public abstract class Email : Message
    {
        /// <summary>
        /// Used to return subject for email.
        /// </summary>
        public string Subject { get; }

        /// <summary>
        /// Default constructor for Email class
        /// </summary>
        /// <param name="subject">Sets the emails subject</param>
        /// <param name="messageId">Sets message ID.</param>
        /// <param name="messageText">Sets message text.</param>
        /// <param name="sender">Sets message sender.</param>
        /// <param name="sanitizingBehaviour">Sets the sanitizing behaviour</param>
        public Email(string subject, int messageId, string messageText, string sender, Sanitizing sanitizingBehaviour) : base(messageId,messageText,sender,sanitizingBehaviour)
        {
            //Sets subject for email
            this.Subject = subject;
        }
    }
}
