using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poker.Data;
using Poker.Models;

namespace Poker.Controllers
{
    public class CardController : Controller
    {
        private readonly CardService _cardService;

        public CardController(CardService cardService)
        {
            _cardService = cardService;
        }

        public IActionResult Index()
        {
            var model = new GameModel
            {
                Cards = _cardService.GetRandomCards(2).ToList(),
                BackCards = _cardService.GetRandomBackCards(5).ToList()
            };

            return View(model);
        }

        public IActionResult GameBoard()
        {
            return View();
        }
    }
}
