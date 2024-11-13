using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MentalPoker
{
    public class MentalPoker
    {
        private static readonly List<string> Deck = new()
        {
            "2H", "3H", "4H", "5H", "6H", "7H", "8H", "9H", "10H", "JH", "QH", "KH", "AH",
            "2D", "3D", "4D", "5D", "6D", "7D", "8D", "9D", "10D", "JD", "QD", "KD", "AD",
            "2C", "3C", "4C", "5C", "6C", "7C", "8C", "9C", "10C", "JC", "QC", "KC", "AC",
            "2S", "3S", "4S", "5S", "6S", "7S", "8S", "9S", "10S", "JS", "QS", "KS", "AS"
        };

        private readonly List<byte[]> EncryptedDeck;
        private readonly RSAParameters[] PlayerKeys;

        public MentalPoker(int numberOfPlayers)
        {
            if (numberOfPlayers < 2) throw new ArgumentException("Must have at least two players.");

            PlayerKeys = new RSAParameters[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                using var rsa = RSA.Create();
                PlayerKeys[i] = rsa.ExportParameters(true);
            }

            EncryptedDeck = Deck.Select(card => EncryptCard(card)).ToList();
        }

        private byte[] EncryptCard(string card)
        {
            using var rsa = RSA.Create();
            rsa.ImportParameters(PlayerKeys[0]);
            return rsa.Encrypt(Encoding.UTF8.GetBytes(card), RSAEncryptionPadding.OaepSHA256);
        }

        public void DistributeCards(int playersCount)
        {
            var rnd = new Random();
            var shuffledDeck = EncryptedDeck.OrderBy(x => rnd.Next()).ToList();

            for (int i = 0; i < playersCount; i++)
            {
                Console.WriteLine($"Player [{i + 1}]:");
                Console.WriteLine(Convert.ToBase64String(shuffledDeck[i])); // Преобразование в Base64
            }
        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            var poker = new MentalPoker(4);
            poker.DistributeCards(4);
        }
    }
}
