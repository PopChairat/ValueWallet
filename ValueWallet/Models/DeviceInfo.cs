namespace ValueWallet.Models
{
    public class DeviceInfo
    {
        public DeviceInfo(Platform platform)
        {
            CurrentPlatform = platform;
        }

        //single ton
        public static DeviceInfo CurrentDevice { get; set; }

        public Platform CurrentPlatform { get; private set; }

        public bool IsNotchIosDevice { get; set; } = false;
        public bool IsSupportAuthBio { get; set; }
        public bool IsAuthBioEnable { get; set; }

        public string OSCode { get; set; }
        public string Brand { get; set; }
        public string OSVersion { get; set; }
        public string AppVersion { get; set; }
        public string DeviceModel { get; set; }

        public double ScreenScale { get; set; }
        public double ScreenWidth { get; set; }
        public double ScreenWidthPixels { get; set; }
        public double ScreenHeightPixels { get; set; }
        public double ScreenHeight { get; set; }


    }
}
