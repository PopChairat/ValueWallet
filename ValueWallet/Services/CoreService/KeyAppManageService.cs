using System.Threading.Tasks;
using Newtonsoft.Json;
using ValueWallet.Models;
using Xamarin.Essentials;

namespace ValueWallet.Services.CoreService
{
    public static class KeyAppManageService
    {
        public async static void SetData(KeyAppType keyName, string value)
        {
            string keyNameSt = keyName.ToString();

            if (LocalDeviceInfo.CurrentDevice.IsSupportSecureStorage)
            {
                await SecureStorage.SetAsync(keyNameSt, value);
            }
            else
            {
                Preferences.Set(keyNameSt, value);
            }

        }

        public async static void SetEntityData(KeyAppType keyName, object value)
        {
            string data = JsonConvert.SerializeObject(value);
            string keyNameSt = keyName.ToString();

            if (LocalDeviceInfo.CurrentDevice.IsSupportSecureStorage)
            {
                await SecureStorage.SetAsync(keyNameSt, data);
            }
            else
            {
                Preferences.Set(keyNameSt, data);
            }

        }

        public async static ValueTask<string> GetData(KeyAppType keyName)
        {
            string value;
            string keyNameSt = keyName.ToString();

            if (LocalDeviceInfo.CurrentDevice.IsSupportSecureStorage)
            {
                value = await SecureStorage.GetAsync(keyNameSt);
            }
            else
            {
                value = Preferences.Get(keyNameSt, null);
            }

            return value;
        }

        public async static ValueTask<T> GetEntityData<T>(KeyAppType keyName)
        {
            string value;
            string keyNameSt = keyName.ToString();

            if (LocalDeviceInfo.CurrentDevice.IsSupportSecureStorage)
            {
                value = await SecureStorage.GetAsync(keyNameSt);
            }
            else
            {
                value = Preferences.Get(keyNameSt,null);
            }

            T data = JsonConvert.DeserializeObject<T>(value);

            return data;
        }

        public static void RemoveData(KeyAppType keyName)
        {
            string keyNameSt = keyName.ToString();

            if (LocalDeviceInfo.CurrentDevice.IsSupportSecureStorage)
            {
                SecureStorage.Remove(keyNameSt);
            }
            else
            {
                Preferences.Remove(keyNameSt);
            }
        }
    }
}
