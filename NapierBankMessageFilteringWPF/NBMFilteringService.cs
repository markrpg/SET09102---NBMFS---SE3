using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using Microsoft.Win32;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace NapierBankMessageFilteringWPF
{
    /// <summary>
    /// NBMFilteringService Singleton control class
    /// 40200606
    /// </summary>
    public class NBMFilteringService
    {
        /// <summary>
        /// List used to hold all messages within the system.
        /// </summary>
        private List<Message> MessageList;

        /// <summary>
        /// Singleton class default constructor
        /// </summary>
        public NBMFilteringService()
        {
            //Setup messagelist
            MessageList = new List<Message>();
        }

        /// <summary>
        /// Used to return a list of messages.
        /// </summary>
        /// <returns>List of messages.</returns>
        public List<Message> GetMessageList()
        {
            return MessageList;
        }

        /// <summary>
        /// Method used to return a sorted list of trending hashtags
        /// </summary>
        /// <returns>List of sorted hashtags trending</returns>
        public List<string> GetHashtagTrendingList()
        {
            //int used for counting trending
            int usedForCounting;
            //Temp list to return trending list
            List<string> trendingListSorted = new List<string>();
            //Temp dictionary for working out trending list
            Dictionary<string,int> trending = new Dictionary<string, int>();
            foreach (Message message in MessageList)
            {
                if (message.GetType() == typeof (Tweet))
                {
                    foreach (string hashtag in ((Tweet)message).Hashtags)
                    {
                        //If hashtag already exists in dictionary increment value
                        if(trending.TryGetValue(hashtag,out usedForCounting))
                        {
                            //Increment value
                            trending[hashtag] = usedForCounting + 1;
                        }
                        //if dictionary doesnt contain hashtag add it
                        else if (!trending.TryGetValue(hashtag, out usedForCounting))
                        {
                            //Add it and set to 1
                            trending.Add(hashtag,1);
                        }
                    }
                }
            }

            //build list in order of highest trending
            foreach (KeyValuePair<string, int> hashtags in trending.OrderByDescending(x => x.Value))
            {
                trendingListSorted.Add(hashtags.Key + " : " + hashtags.Value);
            }

            return trendingListSorted;
        }

        

        /// <summary>
        /// Method used to sorted list of trending mentions
        /// </summary>
        /// <returns>Returns list of trending mentions</returns>
        public List<string> GetMentionsTrendingList()
        {
            //int used for counting trending
            int usedForCounting;
            //Temp list to return trending list
            List<string> trendingListSorted = new List<string>();
            //Temp dictionary for working out trending list
            Dictionary<string, int> trending = new Dictionary<string, int>();
            foreach (Message message in MessageList)
            {
                if (message.GetType() == typeof(Tweet))
                {
                    foreach (string mentions in ((Tweet)message).Mentions)
                    {
                        //If hashtag already exists in dictionary increment value
                        if (trending.TryGetValue(mentions, out usedForCounting))
                        {
                            //Increment value
                            trending[mentions] = usedForCounting + 1;
                        }
                        //if dictionary doesnt contain hashtag add it
                        else if (!trending.TryGetValue(mentions, out usedForCounting))
                        {
                            //Add it and set to 1
                            trending.Add(mentions, 1);
                        }
                    }
                }
            }

            //build list in order of highest trending
            foreach (KeyValuePair<string, int> mentions in trending.OrderByDescending(x => x.Value))
            {
                trendingListSorted.Add(mentions.Key + " : " + mentions.Value);
            }

            return trendingListSorted;
        }



        /// <summary>
        /// Method used to get trending sircodes
        /// </summary>
        /// <returns>Returns sorted list of trending sir codes.</returns>
        public List<string> GetSirCodesTrendingList()
        {
            //int used for counting trending
            int usedForCounting;
            //Temp list to return trending list
            List<string> trendingListSorted = new List<string>();
            //Temp dictionary for working out trending list
            Dictionary<string, int> trending = new Dictionary<string, int>();
            foreach (Message message in MessageList)
            {
                if (message.GetType() == typeof(SIREmail))
                {
                    //If hashtag already exists in dictionary increment value
                    if (trending.TryGetValue(((SIREmail)message).SirIncidentCode, out usedForCounting))
                    {
                        //Increment value
                        trending[((SIREmail)message).SirIncidentCode] = usedForCounting + 1;
                    }
                    //if dictionary doesnt contain hashtag add it
                    else if (!trending.TryGetValue(((SIREmail)message).SirIncidentCode, out usedForCounting))
                    {
                        //Add it and set to 1
                        trending.Add(((SIREmail)message).SirIncidentCode, 1);
                    }

                }
            }

            //build list in order of highest trending
            foreach (KeyValuePair<string, int> mentions in trending.OrderByDescending(x => x.Value))
            {
                trendingListSorted.Add(mentions.Key + " : " + mentions.Value);
            }

            return trendingListSorted;
        }

        /// <summary>
        /// Used to get text abbreviations for sanitizing Tweets and SMS Messages.
        /// </summary>
        /// <returns>Returns a dictionary containing (abbreviations and their value)</returns>
        public static Dictionary<string, string> GetAbbreviations()
        {
            try
            {
                //Get textspeak abbreviations
                //Dictionary to hold textspeak abbreviations
                Dictionary<string, string> textAbbreviations = new Dictionary<string, string>();

                using (StreamReader sr = new StreamReader("textwords.csv"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        textAbbreviations.Add(line.Split(',')[0], line.Split(',')[1]);
                    }

                    sr.Close();
                }

                return textAbbreviations;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Method to create message from file location
        /// </summary>
        /// <param name="fileLocation">Location of file to load messages</param>
        /// <returns>Returns true if successful, throws an exception if not.</returns>
        public bool OpenMessageFromFile(string fileLocation)
        {
            //Local string to hold message
            List<string> messages = new List<string>();

            //Error Handling for message reading
            try
            {
                //Read message in from file
                using (StreamReader sr = new StreamReader(fileLocation))
                {
                    while (sr.Peek() >= 0)
                    {
                        messages.Add(sr.ReadLine().Trim());
                    }

                    //Close file
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading message file: " + ex.Message);
            }

            //Add all messages to system 
            foreach (string messageString in messages)
            {
                //Split message up into string array
                string[] messageSplit = messageString.Split(',');

                //Error handling for adding new messages
                try
                {
                    //depending on the type of message the string array will have a different count
                    //Check if Tweet or sms message both contain 3 entries in array
                    //Ex Tweet. T999999999,@bob,This is a tweet
                    if (messageSplit.Count() == 3)
                    {

                        //Create Tweet or SMS
                        NewMessage(messageSplit[0][0].ToString(),
                            int.Parse(messageSplit[0].Substring(1, messageSplit[0].Length - 1)), messageSplit[1],String.Empty,
                            messageSplit[2]);

                    }
                    //Ex Email. E999999999,bob@bob.com,My subject,Hi this is an email
                    else if (messageSplit.Count() == 4)
                    {
                        NewMessage(messageSplit[0][0].ToString(),
                            int.Parse(messageSplit[0].Substring(1, messageSplit[0].Length - 1)), messageSplit[1],
                            messageSplit[2], messageSplit[3]);
                    }
                    //Invalid message 
                    else
                    {
                        throw new Exception("Invalid message detected!");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            //If all messages successfully added to system
            return true;
        }



        /// <summary>
        /// Creates a message (Tweet, SMS, Email)
        /// </summary>
        /// <param name="messageHeader">Type of message (S,E,T)</param>
        /// <param name="messageId">ID for message</param>
        /// <param name="messageSender">Sender of message (Telephone number, Tweet ID, Email sender)</param>
        /// <param name="messageSubject">Subject of message if email.</param>
        /// <param name="messageBody">Message body.</param>
        /// <returns>Returns true if successful, throw exception if not.</returns>
        public bool NewMessage(string messageHeader, int messageId, string messageSender, string messageSubject, string messageBody)
        {


            //Regex Expression to discern if an email has a SIRCode or not.
            Regex isSirCode = new Regex(@"([Ss][Ii][Rr]:)(.?[0-9]{2}\/[0-9]{2}\/[0-9]{2})");
            //Regex Expression to dicern a sortcode
            Regex findSortCode = new Regex(@"([Ss]ort.?[Cc]ode:)(.?[0-9]{2}\-[0-9]{2}\-[0-9]{2})");
            //Regex Expression to find incident code
            Regex findIncidentCode = new Regex(@"([Nn]ature.?[Oo]f.?[Ii]ncident:)(.?[a-zA-Z]+.[a-zA-Z]+)");

            //Error control for invalid messages or problems creating them
            try
            {
                //Check if message already exists
                if (AlreadyExist(messageId))
                    throw new Exception("Unique message already exists in the system.");

                //Check if valid message
                if (!checkMessageIsValid(messageHeader.ToLower(), messageId, messageSender, messageBody, messageSubject))
                    throw new Exception("Invalid message.");

                //Create message
                switch (messageHeader.ToLower().Trim())
                {
                    //Message is SMS 
                    case "s":
                        //Create new smsmessage
                        SMSMessage smsMessage = new SMSMessage(messageId, messageBody, messageSender, new SMSSanitizing());
                        //Add message to message list
                        MessageList.Add(smsMessage);
                        return true;
                    //Message is Tweet
                    case "t":
                        //Create new tweet message
                        Tweet tweetMessage = new Tweet(messageId, messageBody, messageSender, new TweetSanitizing());
                        //Add message to message list
                        MessageList.Add(tweetMessage);
                        return true;
                    //Message is Email
                    case "e":
                        //Check the type of email
                        //If it has SIR code its SIREmail
                        if (isSirCode.IsMatch(messageSubject))
                        {
                            //Holds sortcode
                            string sortCode = findSortCode.Match(messageBody).Groups[2].Value.Trim();
                            //Holds incidentReport
                            string incidentReport = findIncidentCode.Match(messageBody).Groups[2].Value.Trim();
                            //Holds date of incident
                            string dateofIncident = isSirCode.Match(messageSubject).Groups[2].Value.Trim();
                            //Make a message and add it to list
                            MessageList.Add(new SIREmail(messageSubject, messageId, messageBody, messageSender, sortCode,
                                incidentReport, new EmailSanitizing(), dateofIncident));
                            return true;
                        }
                        else
                        {
                            //Create standard email and add to list
                            MessageList.Add(new StandardEmail(messageSubject, messageId, messageBody, messageSender, new EmailSanitizing()));
                            return true;
                        }
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error found: " + ex.Message);
            }
        }

        /// <summary>
        /// Method to check if a message meets the restrictions of each message type
        /// </summary>
        /// <param name="header">Type of message (e.g s,t,e)</param>
        /// <param name="messageId">ID of message.</param>
        /// <param name="sender">Sender of message.</param>
        /// <param name="messageBody">Message text of message.</param>
        /// <returns>Returns true if message is correct, throw error if not.</returns>
        private bool checkMessageIsValid(string header, int messageId, string sender, string messageBody, string subject)
        {
            //Regex needed for verification of message data
            //Regex to test international telephone number
            Regex isInternationalNumber = new Regex(@"^(\+)?([0-9].[\/.()-]*)(\d[\/.()-]*){7,11}$");
            //Regex to test for email
            Regex isEmail = new Regex(@"^(([^\.\@])(([a-zA-Z0-9](\.?_?\-?)))+(\@)[\w]+(\.)[a-zA-Z]+(\.?)[a-zA-Z]+)$");
            //Regex to test for User ID for Tweet
            Regex isTwitterId = new Regex(@"\B@[a-z0-9_-]+");

            //Check its correct number of digits for ID
            if (messageId.ToString().Length > 9)
                throw new Exception("Message ID is more than 9 characters in length. (Must be 9 and under)");

            //Check what type of message it is
            switch (header.ToLower())
            {
                //Sms message
                case "s":
                    //Check sender has international phone number
                    if (!isInternationalNumber.IsMatch(sender))
                        throw new Exception("Sender must be an international telephone number. (e.g +447534776521, +4407332442566)");
                    //Check correct message length for sms
                    if (messageBody.Length > 140)
                        throw new Exception("Message text is larger than 140 characters!");
                    //If everything passed the filter return true correct message format
                    return true;
                //Text message
                case "t":
                    //Check sender has twitter id
                    if (!isTwitterId.IsMatch(sender))
                        throw new Exception("Please enter a valid twitter id (e.g @bob or @bob_bob)");
                    //Check correct message length for tweet
                    if (messageBody.Length > 140)
                        throw new Exception("Message text is larger than 140 characters!");
                    //Check if Twitter Id length is correct
                    if (sender.Length > 16)
                        throw new Exception("Twitter ID cannot be greater than 16 characters");
                    //If everything passed the filter return true correct message format
                    return true;
                //Email message
                case "e":
                    //Check sender is an email address
                    if (!isEmail.IsMatch(sender))
                        throw new Exception("Please enter a correct email address (e.g bob@gmail.com)");
                    //Check message length is correct for email
                    if (messageBody.Length > 1028)
                        throw new Exception("Message text is larger than 1028 characters");
                    //Check if the subject is 20 characters
                    if (subject.Length > 20)
                        throw new Exception("Subject cant be larger than 20 characters");
                    //If everything passed the filter return true correct message format
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Method used to find if a message already exists in system.
        /// </summary>
        /// <returns>Returns true if already exists, false is not</returns>
        public bool AlreadyExist(int messageId)
        {
            return MessageList.Any(m => m.MessageID == messageId);
        }


        /// <summary>
        /// Method used to output a message to JSON File in JSON format
        /// </summary>
        /// <param name="m">Message to output to JSON</param>
        /// <param name="outputDestination">Destination of JSON output file.</param>
        /// <returns>Returns true if successful.</returns>
        public bool WriteMessageToJSON(Message m, out string outputDestination)
        {
            //String to hold JSON output
            string jsonData = string.Empty;
            
            //String to set output file name
            string outputFilename = m.MessageID + ".txt";

            //Output destination
            outputDestination = Environment.CurrentDirectory + "\\" + outputFilename;

            //Check type of message
            //If Tweet
            if (m.GetType() == typeof (Tweet))
            {
                //Seralize Tweet to JSON
                jsonData = JsonConvert.SerializeObject(((Tweet) m));
            }
            //If SMS Message
            else if (m.GetType() == typeof (SMSMessage))
            {
                //Serialize SMS Message to JSON
                jsonData = JsonConvert.SerializeObject(((SMSMessage) m));
            }
            //If Standard Email
            else if (m.GetType() == typeof (StandardEmail))
            {
                //Serialize Standard Email to JSON
                jsonData = JsonConvert.SerializeObject(((StandardEmail) m));
            } 
            //If SIR Email
            else if (m.GetType() == typeof (SIREmail))
            {
                //Serialize SIR Email to JSON
                jsonData = JsonConvert.SerializeObject(((SIREmail) m));
            }

            //If not correct message object return false
            if (jsonData == string.Empty)
                return false;

            //Error handling for output file writing
            try
            {
                //Use streamwriter to output JSON File
                using (StreamWriter sw = new StreamWriter(outputDestination))
                {
                    sw.Write(jsonData);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Writing JSON FIle!: " + ex.Message);
            }

            //If all successful return true
            return true;
        }


        
    }
}
