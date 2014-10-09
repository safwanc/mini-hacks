using System;
using System.Collections.Generic; 
using Xamarin;

namespace XamarinInsights
{
	public static class Analytics
	{
		public static void UserAuthenticated(User user)
		{
			//TODO: Xamarin.Insights.Identify()
			//The UID can be any identifier you use to identify a user. Email can be a good option
			//The Table parameter allows you to send any other traits about your users.
			//Check Insights.Traits for some reccomended/supported traits.
			//Feel free to track any additional data you need to about your user

            var table = new Dictionary<string, string>()
            {
                { Xamarin.Insights.Traits.Name, user.Name }, 
                { Xamarin.Insights.Traits.Email, user.Email },
                { Xamarin.Insights.Traits.Avatar, user.ImageUrl },
            }; 

            Xamarin.Insights.Identify(user.Email, table); 
		}

		public static void LogKeyPress(string key)
		{
			//TODO:Xamarin.Insights.Track();
			//Track using the identifier KeyPress
			//Pass in the actual key press by using the dictionary with a key of "Key"

            var table = new Dictionary<string, string>()
            {
                { "Key", key }
            };  

            Xamarin.Insights.Track("Keypress", table); 
		}

		public static void LogPageView(string pageView)
		{
			//TODO:Xamarin.Insights.Track();

            var table = new Dictionary<string, string>()
            {
                { "Page View", pageView }
            };  

            Xamarin.Insights.Track("Keypress", table); 
		}

		public static void LogNewLevel(int level, string currentCombination, double timeRemaining)
		{
			//TODO:Xamarin.Insights.Track();
			//Pass in the other parameters using the dictionary

            var table = new Dictionary<string, string>()
            {
                { "Level", Convert.ToString(level) },
                { "Current Combination", currentCombination },
                { "Time Remaining", Convert.ToString(timeRemaining) },
            };  

            Xamarin.Insights.Track("New Level", table); 
		}

        public static void LogException(Exception e)
        {
            Xamarin.Insights.Report(e); 
        }
	}
}

