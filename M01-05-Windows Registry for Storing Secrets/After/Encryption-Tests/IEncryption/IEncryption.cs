namespace Encryption_Tests.IEncryption
{
    public interface IEncryption
    {
        string? Encrypt(string? stringToEncrypt);

        string? Decrypt(string? stringToDecrypt);
    }
}
