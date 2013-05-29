using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Rahman_Tweets
{
    public partial class MainPage : PhoneApplicationPage
    {
        int tweetcount = 0;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            WebClient tweets = new WebClient();
            tweets.DownloadStringCompleted += new DownloadStringCompletedEventHandler(tweets_DownComp);
            tweets.DownloadStringAsync(new Uri("http://api.twitter.com/1/statuses/user_timeline.xml?screen_name=arrahman"));
            tweetcount = 20;

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            WebClient tweets = new WebClient();
            tweets.DownloadStringCompleted += new DownloadStringCompletedEventHandler(tweets_DownComp);
            tweets.DownloadStringAsync(new Uri("http://api.twitter.com/1/statuses/user_timeline.xml?screen_name=arrahman"));
            tweetcount = 20;
        }

        private void Update_Click_load_more(object sender, RoutedEventArgs e)
        {
            tweetcount += 20;
            WebClient tweets = new WebClient();
            tweets.DownloadStringCompleted += new DownloadStringCompletedEventHandler(tweets_DownComp);
            tweets.DownloadStringAsync(new Uri("http://api.twitter.com/1/statuses/user_timeline.xml?screen_name=arrahman&count=" + tweetcount));
        }

        void tweets_DownComp(object sender, DownloadStringCompletedEventArgs dsce)
        {
            if (dsce.Error != null)
                return;
            XElement xmlTweets = XElement.Parse(dsce.Result);

            tweetlist.ItemsSource = from tweet in xmlTweets.Descendants("status")
                                    select new tweetitem
                                    {
                                        ImageSource = tweet.Element("user").Element("profile_image_url").Value,
                                        Message = tweet.Element("text").Value,
                                        UserName = "@"+tweet.Element("user").Element("screen_name").Value

                                    };
        }
    }

    public class tweetitem
    {
        public string UserName { get; set; }
        public string Message { get; set; }
        public string ImageSource { get; set; }

    }

}