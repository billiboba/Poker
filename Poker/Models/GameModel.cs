namespace Poker.Models
{
    public class GameModel
    {
        public List<string> Cards { get; set; } 
        public List<string> BackCards { get; set; } 
        public UserModel Player { get; set; } 
        public int PlayerCount { get; set; } 

        public GameModel()
        {
            Cards = new List<string>();
            BackCards = new List<string>();
            Player = new UserModel();
        }

        public string GetCardPositionClass(string card, int position)
        {
            return $"card-position-{position}";
        }

        public string GetBackCardPositionClass(string card, int position)
        {
            return $"backCard-position-{position}";
        }
    }
}
