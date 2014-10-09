//
//  DropboxMiniHackViewController.cs
//
//  Author:
//       Safwan Choudhury <safwan.choudhury@quanser.com>
//
//  Copyright (c) 2014 Quanser Inc.
//
//
using System;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;

using Foundation;
using UIKit;

namespace DropboxMiniHack
{
    public partial class DropboxMiniHackViewController : UIViewController
    {
        private const string DROPBOX_API_KEY = "9M9UM2hJAV8AAAAAAAAjCZAz5r86fCMs3HUuuF-NzBBk6Bng56JRqjC1NYmPlKLs"; 

        private static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public DropboxMiniHackViewController(IntPtr handle) : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
			
            // Release any cached data, images, etc that aren't in use.
        }

        #region View lifecycle
		
        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
			
            // Perform any additional setup after loading the view, typically from a nib.
            var button = new UIButton(UIButtonType.RoundedRect)
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
            }; 

            button.SetTitle(@"Upload to Dropbox", UIControlState.Normal); 
            button.TouchUpInside += HandleTouchUpInside;

            View.AddSubview(button); 

            View.AddConstraint(NSLayoutConstraint.Create(View, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, button, NSLayoutAttribute.CenterX, 1.0f, 0.0f)); 
            View.AddConstraint(NSLayoutConstraint.Create(View, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, button, NSLayoutAttribute.CenterY, 1.0f, 0.0f)); 
        }

        private async void HandleTouchUpInside (object sender, EventArgs e)
        {
            var request = new HttpRequestMessage {
                // See documentation here: https://www.dropbox.com/developers/core/docs#files_put
                RequestUri = new Uri(String.Format(@"https://api-content.dropbox.com/1/files_put/auto/{0}.txt", Guid.NewGuid().ToString().ToUpper())),
                Content = new StringContent ("Dropbox API Mini Hack"),
                Method = HttpMethod.Put
            };
            request.Headers.Authorization = new AuthenticationHeaderValue ("Bearer", DROPBOX_API_KEY);
            var result = await new HttpClient().SendAsync(request);
            var message = result.IsSuccessStatusCode ? "Success!" : "Failure: " + result.StatusCode;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }
		
        #endregion
    }
}

