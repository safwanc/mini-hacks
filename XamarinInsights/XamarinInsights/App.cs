using System;
using Xamarin.Forms;

namespace XamarinInsights
{
	public class App
	{
        public const string InsightsApiKey = "238902ac5ea9a690ffc182fa85a0ac5b7ecbf900";
		public static Page GetMainPage ()
		{	
			return new UserInformationPage ();
		}
		public static Action<Action> RunOnMainThread;
	}
}

