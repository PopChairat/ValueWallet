using Microsoft.Extensions.DependencyInjection;
using ValueWallet.Domain.IServices;
using ValueWallet.Domain.Services;

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

        //public T DecryptData<T>()
        //{
        //    object PropertyValue =
        //        if (Convert.GetTypeCode(PropertyValue) != TypeCode.Object)
        //    {
        //        string StringValue = Convert.ToString(PropertyValue);

        //    }
        //}

        private static void InitialService()
        {
            IServiceCollection services = new ServiceCollection()
                .AddSingleton<ICryptographyManager>(new CryptographyManager());

            serviceProvider = services.BuildServiceProvider();
        }
    }
}
