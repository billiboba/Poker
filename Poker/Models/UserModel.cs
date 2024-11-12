namespace Poker.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public double Balance { get; set; }

        public UserModel()
        {
            Balance = 1000;
        }
    }
}
