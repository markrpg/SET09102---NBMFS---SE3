using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessageFilteringWPF
{
    /// <summary>
    /// SIREmail clas
    /// 40200606
    /// </summary>
    public class SIREmail : Email
    {
        /// <summary>
        /// Property to return sort code
        /// </summary>
        public string SortCode { get; }


        /// <summary>
        /// Used to return date of incident
        /// </summary>
        /// <returns>Returns date of incident.</returns>
        public string DateOfIncident { get; }


        /// <summary>
        /// Gets SIR incident for email.
        /// </summary>
        /// <returns>Returns SIR Incident Code.</returns>
        public string SirIncidentCode { get; }

        /// <summary>
        /// Default constructor for SIREmail class
        /// </summary>
        /// <param name="subject">Sets the email subject.</param>
        /// <param name="messageId">Sets message ID.</param>
        /// <param name="messageText">Sets message text.</param>
        /// <param name="sender">Sets message sender.</param>
        /// <param name="sortCode">Sets sortcode for SIREmail.</param>
        /// <param name="sirIncidentCode">Sets Incident report for SIREmail.S</param>
        /// <param name="sanitizingBehaviour">Sets the sanitizing behaviour</param>
        public SIREmail(string subject, int messageId, string messageText, string sender, string sortCode, string  sirIncidentCode, Sanitizing sanitizingBehaviour, string dateofincident) : base(subject,messageId,messageText,sender, sanitizingBehaviour)
        {
            //Sets SIR Incident code for SIREmail
            this.SirIncidentCode = sirIncidentCode;
            //Sets SortCode for SIREmail
            this.SortCode = sortCode;
            //Set date of incident
            DateOfIncident = dateofincident;
        }
    }
}
