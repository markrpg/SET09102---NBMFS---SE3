using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NapierBankMessageFilteringWPF
{

    /// <summary>
    /// Abstract class Sanitizing
    /// 40200606
    /// </summary>
    public abstract class Sanitizing
    {

        /// <summary>
        /// abstract class SanitizeMessage used in all child classes
        /// </summary>
        /// <param name="messageBody"></param>
        /// <returns></returns>
        public abstract string SanitizeMessage(string messageBody);

        /// <summary>
        /// Expands abbreviations for text.
        /// </summary>
        /// <param name="messageBody">Message text to check for abbreviations.</param>
        /// <returns>Message text with expanded abbreviations.</returns>
        public string ExpandAbbreviations(string messageBody)
        {
            //Regex to find word
            Regex findWord;
            //Loval string to hold message text
            string messageText = messageBody;
            //Get textspeak abbreviations
            //Dictionary to hold textspeak abbreviations
            Dictionary<string, string> textAbbreviations = NBMFilteringService.GetAbbreviations();

            //Find abbreviations and expand them
            foreach (string key in textAbbreviations.Keys)
            {
                //Make regex to find word
                findWord = new Regex(string.Format(@"\b({0}|{1})\b", key.ToLower(),key.ToUpper()));

                //Used to store output of dictionary
                string tempAbbreviations;

                //If message body contains key
                if (findWord.IsMatch(messageBody))
                {
                    //Get abbreviation for key
                    textAbbreviations.TryGetValue(key, out tempAbbreviations);
                    tempAbbreviations = " <" + tempAbbreviations + "> ";
                    //Expand abbreviations
                    messageText = findWord.Replace(messageText,key + tempAbbreviations);
                }
            }

            //Return expanded abbreviations 
            return messageText;
        }
    }
}
