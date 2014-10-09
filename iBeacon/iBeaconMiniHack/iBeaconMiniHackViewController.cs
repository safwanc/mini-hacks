//
//  iBeaconMiniHackViewController.cs
//
//  Author:
//       Safwan Choudhury <safwan.choudhury@quanser.com>
//
//  Copyright (c) 2014 Quanser Inc.
//
//
using System;
using System.Drawing;

using Foundation;
using UIKit;
using CoreLocation; 

namespace iBeaconMiniHack
{
    public partial class iBeaconMiniHackViewController : UIViewController
    {
        static readonly string uuid = "B9407F30-F5F8-466E-AFF9-25556B57FE6D";
        static readonly string beaconId = "myEstimoteBeacon";

        CLLocationManager locationManager;
        CLProximity previousProximity;
        string message;

        public iBeaconMiniHackViewController(IntPtr handle) : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
			
            // Release any cached data, images, etc that aren't in use.
        }

        #region View lifecycle
		
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var beaconUUID = new NSUuid (uuid);
            var beaconRegion = new CLBeaconRegion (beaconUUID, beaconId);

            beaconRegion.NotifyEntryStateOnDisplay = true;
            beaconRegion.NotifyOnEntry = true;
            beaconRegion.NotifyOnExit = true;

            locationManager = new CLLocationManager ();
            locationManager.RequestWhenInUseAuthorization ();

            locationManager.DidStartMonitoringForRegion += (object sender, CLRegionEventArgs e) => {
                locationManager.RequestState (e.Region);
            };

            locationManager.RegionEntered += (object sender, CLRegionEventArgs e) => {
                if (e.Region.Identifier == beaconId) {
                    Console.WriteLine ("beacon region entered");
                }
            };

            locationManager.DidDetermineState += (object sender, CLRegionStateDeterminedEventArgs e) => {

                switch (e.State) {
                    case CLRegionState.Inside:
                        Console.WriteLine ("region state inside");
                        break;
                    case CLRegionState.Outside:
                        Console.WriteLine ("region state outside");
                        break;
                    case CLRegionState.Unknown:
                    default:
                        Console.WriteLine ("region state unknown");
                        break;
                }
            };  

            locationManager.DidRangeBeacons += (object sender, CLRegionBeaconsRangedEventArgs e) => {
                if (e.Beacons.Length > 0) 
                {
                    int major, minor; 

                    foreach (var beacon in e.Beacons) 
                    {
                        major = (int) beacon.Major; 
                        minor = (int) beacon.Minor; 

//                        Console.WriteLine("Found Beacon with Major = {0} Minor = {1}", major, minor);

                        // Lock on to a specific iBeacon since there are many of them in the Darwin lounge
                        if (major == 51093 && minor == 43988)
                        {
                            switch (beacon.Proximity) {
                                case CLProximity.Immediate:
                                    message = "Immediate";
                                    break;
                                case CLProximity.Near:
                                    message = "Near";
                                    break;
                                case CLProximity.Far:
                                    message = "Far";
                                    break;
                                case CLProximity.Unknown:
                                    message = "Unknown";
                                    break;
                            }

                            if (previousProximity != beacon.Proximity) {
                                Console.WriteLine (message);
                            }

                            previousProximity = beacon.Proximity;
                        }
                    }
                }
            };

            locationManager.StartMonitoring (beaconRegion);
            locationManager.StartRangingBeacons (beaconRegion);


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

