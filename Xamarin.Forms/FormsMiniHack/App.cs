//
//  App.cs
//
//  Author:
//       Safwan Choudhury <safwan.choudhury@quanser.com>
//
//  Copyright (c) 2014 Quanser Inc.
//
//
using System;
using Xamarin.Forms;

namespace FormsMiniHack
{
    public class App
    {
        public static Image BallImage { get; private set; }
        public static Button ShakeButton { get; private set; }
        public static Label MessageLabel { get; private set; }

        private static Random random = new Random(); 

        public static Page GetMainPage()
        {	
            BallImage = new Image()
            {
                Source = "8Ball.jpg"
            };

            ShakeButton = new Button()
            {
                Text = "Shake",
            };

            MessageLabel = new Label(); 

            ShakeButton.Clicked += (sender, e) => 
            {
                MessageLabel.Text = options[random.Next(options.Length - 1)]; 
            };

            return new ContentPage
            {
                Content = new StackLayout
                {
                    Spacing = 20, Padding = 50,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Children =
                    {
                        BallImage,
                        ShakeButton,
                        MessageLabel
                    }
                }
            };
        }

        private static string[] options = {" It is certain"
            , " It is decidedly so"
            , " Without a doubt"
            , " Yes definitely"
            , " You may rely on it"
            , " As I see it, yes"
            , " Most likely"
            , " Outlook good"
            , " Yes"
            , " Signs point to yes"

            , " Reply hazy try again"
            , " Ask again later"
            , " Better not tell you now"
            , " Cannot predict now"
            , " Concentrate and ask again"

            , " Don't count on it"
            , " My reply is no"
            , " My sources say no"
            , " Outlook not so good"
            , " Very doubtful "
        };
    }
}

