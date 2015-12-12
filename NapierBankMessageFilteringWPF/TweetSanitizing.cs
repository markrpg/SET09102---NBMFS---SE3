using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NapierBankMessageFilteringWPF
{
    public class TweetSanitizing : Sanitizing
    {
        public override string SanitizeMessage(string messageBody)
        {
            //Get textspeak abbreviations
            return ExpandAbbreviations(messageBody);
        }
    }
}
