using Xamarin.Forms;

namespace ValueWallet.Models
{
    public class LocalDeviceInfo
    {
        public LocalDeviceInfo(Platform platform)
        {
            CurrentPlatform = platform;
        }

        //single ton
        public static LocalDeviceInfo CurrentDevice { get; set; }

        public Platform CurrentPlatform { get; private set; }

        public bool IsAuthBioEnable { get; set; }
        public bool IsNotchIosDevice { get; set; } = false;
        public bool IsSupportAuthBio { get; set; }
        public bool IsSupportSecureStorage { get; set; }

        public string OSCode { get; set; }
        public string Brand { get; set; }
        public string OSVersion { get; set; }
        public string AppVersion { get; set; }
        public string DeviceModel { get; set; }
        public string DeviceID { get; set; }
        public string DeviceSerial { get; set; }

        public double ScreenScale { get; set; }
        public double ScreenWidth { get; set; }
        public double ScreenWidthPixels { get; set; }
        public double ScreenHeightPixels { get; set; }
        public double ScreenHeight { get; set; }

    }
}
