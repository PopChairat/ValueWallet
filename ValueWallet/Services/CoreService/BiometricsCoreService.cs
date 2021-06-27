using System;
using System.Threading.Tasks;
using ValueWallet.Domain.Entities;
using ValueWallet.Models;
using ValueWallet.Services.IService;
using Xamarin.Forms;

namespace ValueWallet.Services.CoreService
{
    public class BiometricsService
    {
        public static Task<BioProfileEntity> GetPassword()
        {
            return DependencyService.Get<IBiometricsService>().GetSecretBio();
        }

        public static bool IsBioPermissionGranted()
        {
            return DependencyService.Get<IBiometricsService>().IsBioPermissionGranted();
        }

        public static void RemoveUserLogin()
        {
            DependencyService.Get<IBiometricsService>().RemoveUserLogin();
        }

        public static void SetUserLogin(string username, string password, LoginBy loginType)
        {
            try
            {
                BioProfileEntity userInfo = new BioProfileEntity() { Username = username, Password = password, LoginBy = loginType };
                DependencyService.Get<IBiometricsService>().SetUserLogin(userInfo);
            }
            catch(Exception ex)
            {
               
            }
        }
    }
}
