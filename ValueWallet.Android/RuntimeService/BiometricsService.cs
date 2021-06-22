using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using AndroidX.Core.Content;
using ValueWallet.Models;
using ValueWallet.Services.IRuntimeService;

namespace ValueWallet.Droid.RuntimeService
{
    public class BiometricsService : IBiometricsService
    {
        private static byte[] initializationVector;

        Context context = Application.Context;

        public Task<bool> AuthenticateBiometric()
        {
            throw new System.NotImplementedException();
        }

        public Task<BioProfileEntity> GetPassword()
        {
            throw new System.NotImplementedException();
        }

        public bool IsBioPermissionGranted()
        {
            // The context is typically a reference to the current activity.
            Android.Content.PM.Permission permissionResult = ContextCompat.CheckSelfPermission(context, Manifest.Permission.UseFingerprint);
            if (permissionResult == Android.Content.PM.Permission.Granted)
            {
                return true;
            }
            else
                return false;
        }

        public void RemoveUserLogin()
        {
            throw new System.NotImplementedException();
        }

        public void SetUserLogin(string username, string password, int loginType)
        {
            throw new System.NotImplementedException();
        }
    }
}
