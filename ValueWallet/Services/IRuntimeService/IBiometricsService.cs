using System.Threading.Tasks;
using ValueWallet.Domain.Entities;
using ValueWallet.Models;

namespace ValueWallet.Services.IRuntimeService
{
    public interface IBiometricsService
    {
        bool IsBioPermissionGranted();
        Task<bool> AuthenticateBiometric();
        Task<BioProfileEntity> GetPassword();
        void SetUserLogin(string username, string password, int loginType);
        void RemoveUserLogin();
    }
}
