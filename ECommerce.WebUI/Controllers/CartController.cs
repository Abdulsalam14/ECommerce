using ECommerce.Business.Abstract;
using ECommerce.Entities.Concrete;
using ECommerce.WebUI.Models;
using ECommerce.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartSessionService _cartSessionService;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public CartController(ICartSessionService cartSessionService, IProductService productService, ICartService cartService)
        {
            _cartSessionService = cartSessionService;
            _productService = productService;
            _cartService = cartService;
        }
        public async Task<IActionResult> AddToCart(int productId, int page, int category, string filteraz, string filterhl)
        {
            var productToBeAdded = await _productService.GetById(productId);
            var cart = _cartSessionService.GetCart();
            _cartService.AddToCart(cart, productToBeAdded);
            productToBeAdded.HasAdded = true;
            _productService?.Update(productToBeAdded);
            _cartSessionService.SetCart(cart);

            TempData.Add("message", String.Format("Your product, {0} was added successfully to cart", productToBeAdded.ProductName));

            return RedirectToAction("Index", "Product", new { page = page, category = category, filter_az = filteraz, filter_hl = filterhl });
        }

        public async Task<IActionResult> Remove(int productId, int page, int category,string filteraz,string filterhl)
        {
            var RemoveProduct = await _productService.GetById(productId);
            var cart = _cartSessionService.GetCart();
            _cartService.RemoveFromCart(cart, RemoveProduct.ProductId);
            RemoveProduct.HasAdded = false;
            _productService?.Update(RemoveProduct);
            _cartSessionService.SetCart(cart);

            TempData.Add("message", String.Format("Your product, {0} was removed successfully from cart", RemoveProduct.ProductName));

            return RedirectToAction("Index", "Product", new { page = page, category = category, filter_az=filteraz,filter_hl=filterhl });
        }


        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var cart = _cartSessionService.GetCart();
            var RemoveProduct = await _productService.GetById(productId);
            _cartService.RemoveFromCart(cart, productId);
            _cartSessionService.SetCart(cart);
            RemoveProduct.HasAdded = false;
            _productService?.Update(RemoveProduct);
            TempData.Add("message", "Your Product was removed successfully from cart");
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            var cart = _cartSessionService.GetCart();
            var model = new CartListViwModel
            {
                Cart = cart
            };
            return View(model);
        }


        public IActionResult Increase(int productId)
        {
            var cart = _cartSessionService.GetCart();
            var cartLine = cart.CartLines.FirstOrDefault(c => c.Product.ProductId == productId);
            if (cartLine.Quantity < cartLine.Product.UnitsInStock)
            {
                cartLine.Quantity++;
                _cartSessionService.SetCart(cart);

                TempData.Add("message", "One item added");
            }


            return RedirectToAction("List");
        }
        public IActionResult Decrease(int productId)
        {
            var cart = _cartSessionService.GetCart();
            var cartLine = cart.CartLines.FirstOrDefault(c => c.Product.ProductId == productId);
            if (cartLine.Quantity > 1)
            {
                cartLine.Quantity--;
                _cartSessionService.SetCart(cart);

                TempData.Add("message", "One item removed");
            }

            return RedirectToAction("List");
        }

        public IActionResult Complete()
        {
            var shippingDetailViewModel = new ShippingDetailViewModel
            {
                ShippingDetails = new ShippingDetails()
            };

            return View(shippingDetailViewModel);
        }

        [HttpPost]
        public IActionResult Complete(ShippingDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            TempData.Add("message", String.Format("Thank you {0} , you order is in progress", model.ShippingDetails.Firstname));
            return View();
        }
    }
}
