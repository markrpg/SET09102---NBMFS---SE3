using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessageFilteringWPF
{
    public class SMSSanitizing : Sanitizing
    {
        public override string SanitizeMessage(string messageBody)
        {
            //Get textspeak abbreviations
            return ExpandAbbreviations(messageBody);
        }
    }
}
