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

        public IActionResult Game(bool isGameStarted = false)
        {
            var model = new GameModel();

            var username = TempData.Peek("Username")?.ToString() ?? "Unknown";
            var balanceString = TempData.Peek("Balance")?.ToString();
            var balance = double.TryParse(balanceString, out var result) ? result : 1000;
            var userId = (int?)TempData.Peek("UserId") ?? 0;

            if (isGameStarted)
            {
                model.Cards = _cardService.GetRandomCards(2).ToList();
                model.BackCards = _cardService.GetRandomBackCards(5).ToList();
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

            var username = TempData.Peek("Username")?.ToString() ?? "Unknown";
            var balanceString = TempData.Peek("Balance")?.ToString();
            var balance = double.TryParse(balanceString, out var result) ? result : 1000;
            var userId = (int?)TempData.Peek("UserId") ?? 0; 

            var model = new GameModel
            {
                Cards = _cardService.GetRandomCards(playerCount * 2).ToList(),
                BackCards = _cardService.GetRandomBackCards(5).ToList(),
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



        public IActionResult Fold()
        {
            TempData["Message"] = "Fold action performed!";
            return RedirectToAction("Game");
        }
    }
}
