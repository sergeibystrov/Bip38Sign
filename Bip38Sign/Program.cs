using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBitcoin;

namespace Bip38Sign
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length!=3)
            {
                Console.WriteLine("Usage: Bip38Sign bip38privatekey passphrase messagetosign");
                return;
            }

            var encryptedPrivateKey = args[0];
            var password = args[1];
            var messageToSign = args[2];

            var encryptedSecret = BitcoinEncryptedSecret.Create(encryptedPrivateKey, Network.Main);
            var secret = encryptedSecret.GetSecret(password);

            // uncomment the following line to show the private key: 
            // Console.WriteLine($"Private key (wif):\r\n{secret.ToWif()}");
            Console.WriteLine($"Public key:\r\n{secret.PubKey.GetAddress(Network.Main)}");

            string signature = secret.PrivateKey.SignMessage(messageToSign);

            Console.WriteLine($"The signature:\r\n{signature}");
            Console.WriteLine($"Signature validity: {secret.PubKey.VerifyMessage(messageToSign, signature)}");
        }
    }
}
