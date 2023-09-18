using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents
{
    public class NavbarBasketViewComponent : ViewComponent
    {
       

        public NavbarBasketViewComponent()
        {

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //login olunmuşsa kullanici id, olunmamışsa cookiede saklanan anonim id eğer o da yoksa şimdi üretilecek ve sonra cookielerde saklanacak benzersiz bir anonim id parametre olarak sokulmalı
            
            return View();
        }
    }
}
