using System;
using System.Security.Cryptography;
using System.IO;
using ValueWallet.Domain.IServices;
using System.Text;

//Thank Ref: https://www.codeproject.com/Articles/769741/Csharp-AES-bits-Encryption-Library-with-Salt

namespace ValueWallet.Domain.Services
{
    public class CryptographyManager : ICryptographyManager
    {
        private string secretKey = "";

        private readonly byte[] saltBytes = new byte[] { 6, 8, 5, 7, 5, 2, 3, 0 };

        private string UniqDevice { get; set; } = "ValueWalletProject"; //default UniqDevice

        private const string _formatDate = "ddyyyyMMM";

        private readonly int days = DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month);

        private string Key1
        {
            get
            {
                if (days % 2 == 0) //even number เลขคู่
                    return $"ZP$Dr*7D{days}&my%vvS+Even";
                else
                    return $"AM%6^+yF{days}vB^KjyjsOdd"; //Odd
            }
        }

        private string Key2
        {
            get
            {
                if (days % 2 == 0) //even number เลขคู่
                    return $"gBU*f-AG{days}Ta@e=E3$";
                else
                    return $"b9?kBm@UH{days}M&DP28#"; //Odd
            }
        }

        public CryptographyManager(string uniqDevice)
        {
            if (uniqDevice.IsValid())
                UniqDevice = uniqDevice;
        }

        public string DecryptData(string painText)
        {
            return DecryptString(painText);
        }

        public string EncryptData(string painText)
        {
            return EncryptString(painText);
        }

        private void InitialKey()
        {
            if (UniqDevice.Length < 10)
                UniqDevice += Key2;

            string _date = DateTime.UtcNow.ToString(_formatDate);
            string a = UniqDevice.Substring(2, 5);
            string b = Key1;
            string c = UniqDevice.Substring(0, 4);
            string d = UniqDevice.Substring(6, 4);
            string e = Key2;
            string f = UniqDevice.Substring(3, 6);

            secretKey = $"{a}{b}{c}{_date}{d}{e}{f}";
        }

        private string EncryptString(string text)
        {
            InitialKey();

            byte[] baPwd = Encoding.UTF8.GetBytes(secretKey);

            // Hash the password with SHA256
            byte[] baPwdHash = SHA256Managed.Create().ComputeHash(baPwd);

            byte[] baText = Encoding.UTF8.GetBytes(text);

            byte[] baSalt = GetRandomBytes();
            byte[] baEncrypted = new byte[baSalt.Length + baText.Length];

            // Combine Salt + Text
            for (int i = 0; i < baSalt.Length; i++)
                baEncrypted[i] = baSalt[i];
            for (int i = 0; i < baText.Length; i++)
                baEncrypted[i + baSalt.Length] = baText[i];

            baEncrypted = AES_Encrypt(baEncrypted, baPwdHash);

            string result = Convert.ToBase64String(baEncrypted);
            return result;
        }

        private string DecryptString(string text)
        {
            InitialKey();

            byte[] baPwd = Encoding.UTF8.GetBytes(secretKey);

            // Hash the password with SHA256
            byte[] baPwdHash = SHA256Managed.Create().ComputeHash(baPwd);

            byte[] baText = Convert.FromBase64String(text);

            byte[] baDecrypted = AES_Decrypt(baText, baPwdHash);

            // Remove salt
            int saltLength = GetSaltLength();
            byte[] baResult = new byte[baDecrypted.Length - saltLength];
            for (int i = 0; i < baResult.Length; i++)
                baResult[i] = baDecrypted[i + saltLength];

            string result = Encoding.UTF8.GetString(baResult);
            return result;
        }

        private byte[] GetRandomBytes()
        {
            int saltLength = GetSaltLength();
            byte[] ba = new byte[saltLength];
            RNGCryptoServiceProvider.Create().GetBytes(ba);
            return ba;
        }

        private int GetSaltLength()
        {
            return 8;
        }

        private byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.ECB;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        private byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.ECB;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }
    }
}
