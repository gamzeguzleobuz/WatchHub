using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class BasketController : Controller
    {
        private IBasketViewModelService _basketViewModelService;

        public BasketController(IBasketViewModelService basketViewModelService) 
        {
        _basketViewModelService = basketViewModelService;
        }


        public Task<ActionResult<BasketViewModel>> AddItem(int productId, int quantity = 1)
        { 
        
        }
    }
}
