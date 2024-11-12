using Microsoft.AspNetCore.Mvc;
using Poker.Data;
using Poker.Models;

namespace Poker.Controllers
{
    public class PokerController : Controller
    {
        private readonly CardService _cardService;

        public PokerController(CardService cardService)
        {
            _cardService = cardService;
        }
        public IActionResult Fold()
        {
            TempData["Message"] = "Fold action performed!";
            return RedirectToAction("Game");
        }
        public IActionResult Game()
        {
            var model = new GameModel
            {
                Cards = _cardService.GetRandomCards(2).ToList(),
                BackCards = _cardService.GetRandomBackCards(5).ToList(),
                Player = new UserModel { Id = 1, Username = "cos1nys", Balance = 1000 }
            };

            return View(model);
        }
    }
}
