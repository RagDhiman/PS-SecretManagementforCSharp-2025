﻿using System.Security.Cryptography;
using System.Text;

namespace Encryption_Tests.IEncryption
{
    public class WindowsDPAPIEncryption : IEncryption
    {
        public string? Encrypt(string? stringToEncrypt)
        {
            if (string.IsNullOrEmpty(stringToEncrypt)) return null;

            byte[] toEncrypt = Encoding.UTF8.GetBytes(stringToEncrypt);

            #pragma warning disable CA1416 // Validate platform compatibility
            byte[] encrypted = ProtectedData.Protect(toEncrypt, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encrypted);
        }

        public string? Decrypt(string? stringToDecrypt)
        {
            if (string.IsNullOrEmpty(stringToDecrypt)) return null;

            byte[] decrypted;
            try
            {
                byte[] data = Convert.FromBase64String(stringToDecrypt);
                decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return Encoding.UTF8.GetString(decrypted);
        }
    }
}
