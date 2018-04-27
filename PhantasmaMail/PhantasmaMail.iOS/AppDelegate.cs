﻿using FFImageLoading.Forms.Touch;
using FFImageLoading.Svg.Forms;
using Foundation;
using RoundedBoxView.Forms.Plugin.iOSUnified;
using Syncfusion.ListView.XForms.iOS;
using UIKit;
using Xfx;

namespace PhantasmaMail.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			Rg.Plugins.Popup.Popup.Init();
            XfxControls.Init();
            Xamarin.Forms.Forms.Init();
            RoundedBoxViewRenderer.Init();
            SfListViewRenderer.Init();
            XFGloss.iOS.Library.Init();
            CachedImageRenderer.Init();
            var ignore = typeof(SvgCachedImage);
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
