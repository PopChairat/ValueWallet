namespace ValueWallet.Domain.IServices
{
    public interface ICryptographyManager
    {
        string EncryptData(string data);

        T DecryptData<T>();

        string DecryptData(string text);
    }
}
