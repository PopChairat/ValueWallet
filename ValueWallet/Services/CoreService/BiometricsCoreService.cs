using System;
using System.Threading.Tasks;
using ValueWallet.Domain.Entities;
using ValueWallet.Models;
using ValueWallet.Services.IRuntimeService;
using Xamarin.Forms;

namespace ValueWallet.Services.CoreService
{
    public class BiometricsCoreService
    {
        public static Task<bool> AuthenticateBiometric()
        {
            return DependencyService.Get<IBiometricsService>().AuthenticateBiometric();
        }

        public static Task<BioProfileEntity> GetPassword()
        {
            return DependencyService.Get<IBiometricsService>().GetPassword();
        }

        public static bool IsBioPermissionGranted()
        {
            return DependencyService.Get<IBiometricsService>().IsBioPermissionGranted();
        }

        public static LoginBy IsCanUseBiometric()
        {
            return DependencyService.Get<IBiometricsService>().DeviceCanLoginBy();
        }

        public static void RemoveUserLogin()
        {
            DependencyService.Get<IBiometricsService>().RemoveUserLogin();
        }

        public static void SetUserLogin(string username, string password, int loginType)
        {
            DependencyService.Get<IBiometricsService>().SetUserLogin(username, password, loginType);
        }
    }
}
