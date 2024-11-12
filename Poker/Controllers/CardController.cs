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

            if (isGameStarted)
            {
                model.Cards = _cardService.GetRandomCards(2).ToList();
                model.BackCards = _cardService.GetRandomBackCards(5).ToList();
                model.Player = new UserModel { Id = 1, Username = "cos1nys", Balance = 1000 };
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

            var model = new GameModel
            {
                Cards = _cardService.GetRandomCards(playerCount * 2).ToList(), 
                BackCards = _cardService.GetRandomBackCards(5).ToList(),
                Player = new UserModel { Id = 1, Username = "cos1nys", Balance = 1000 },
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
