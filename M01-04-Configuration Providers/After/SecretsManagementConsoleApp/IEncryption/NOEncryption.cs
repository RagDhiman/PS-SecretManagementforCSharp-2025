using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretsManagementConsoleApp.IEncryption
{
    public class NOEncryption : IEncryption
    {
        public string? Decrypt(string? stringToDecrypt)
        {
            return stringToDecrypt;
        }

        public string? Encrypt(string? stringToEncrypt)
        {
            return stringToEncrypt;
        }
    }
}
