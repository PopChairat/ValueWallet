using System;
namespace ValueWallet.Droid.DroidModels
{
    public class EncryptedData
    {
        public EncryptedData(byte[] ciphertext, byte[] vi)
        {
            Ciphertext = ciphertext;
            Vi = vi;
        }

        public byte[] Ciphertext { get; }
        public byte[] Vi { get; }
    }
}
