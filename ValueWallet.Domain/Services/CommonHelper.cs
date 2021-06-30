
namespace ValueWallet.Domain.Services
{
    public static class CommonHelper
    {
        public static bool IsValid(this string value)
        {
            return (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value));
        }
    }
}
