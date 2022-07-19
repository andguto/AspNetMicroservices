using AspnetRunBasics.Models;
using AspnetRunBasics.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace AspnetRunBasics
{
    public class CheckOutModel : PageModel
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public CheckOutModel(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        }

        [BindProperty]
        public BasketCheckoutModel Order { get; set; }

        public BasketModel Cart { get; set; } = new BasketModel();

        public async Task<IActionResult> OnGetAsync()
        {
            var userName = "Anderson G. Masiero";
            Cart = await _basketService.GetBasket(userName);
            return Page();
        }

        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            var userName = "Anderson G. Masiero";
            Cart = await _basketService.GetBasket(userName);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Order.UserName = userName;
            Order.TotalPrice = Cart.TotalPrice;

            await _basketService.ChekoutBasket(Order);
            
            return RedirectToPage("Confirmation", "OrderSubmitted");
        }       
    }
}