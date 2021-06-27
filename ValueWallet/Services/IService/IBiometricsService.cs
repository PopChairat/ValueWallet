using System.Threading.Tasks;
using ValueWallet.Models;

namespace ValueWallet.Services.IService
{
    public interface IBiometricsService
    {
        bool IsBioPermissionGranted();
        Task<BioProfileEntity> GetSecretBio();
        void SetUserLogin(BioProfileEntity bioProfile);
        void RemoveUserLogin();
    }
}
