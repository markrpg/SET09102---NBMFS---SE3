using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NapierBankMessageFilteringWPF
{
    /// <summary>
    /// Interaction logic for TrendingWindow.xaml
    /// </summary>
    public partial class TrendingWindow : Window
    {
        /// <summary>
        /// Singlton reference for filtering service
        /// </summary>
        private NBMFilteringService filteringService;

        public TrendingWindow(NBMFilteringService filteringservice)
        {
            //Initializes Trending window WPF elements
            InitializeComponent();

            //Set filtering service reference
            filteringService = filteringservice;

            //Method to setup trending lists
            SetupTrendingList();
        }


        /// <summary>
        /// Method used to setup trending lists (hashtags,mentions,SIR codes)
        /// </summary>
        public void SetupTrendingList()
        {
            //Hashtags
            foreach (string hashtag in filteringService.GetHashtagTrendingList())
                lbHashtagsTrending.Items.Add(hashtag);

            //Mentions
            foreach (string mentions in filteringService.GetMentionsTrendingList())
                lbMentionsTrending.Items.Add(mentions);

            //SIR Codes
            foreach (string sircode in filteringService.GetSirCodesTrendingList())
                lbTrendingIncidents.Items.Add(sircode);
        }

        /// <summary>
        /// Simple button event to close this window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            //Close window
            this.Close();
        }
    }
}
