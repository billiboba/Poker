namespace Poker.Data
{
    public class CardService
    {
        private static readonly string[] CardNames =
{
    "2Б.jpg", "2К.jpg", "2П.jpg", "2Ч.jpg",
    "3Б.jpg", "3К.jpg", "3П.jpg", "3Ч.jpg",
    "ТузБ.jpg", "ТузК.jpg", "ТузП.jpg", "ТузЧ.jpg"
};

        private static readonly string[] BackCardsNames =
        {
    "2Б.jpg", "2К.jpg", "2П.jpg", "2Ч.jpg",
    "3Б.jpg", "3К.jpg", "3П.jpg", "3Ч.jpg",
    "ТузБ.jpg", "ТузК.jpg", "ТузП.jpg", "ТузЧ.jpg",
    "BackCard.jpg" 
};

        public IEnumerable<string> GetRandomCards(int count)
        {
            var random = new Random();
            return CardNames.OrderBy(_ => random.Next()).Take(count);
        }
        public IEnumerable<string> GetRandomBackCards(int count)
        {
            var random = new Random();
            return CardNames.OrderBy(_ => random.Next()).Take(count);
        }
    }
}
