using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poker.Data;

namespace Poker.Pages.Main
{
    public class MenuModel : PageModel
    {
        private readonly CardService _cardService;

        public List<string> Cards { get; private set; }
        public List<string> BackCards { get; private set; }

        public MenuModel(CardService cardService)
        {
            _cardService = cardService;
            Cards = new List<string>();
            BackCards = new List<string>();
        }

        public void OnGet()
        {
            Cards = new List<string>();
        }

        public void OnPost()
        {
            Cards = _cardService.GetRandomCards(2).ToList();
            BackCards = _cardService.GetRandomBackCards(1).ToList();
        }
        public string GetCardPositionClass(string card)
        {
            var index = Cards.IndexOf(card);
            return $"card-position-{index}";
        }
        public string GetBackCardPositionClass(string card)
        {
            var index = BackCards.IndexOf(card);
            return $"backCard-position-{index}";
        }
    }
}
