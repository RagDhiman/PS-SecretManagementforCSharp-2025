using Encryption_Tests.IEncryption;

IEncryption encryption;
while (true)
{
    Console.Clear();
    Console.WriteLine("=== Select Encryption Method ===");
    Console.WriteLine("1. Windows DPAPI");
    Console.WriteLine("2. ASP.NET Core Data Protection");
    Console.WriteLine("3. Exit");
    Console.Write("Enter your choice: ");
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            encryption = new WindowsDPAPIEncryption();
            EncryptionMenu(encryption);
            break;
        case "2":
            encryption = new ANCDataProtectionEncryption();
            EncryptionMenu(encryption);
            break;
        case "3":
            return;
        default:
            Console.WriteLine("Invalid option. Please choose between 1 and 3.");
            break;
    }
}


void EncryptionMenu(IEncryption encryption)
{
    while (true)
    {
        Console.Clear();
        if (encryption is WindowsDPAPIEncryption)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Windows DPAPI ===");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("=== ASP.NET Core Data Protection ===");
        }

        Console.WriteLine("1. Encrypt");
        Console.WriteLine("2. Decrypt");
        Console.WriteLine("3. Exit");
        Console.ResetColor();
        Console.Write("Enter your choice: ");

        string? choice = Console.ReadLine();
        string? inputText;

        switch (choice)
        {
            case "1":
                Console.WriteLine("Please enter text to encrypt:");
                inputText = Console.ReadLine();
                if (inputText != null && inputText.Length > 0)
                {
                    var secret = encryption.Encrypt(inputText);
                    PrintGreen(secret);
                }
                break;
            case "2":
                Console.WriteLine("Please enter text to decrypt:");
                inputText = Console.ReadLine();
                if (inputText != null && inputText.Length > 0)
                {
                    PrintRed(encryption.Decrypt(inputText));
                }
                break;
            case "3":
                Console.WriteLine("Exiting...");
                return;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }

        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    }
}

void PrintRed(string text)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(text);
    Console.ResetColor();
}

void PrintGreen(string text)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(text);
    Console.ResetColor();
}