using Microsoft.Extensions.DependencyInjection;
using ValueWallet.Domain.IServices;
using ValueWallet.Domain.Services;
using ValueWallet.Models;

namespace ValueWallet.Services.CoreService
{
    public class CryptoManagerApp
    {
        private static ServiceProvider serviceProvider;

        public static string EncryptData(string text)
        {
            if (serviceProvider == null)
                InitialService();

            string value = serviceProvider.GetService<ICryptographyManager>().EncryptData(text);

            return value;
        }

        public static string DecryptData(string text)
        {
            if (serviceProvider == null)
                InitialService();

            string value = serviceProvider.GetService<ICryptographyManager>().DecryptData(text);

            return value;
        }

        private static void InitialService()
        {
            IServiceCollection services = new ServiceCollection()
                .AddSingleton<ICryptographyManager>(new CryptographyManager(LocalDeviceInfo.CurrentDevice.DeviceID));
            serviceProvider = services.BuildServiceProvider();
        }
    }
}
