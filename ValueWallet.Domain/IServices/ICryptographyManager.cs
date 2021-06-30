namespace ValueWallet.Domain.IServices
{
    public interface ICryptographyManager
    {
        string EncryptData(string data);

        string DecryptData(string text);
    }
}
