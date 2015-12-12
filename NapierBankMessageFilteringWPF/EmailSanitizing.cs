using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NapierBankMessageFilteringWPF
{
    public class EmailSanitizing : Sanitizing
    {
        public override string SanitizeMessage(string messageBody)
        {
           //Local string to hold message text
            string messageText = messageBody;
            //Regex expression to find URL's
            Regex findUrl = new Regex(@"((?:http|https):\/\/[a-z0-9]+(\.[a-z0-9]+)+)|(www(\.[a-z0-9]+)+)");
            //String to replace quarentined url's
            string sanitizeReplacementText = "<URL Quarantined>";
            //Replace all urls with message
             messageText = findUrl.Replace(messageText,sanitizeReplacementText);
            //Return message text
            return messageText;
        }
    }
}
