using SecretsManagementConsoleApp.IEncryption;

IEncryption myEncryption = new ANCDataProtectionEncryption();

var secret = "mySecret";
Console.WriteLine(secret);
var encryptedSecret = myEncryption.Encrypt(secret);
Console.WriteLine(encryptedSecret);
Console.WriteLine(myEncryption.Decrypt(encryptedSecret));