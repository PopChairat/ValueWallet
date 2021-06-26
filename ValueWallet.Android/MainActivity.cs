using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Views;
using ValueWallet.Models;
using Android.Util;
using Plugin.Fingerprint;
using System.Threading.Tasks;
using Android.Hardware.Fingerprints;
using Android.Content;
using Xamarin.Essentials;
using ValueWallet.Droid.RuntimeService;

namespace ValueWallet.Droid
{
    [Activity(Label = "Value Wallet", Theme = "@style/splashscreen", Icon = "@mipmap/icon", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, LaunchMode = LaunchMode.SingleTop)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.SetTheme(Resource.Style.MainTheme);
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            CrossFingerprint.SetCurrentActivityResolver(() => Xamarin.Essentials.Platform.CurrentActivity);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.P)
            {
                Window.Attributes.LayoutInDisplayCutoutMode
                = LayoutInDisplayCutoutMode.Never;
            }

            LocalDeviceInfo.CurrentDevice = GetDeviceInfo();
            InitialApp();

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private LocalDeviceInfo GetDeviceInfo()
        {
            LocalDeviceInfo deviceInfo = new(Models.Platform.Android);

            BuildVersionCodes versionOS = Build.VERSION.SdkInt;

            deviceInfo.OSVersion = $"Android {Build.VERSION.Release}";
            deviceInfo.OSCode = Convert.ToString((int)versionOS);
            PackageInfo packageInfopInfo = PackageManager.GetPackageInfo(PackageName, 0);
            deviceInfo.AppVersion = packageInfopInfo.VersionName;
            deviceInfo.Brand = Build.Brand;
            deviceInfo.DeviceModel = Build.Model.StartsWith(Build.Brand, StringComparison.InvariantCulture) ? Build.Model : (Build.Brand + " " + Build.Model);

            DisplayMetrics metrics = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(metrics);
            deviceInfo.ScreenScale = metrics.Density;
            deviceInfo.ScreenWidthPixels = metrics.WidthPixels;
            deviceInfo.ScreenHeightPixels = metrics.HeightPixels;
            deviceInfo.ScreenWidth = metrics.WidthPixels / metrics.Density;
            deviceInfo.ScreenHeight = metrics.HeightPixels / metrics.Density;

            //Check Authenticate Biometrics
            deviceInfo.IsSupportAuthBio = IsSupportBioCheck();

            Task.Run(async () =>
            {
                deviceInfo.IsAuthBioEnable = await CrossFingerprint.Current.IsAvailableAsync();

                deviceInfo.IsSupportSecureStorage = await IsSupportSecureStorage();

                InitialKeyApp();
            });

            return deviceInfo;

        }

        private bool IsSupportBioCheck()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                // Using API level 23:
                FingerprintManager fingerprintManager = GetSystemService(Context.FingerprintService) as FingerprintManager;

                if (!fingerprintManager.IsHardwareDetected || !fingerprintManager.HasEnrolledFingerprints)
                {
                    return false;
                }

            }
            else
            {
                return false;
            }

            KeyguardManager keyguardManager = (KeyguardManager)GetSystemService(Context.KeyguardService);
            if (!keyguardManager.IsKeyguardSecure)
            {
                return false;
            }

            return true;
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

        private async void InitialKeyApp()
        {
            if (LocalDeviceInfo.CurrentDevice.IsSupportSecureStorage)
            {
                await SecureStorage.SetAsync(KeyAppType.KeySecret.ToString(), "IamChairat");
            }
        }
    }
}
