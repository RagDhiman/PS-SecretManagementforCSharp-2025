using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Encryption_Tests.IEncryption
{
    public class ANCDataProtectionEncryption : IEncryption
    {
        private IDataProtector _protector;

        public ANCDataProtectionEncryption()
        {
            // add data protection services
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDataProtection();
            var services = serviceCollection.BuildServiceProvider();
            var dataProtectionProvider = services.GetService<IDataProtectionProvider>();
            if (dataProtectionProvider != null)
            {
                _protector = dataProtectionProvider.CreateProtector("MyProtector");
            }
            else
            {
                throw new InvalidOperationException("DataProtectionProvider is not available.");
            }
        }

        public string? Decrypt(string? stringToDecrypt)
        {
            if (string.IsNullOrEmpty(stringToDecrypt)) return null;

            string decryptedString;
            try
            {
                decryptedString = _protector.Unprotect(stringToDecrypt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return decryptedString;
        }

        public string? Encrypt(string? stringToEncrypt)
        {
            if (string.IsNullOrEmpty(stringToEncrypt)) return null;
            
            return _protector.Protect(stringToEncrypt);
        }
    }
}
