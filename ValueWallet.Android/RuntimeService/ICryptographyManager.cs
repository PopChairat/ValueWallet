using System;
using Android.Security.Keystore;
using Java.Security;
using Javax.Crypto;
using ValueWallet.Droid.DroidModels;
using Javax.Crypto.Spec;
using System.Text;

namespace ValueWallet.Droid.RuntimeService
{
    public interface ICryptographyManager
    {
        /**
         * * This method first gets or generates an instance of SecretKey and then initializes the Cipher
         * with the key. The secret key uses [ENCRYPT_MODE][Cipher.ENCRYPT_MODE] is used.
         */
        Cipher GetInitializedCipherForEncryption(string keyName);

        /**
         * This method first gets or generates an instance of SecretKey and then initializes the Cipher
         * with the key. The secret key uses [DECRYPT_MODE][Cipher.DECRYPT_MODE] is used.
         */
        Cipher GetInitializedCipherForDecryption(string keyName, byte[] initializationVector);

        /**
         * The Cipher created with [getInitializedCipherForEncryption] is used here
         */
        EncryptedData EncryptData(string plaintext, Cipher cipher);

        /**
         * The Cipher created with [getInitializedCipherForDecryption] is used here
         */
        string DecryptData(byte[] ciphertext, Cipher cipher);
    }

    internal class CryptographyManager : ICryptographyManager
    {
        private readonly int KEY_SIZE = 256;
        private readonly string ANDROID_KEYSTORE = "AndroidKeyStore";
        private readonly string ENCRYPTION_BLOCK_MODE = KeyProperties.BlockModeGcm;
        private readonly string ENCRYPTION_PADDING = KeyProperties.EncryptionPaddingNone;
        //private string ENCRYPTION_ALGORITHM = KeyProperties.KeyAlgorithmAes;

        public string DecryptData(byte[] ciphertext, Cipher cipher)
        {
            byte[] plaintext = cipher.DoFinal(ciphertext);
            string result = Encoding.UTF8.GetString(plaintext);
            return result;
        }

        public EncryptedData EncryptData(string plaintext, Cipher cipher)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plaintext);
            byte[] ciphertext = cipher.DoFinal(bytes);
            EncryptedData result = new EncryptedData(ciphertext, cipher.GetIV());
            return result;
        }

        public Cipher GetInitializedCipherForDecryption(string keyName, byte[] initializationVector)
        {
            Cipher cipher = GetCipher();
            IKey secretKey = GetOrCreateSecretKey(keyName);
            GCMParameterSpec gCM = new GCMParameterSpec(128, initializationVector);
            cipher.Init(CipherMode.EncryptMode, secretKey, gCM);
            return cipher;
        }

        public Cipher GetInitializedCipherForEncryption(string keyName)
        {
            Cipher cipher = GetCipher();
            IKey secretKey = GetOrCreateSecretKey(keyName);
            cipher.Init(CipherMode.EncryptMode, secretKey);
            return cipher;
        }

        private Cipher GetCipher()
        {
            string transformation = "$ENCRYPTION_ALGORITHM/$ENCRYPTION_BLOCK_MODE/$ENCRYPTION_PADDING";
            return Cipher.GetInstance(transformation);
        }


        private IKey GetOrCreateSecretKey(string keyName)
        {
            // If Secretkey was previously created for that keyName, then grab and return it.
            KeyStore keyStore = KeyStore.GetInstance(ANDROID_KEYSTORE);
            keyStore.Load(null); // Keystore must be loaded before it can be accessed
            IKey result = keyStore.GetKey(keyName, null);

            if (result != null)
                return result;

            KeyGenerator keyGen = KeyGenerator.GetInstance(KeyProperties.KeyAlgorithmAes, ANDROID_KEYSTORE);
            KeyGenParameterSpec keyGenSpec =
                new KeyGenParameterSpec.Builder(keyName, KeyStorePurpose.Encrypt | KeyStorePurpose.Decrypt)
                    .SetBlockModes(ENCRYPTION_BLOCK_MODE)
                    .SetEncryptionPaddings(ENCRYPTION_PADDING)
                    .SetUserAuthenticationRequired(true)
                    .SetKeySize(KEY_SIZE)
                    .Build();
            keyGen.Init(keyGenSpec);
            ISecretKey secret = keyGen.GenerateKey();
            return secret;
        }
    }
}
