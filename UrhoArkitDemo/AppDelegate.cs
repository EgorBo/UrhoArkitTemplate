using Foundation;
using UIKit;
using Urho;
using Urho.iOS;
using ObjCRuntime;

namespace UrhoArkitDemo
{
	[Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
		public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // create a new window instance based on the screen size
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            // If you have defined a root view controller, set it here:
            Window.RootViewController = new UIViewController();

            var surface = new UrhoSurface(UIScreen.MainScreen.Bounds);
            Window.RootViewController.View.AddSubview(surface);

            // make the window visible
            Window.MakeKeyAndVisible();

            surface.Show<UrhoApp>(new ApplicationOptions {
                Orientation = ApplicationOptions.OrientationType.Landscape,
                ResourcePaths = new [] { "BookshelfData", "BaseArkitData" },
				DelayedStart = Runtime.Arch == Arch.DEVICE // Update() will be called for each new ARFrame (for simulators it's should be 'false')
            });

            return true;
        }
    }
}


