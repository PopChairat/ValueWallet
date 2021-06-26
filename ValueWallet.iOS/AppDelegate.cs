using System;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using ValueWallet.Models;
using Xamarin.Essentials;

namespace ValueWallet.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
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
            global::Xamarin.Forms.Forms.Init();
            LocalDeviceInfo.CurrentDevice = GetDeviceInfo();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private LocalDeviceInfo GetDeviceInfo()
        {
            LocalDeviceInfo deviceInfo = new(Models.Platform.iOS);

            string sysVer = UIDevice.CurrentDevice.SystemVersion;
            deviceInfo.OSVersion = $"iOS {sysVer}";
            deviceInfo.OSCode = sysVer;
            deviceInfo.DeviceModel = RuntimeService.DeviceHardware.Model;
            deviceInfo.AppVersion = RuntimeService.DeviceHardware.Version;
            deviceInfo.Brand = "Apple";
            //deviceInfo.AppVersion = NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
            //deviceInfo.DeviceName = UIDevice.CurrentDevice.Name;

            deviceInfo.ScreenScale = UIScreen.MainScreen.Scale;
            deviceInfo.ScreenWidth = UIScreen.MainScreen.Bounds.Size.Width;
            deviceInfo.ScreenHeight = UIScreen.MainScreen.Bounds.Size.Height;
            deviceInfo.ScreenWidthPixels = deviceInfo.ScreenWidth * deviceInfo.ScreenScale;
            deviceInfo.ScreenHeightPixels = deviceInfo.ScreenHeight * deviceInfo.ScreenScale;

            //Check Authenticate Biometrics
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                LocalAuthentication.LAContext context = new LocalAuthentication.LAContext();
                NSError authError;
                deviceInfo.IsSupportAuthBio = context.CanEvaluatePolicy(LocalAuthentication.LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out authError);
                deviceInfo.IsAuthBioEnable = (deviceInfo.IsSupportAuthBio || (LocalAuthentication.LAStatus)Convert.ToInt16(authError.Code) != LocalAuthentication.LAStatus.TouchIDNotAvailable);
            }


            return deviceInfo;
        }

    }
}
