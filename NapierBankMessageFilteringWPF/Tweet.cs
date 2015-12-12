using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NapierBankMessageFilteringWPF
{
    /// <summary>
    /// Tweet class
    /// 40200606
    /// </summary>
   public class Tweet : Message
    {

        /// <summary>
        /// Gets all hashtags in message and returns them in a list.
        /// </summary>
        /// <returns>Returns ever hashtag in a list.</returns>
       private List<string> GetHashtagList()
       {
            //Regex to find hashtags
            Regex findHashtags = new Regex(@"(#\w+)");
            //Find all hashtags and put into list
            //return list of hashtags
            return (from Match m in findHashtags.Matches(MessageText) select m.Value).ToList();
       }

        /// <summary>
        /// Property to return list of hashtags
        /// </summary>
        public List<string> Hashtags { get; }
         
        /// <summary>
        /// Property to return list of mentions
        /// </summary>
        public List<string> Mentions { get;}

        /// <summary>
        /// Gets all mentions in message.
        /// </summary>
       private List<string> GetMentionsList()
        { 
            //Regex expression to find all mentions
            Regex findMentions =  new Regex(@"(^|\W)@\b([-a-zA-Z0-9._]{1,25})\b");
            //Find all hashtags and put into list
            //return list of hashtags
            return (from Match m in findMentions.Matches(MessageText) select m.Value).ToList();
        }

        /// <summary>
        /// Default constructor for Tweet class
        /// </summary>
        /// <param name="messageId">Sets message ID.</param>
        /// <param name="messageText">Sets message text.</param>
        /// <param name="sender">Sets message sender.</param>
        /// <param name="sanitizingBehaviour">Sets the sanitizing behaviour</param>
        public Tweet(int messageId, string messageText, string sender, Sanitizing sanitizingBehaviour) : base(messageId,messageText,sender, sanitizingBehaviour)
        {
            Hashtags = GetHashtagList();
            Mentions = GetMentionsList();
        }
    }
} 
