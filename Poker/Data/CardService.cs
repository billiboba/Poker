namespace Poker.Data
{
    public class CardService
    {
        private static readonly string[] CardNames =
        {
            "2Б.jpg", "2К.jpg", "2П.jpg", "2Ч.jpg",
            "3Б.jpg", "3К.jpg", "3П.jpg", "3Ч.jpg",
            "ТузБ.jpg", "ТузК.jpg", "ТузП.jpg", "ТузЧ.jpg",
            "4Б.jpg", "4К.jpg", "4П.jpg", "4Ч.jpg",
            "5Б.jpg", "5К.jpg", "5П.jpg", "5Ч.jpg",
            "6Б.jpg", "6К.jpg", "6П.jpg", "6Ч.jpg",
            "7Б.jpg", "7К.jpg", "7П.jpg", "7Ч.jpg",
            "8Б.jpg", "8К.jpg", "8П.jpg", "8Ч.jpg",
            "9Б.jpg", "9К.jpg", "9П.jpg", "9Ч.jpg",
            "10Б.jpg", "10К.jpg", "10П.jpg", "10Ч.jpg",
            "ВБ.jpg", "ВК.jpg", "ВП.jpg", "ВЧ.jpg",
            "ДБ.jpg", "ДК.jpg", "ДП.jpg", "ДЧ.jpg",
            "КБ.jpg", "КК.jpg", "КП.jpg", "КЧ.jpg"
        };

        private static readonly string[] BackCardsNames = { "BackCard.jpg" };

        public IEnumerable<string> GetRandomCards(int count)
        {
            var random = new Random();
            return CardNames.OrderBy(_ => random.Next()).Take(count);
        }

        public IEnumerable<string> GetRandomBackCards(int count)
        {
            return Enumerable.Repeat("BackCard.jpg", count);
        }

        public IEnumerable<IEnumerable<string>> GetCardsForPlayers(int playerCount)
        {
            var random = new Random();
            var allCards = CardNames.OrderBy(_ => random.Next()).ToList();

            var playerCards = new List<IEnumerable<string>>();
            for (int i = 0; i < playerCount; i++)
            {
                playerCards.Add(allCards.Skip(i * 2).Take(2)); 
            }

            return playerCards;
        }
    }
}
