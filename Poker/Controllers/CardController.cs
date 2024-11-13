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
                TempData["ShuffledCards"] = _cardService.GetShuffledEncryptedCards();
                model.BackCards = _cardService.GetRandomBackCards(5).ToList();
                model.Cards = Enumerable.Repeat("BackCard.jpg", 2).ToList();
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

            TempData["PlayerCount"] = playerCount;

            var model = new GameModel
            {
                Cards = _cardService.GetRandomCards(playerCount * 2).ToList(),
                BackCards = _cardService.GetRandomBackCards(5).ToList(),
                Player = new UserModel
                {
                    Username = TempData.Peek("Username")?.ToString() ?? "Unknown",
                    Balance = TempData.Peek("Balance") != null
                        ? Convert.ToDouble(TempData.Peek("Balance"))
                        : 1000
                },
                PlayerCount = playerCount
            };

            return View("Game", model);
        }

        [HttpPost]
        public IActionResult RevealCards()
        {
            var encryptedCards = TempData["ShuffledCards"] as List<string>;
            var revealedCards = encryptedCards?.Take(2).Select(card => _cardService.DecryptCard(card, 0)).ToList();

            var model = new GameModel
            {
                Cards = revealedCards ?? new List<string>(),
                BackCards = _cardService.GetRandomBackCards(5).ToList(),
                PlayerCount = (int)(TempData["PlayerCount"] ?? 0)
            };

            return View("Game", model);
        }


        //[HttpPost]
        //public IActionResult StartGame(int playerCount)
        //{
        //    if (playerCount < 2 || playerCount > 10)
        //    {
        //        TempData["ErrorMessage"] = "Please enter a valid number of players (2-10).";
        //        return RedirectToAction("Game", new { isGameStarted = false });
        //    }

        //    var username = TempData.Peek("Username")?.ToString() ?? HttpContext.Session.GetString("Username") ?? "Unknown";
        //    var balance = TempData.Peek("Balance") != null
        //        ? Convert.ToDouble(TempData.Peek("Balance"))
        //        : HttpContext.Session.GetInt32("Balance") ?? 1000;

        //    var model = new GameModel
        //    {
        //        Cards = _cardService.GetRandomCards(playerCount * 2).ToList(),
        //        BackCards = _cardService.GetRandomBackCards(5).ToList(),
        //        Player = new UserModel
        //        {
        //            Username = username,
        //            Balance = balance
        //        },
        //        PlayerCount = playerCount
        //    };

        //    return View("Game", model);
        //}


        public IActionResult Fold()
        {
            TempData["Message"] = "Fold action performed!";
            return RedirectToAction("Game");
        }
    }
}
