using AspnetRunBasics.Models;
using AspnetRunBasics.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetRunBasics
{
    public class CartModel : PageModel
    {
        private readonly IBasketService _basketService;

        public CartModel(IBasketService basketService)
        {
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        }

        public BasketModel Cart { get; set; } = new BasketModel();        

        public async Task<IActionResult> OnGetAsync()
        {
            var userName = "Anderson G. Masiero";
            Cart = await _basketService.GetBasket(userName);

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
        {
            var userName = "Anderson G. Masiero";
            var basket = await _basketService.GetBasket(userName);

            var item = basket.Items.Single(p => p.ProductId == productId);
            basket.Items.Remove(item);

            var basketUpdated = await _basketService.UpdateBasket(basket);
            return RedirectToPage();
        }
    }
}