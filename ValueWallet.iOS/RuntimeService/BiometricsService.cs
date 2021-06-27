using System;
using System.Threading.Tasks;
using ValueWallet.Models;
using ValueWallet.Services.IService;
using Xamarin.Forms;

[assembly: Dependency(typeof(ValueWallet.iOS.RuntimeService.BiometricsService))]
namespace ValueWallet.iOS.RuntimeService
{
    public class BiometricsService : IBiometricsService
    {
        //encrypt
        public void SetUserLogin(BioProfileEntity bioProfile)
        {
            throw new NotImplementedException();
        }

        //decrypt
        public Task<BioProfileEntity> GetSecretBio()
        {
            throw new NotImplementedException();
        }

        //check permission
        public bool IsBioPermissionGranted()
        {
            throw new NotImplementedException();
        }

        //reset data
        public void RemoveUserLogin()
        {
            throw new NotImplementedException();
        }
    }
}
