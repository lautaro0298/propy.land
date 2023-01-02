using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using LibreriaClases.DTO;

namespace LibreriaExperto.Seguridad
{
    public static class Criptografia
    {
        public static void Encrypt(DTOCryptography dTOCryptography)
        {
            using (Aes aes = Aes.Create())
            {
                // Encrypt the string to an array of bytes.
                dTOCryptography.OriginalTextEncrypted = Encrypting(dTOCryptography.OriginalText, aes.Key, aes.IV);
                dTOCryptography.Key = aes.Key;
                dTOCryptography.Vector = aes.IV;
            }
        }

        public static void Decrypt(DTOCryptography dTOCryptography)
        {
                // Decrypt the bytes to a string.
                dTOCryptography.OriginalText = Decrypting(dTOCryptography.OriginalTextEncrypted,dTOCryptography.Key,dTOCryptography.Vector);
        }

        private static byte[] Encrypting(string Data, byte[] Key, byte[] Vector)
        {

            if (Data == null || Data.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (Vector == null || Vector.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = Vector;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(Data);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        private static string Decrypting(byte[] cipherData, byte[] Key, byte[] Vector)
        {
            // Check arguments.
            if (cipherData == null || cipherData.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (Vector == null || Vector.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string decrypted = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = Vector;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherData))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            decrypted = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return decrypted;
        }

    }
}
