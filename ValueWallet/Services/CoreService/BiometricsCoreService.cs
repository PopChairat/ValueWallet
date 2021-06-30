using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using ValueWallet.Domain.Entities;
using ValueWallet.Models;
using Xamarin.Forms;

namespace ValueWallet.Services.CoreService
{
    public class BiometricsService
    {
        public async static Task<BioProfileEntity> GetUserLogin()
        {
            string userEncrypt = await KeyAppManageService.GetData(KeyAppType.UserInfo);
            string user = CryptoManagerApp.DecryptData(userEncrypt);
            BioProfileEntity bioProfile = JsonConvert.DeserializeObject<BioProfileEntity>(user);
            return bioProfile;
        }

        public async static ValueTask<bool> IsPassAuthUser()
        {
            bool result = false;

            FingerprintAuthenticationResult authResult = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Heads up!", "I would like to use your biometrics, please!"));

            if (authResult.Authenticated)
                result = true;

            return result;
        }

        public static void RemoveUserLogin()
        {
            KeyAppManageService.RemoveData(KeyAppType.UserInfo);
        }

        public static void SetUserLogin(string username, string password, LoginBy loginType)
        {
            BioProfileEntity userInfo = new() { Username = username, Password = password, LoginBy = loginType };
            string user = JsonConvert.SerializeObject(userInfo);
            string userEncrypt = CryptoManagerApp.EncryptData(user);
            KeyAppManageService.SetData(KeyAppType.UserInfo, userEncrypt);
        }
    }
}
