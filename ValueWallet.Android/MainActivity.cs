using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Views;
using ValueWallet.Models;
using Android.Util;
using Android.Content;
using AndroidX.Core.Hardware.Fingerprint;
using Android;

namespace ValueWallet.Droid
{
    [Activity(Label = "Value Wallet", Theme = "@style/splashscreen", Icon = "@mipmap/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, LaunchMode = LaunchMode.SingleTop)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Window.RequestFeature(WindowFeatures.NoTitle);
            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.AddFlags(WindowManagerFlags.LayoutInOverscan);
            Window.SetStatusBarColor(Android.Graphics.Color.Transparent);

            base.SetTheme(Resource.Style.MainTheme);
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.P)
            {
                Window.Attributes.LayoutInDisplayCutoutMode
                = LayoutInDisplayCutoutMode.ShortEdges;
            }

            Context context = ;

            DeviceInfo.CurrentDevice = GetDeviceInfo(context);

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private DeviceInfo GetDeviceInfo(Context context)
        {
            DeviceInfo deviceInfo = new(Platform.Android);

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

            if(versionOS >= BuildVersionCodes.M)
            {
                //BiometricPrompt
            }
            else
            {
                // Using the Android Support Library v4
                FingerprintManagerCompat fingerprintManager = FingerprintManagerCompat.From(context);

                deviceInfo.IsSupportAuthBio = fingerprintManager.IsHardwareDetected;

                // The context is typically a reference to the current activity.
                Permission permissionResult = AndroidX.Core.Content.ContextCompat.CheckSelfPermission(context, Manifest.Permission.UseFingerprint);
                if (permissionResult == Permission.Granted && fingerprintManager.HasEnrolledFingerprints)
                {
                    // Permission granted - go ahead and start the fingerprint scanner.
                    deviceInfo.IsAuthBioEnable = true;
                }
                else
                {
                    // No permission. Go and ask for permissions and don't start the scanner. See
                    // https://developer.android.com/training/permissions/requesting.html

                    deviceInfo.IsAuthBioEnable = false;
                }

            }

            return deviceInfo;

        }
    }
}
