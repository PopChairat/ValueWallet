using ValueWallet.Models;

namespace ValueWallet.Services.CoreService
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
        public string OSVersion { get; set; }
        public string AppVersion { get; set; }
        public string Model { get; set; }
        public string DeviceName { get; set; }
    }
}
