using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace NapierBankMessageFilteringWPF
{

    /// <summary>
    /// Abstract Message class 
    /// 40200606
    /// </summary>
   public  abstract class Message
    {
        /// <summary>
        /// ID for message
        /// </summary>
        public int MessageID { get; }

        /// <summary>
        /// The text included in the message
        /// </summary>
        public string MessageText { get; }

        /// <summary>
        /// Behaviour for sanitizing message
        /// </summary>
        private Sanitizing SanitizeBehaviour;

        /// <summary>
        /// Holds the message sender 
        /// </summary>
        public string Sender { get; }


        /// <summary>
        /// Default constructor for message
        /// </summary>
        /// <param name="messageId">Sets the ID of the message.</param>
        /// <param name="messageText">Sets the message text containing in the message.</param>
        /// <param name="sender">Sets the sender of the message.</param>
        /// <param name="sanitizingBehaviour">Sets the sanitizing behaviour of the Message.</param>
        protected Message(int messageId, string messageText, string sender, Sanitizing sanitizingBehaviour)
        {
            //Sets message id
            MessageID = messageId;
            //Sets message sender
            Sender = sender;
            //Sets the sanitizing behaviour
            SanitizeBehaviour = sanitizingBehaviour;
            //Sanitize message based on behaviour & set message text
            MessageText = SanitizeBehaviour.SanitizeMessage(messageText);
        }

        /// <summary>
        /// Override ToString to show messade ID, useful for listing
        /// </summary>
        /// <returns>Returns Message ID</returns>
        public override string ToString()
        {
            return MessageID.ToString();
        }
    }
}
