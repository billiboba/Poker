using Microsoft.AspNetCore.Mvc;
using Poker.Data;
using Poker.Models;
using System.Linq;

namespace Poker.Controllers
{
    public class PokerController : Controller
    {
        private readonly CardService _cardService;

        public PokerController(CardService cardService)
        {
            _cardService = cardService;
        }

        private (string username, double balance, int userId) GetUserData()
        {
            var username = TempData.Peek("Username")?.ToString() ?? "Unknown";
            var balanceString = TempData.Peek("Balance")?.ToString();
            var balance = double.TryParse(balanceString, out var result) ? result : 1000;
            var userId = (int?)TempData.Peek("UserId") ?? 0;
            return (username, balance, userId);
        }

        private (List<string> cards, List<string> backCards) GetCards(int playerCount)
        {
            var cards = _cardService.GetRandomCards(playerCount * 2).ToList();
            var backCards = _cardService.GetRandomBackCards(5).ToList();
            return (cards, backCards);
        }

        public IActionResult Game(bool isGameStarted = false)
        {
            var model = new GameModel();
            var (username, balance, userId) = GetUserData();

            if (isGameStarted)
            {
                var (cards, backCards) = GetCards(2);
                model.Cards = cards;
                model.BackCards = backCards;
                model.Player = new UserModel { Username = username, Balance = balance, Id = userId };
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult StartGame(int playerCount)
        {
            if (playerCount < 2 || playerCount > 10)
            {
                TempData["ErrorMessage"] = "Please enter a valid number of players (2-10).";
                return RedirectToAction("Game", new { isGameStarted = false });
            }

            var (username, balance, userId) = GetUserData(); 
            var (cards, backCards) = GetCards(playerCount);

            var model = new GameModel
            {
                Cards = cards,
                BackCards = backCards,
                Player = new UserModel
                {
                    Username = username,
                    Balance = balance,
                    Id = userId
                },
                PlayerCount = playerCount
            };

            return View("Game", model);
        }
    }
}