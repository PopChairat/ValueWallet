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
            LocalDeviceInfo.CurrentDevice = GetLocalDeviceInfo();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private LocalDeviceInfo GetLocalDeviceInfo()
        {
            LocalDeviceInfo deviceInfo = new(Models.Platform.iOS);

            //Check Authenticate Biometrics
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                LocalAuthentication.LAContext context = new LocalAuthentication.LAContext();
                NSError authError;
                deviceInfo.IsSupportAuthBio = context.CanEvaluatePolicy(LocalAuthentication.LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out authError);
                deviceInfo.IsAuthBioEnable = (deviceInfo.IsSupportAuthBio || (LocalAuthentication.LAStatus)Convert.ToInt16(authError.Code) != LocalAuthentication.LAStatus.TouchIDNotAvailable);
            }

            Task.Run(async () =>
            {
                deviceInfo.IsSupportSecureStorage = await IsSupportSecureStorage();

            });

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


            string deviceID = UIDevice.CurrentDevice.IdentifierForVendor.AsString();
            string defaultDeviceID = "";
           
            if (!string.IsNullOrEmpty(defaultDeviceID) && GetDeviceID(ref defaultDeviceID))
                deviceInfo.DeviceID = defaultDeviceID;
            else
            {
                deviceInfo.DeviceID = deviceID;
                SetDeviceID(deviceID);
            }

            return deviceInfo;
        }

        private const NSStringEncoding Encoding = NSStringEncoding.UTF8;
        private const string DeviceServiceId = "com.chairat.valuewallet.deviceid.service";
        private const string DeviceAccountName = "com.chairat.valuewallet.deviceid.account";
        private bool GetDeviceID(ref string deviceID)
        {
            bool result;
            Security.SecStatusCode code;

            // Query the record.
            Security.SecRecord queryRec = new Security.SecRecord(Security.SecKind.GenericPassword) { Service = DeviceServiceId, Label = DeviceServiceId, Account = DeviceAccountName, Synchronizable = true };
            queryRec = Security.SecKeyChain.QueryAsRecord(queryRec, out code);
            if (code == Security.SecStatusCode.Success && queryRec != null && queryRec.Generic != null)
            {
                deviceID = NSString.FromData(queryRec.Generic, Encoding);
                result = true;
            }
            else
            {
                deviceID = "";
                result = false;
            }

            System.Diagnostics.Debug.WriteLine("GetDeviceID Security.SecStatusCode =>" + code);

            return result;
        }
        private void SetDeviceID(string deviceID)
        {
            if (deviceID == null)
                throw new ArgumentNullException("deviceID is null");

            Security.SecStatusCode code = Security.SecKeyChain.Add(new Security.SecRecord(Security.SecKind.GenericPassword)
            {
                Service = DeviceServiceId,
                Label = DeviceServiceId,
                Account = DeviceAccountName,
                ValueData = NSData.FromString(deviceID),
                Generic = NSData.FromString(deviceID),
                Accessible = Security.SecAccessible.AlwaysThisDeviceOnly,
                Synchronizable = true
            });

            System.Diagnostics.Debug.WriteLine("SaveDeviceID Security.SecStatusCode =>" + code);

        }
        private async ValueTask<bool> IsSupportSecureStorage()
        {
            try
            {
                string oauthToken = await SecureStorage.GetAsync("oauth_token");

                return true;
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
                return false;
            }
        }
    }
}
