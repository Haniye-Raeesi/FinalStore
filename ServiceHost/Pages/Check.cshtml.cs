using _0_Framework.Application.ZarinPal;
using _01_StoreQuery.Contracts.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;
using System.Globalization;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckModel : PageModel
    {
        public List<CartItem>? CartItems;
        public const string CookieName = "cart-items";
        private readonly IProductQuery _ProductQuery;
        private readonly ICartService _cartService;
        private readonly IOrderApplication _orderApplication;
        private readonly IZarinPalFactory _zarinPalFactory;
        public Cart Cart { get; set; }
        public double TotalDiscountAmount=0;
        public double TotalPayment = 0;
        public double TotalDiscountPayment = 0;

        public CheckModel(IProductQuery productQuery,ICartService cartService,
            IOrderApplication orderApplication,IZarinPalFactory zarinPalFactory)
        {
            _ProductQuery = productQuery;
            _cartService = cartService;
            _orderApplication = orderApplication;
            _zarinPalFactory= zarinPalFactory;
        }
        public void OnGet()
        {
            var Serializar = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = Serializar.Deserialize<List<CartItem>>(value);
            
            foreach (var item in cartItems)
            {
                item.TotalItemPrice = item.unitPrice * item.count;
                item.DiscountedTotalItemPrice = Math.Round((item.unitPrice *(int) item.Discount*item.count) / 100);
                item.TotPriceAfterDisc = item.count*item.unitPrice- item.DiscountedTotalItemPrice;
                TotalDiscountPayment += item.TotPriceAfterDisc;
                TotalPayment += item.TotalItemPrice;
                TotalDiscountAmount += (item.TotalItemPrice - item.TotPriceAfterDisc);
            }
            CartItems = _ProductQuery.CheckInventoryStatus(cartItems);
            Cart = new Cart();
            Cart.Items = CartItems;
            Cart.TotalAmount= TotalPayment;
            Cart.DiscountAmount= TotalDiscountAmount;
            Cart.PayAmount = TotalDiscountPayment;
            _cartService.Set(Cart);


        }
        public IActionResult OnPostPay(int paymentMethod)
        {
            var cart = _cartService.Get();
            cart.SetPaymentMethod(paymentMethod);

            var result = _ProductQuery.CheckInventoryStatus(cart.Items);
            if (result.Any(x => !x.IsInStock))
                return RedirectToPage("/Cart");

            var orderId = _orderApplication.PlaceOrder(cart);
            if (paymentMethod == 1)
            {
                var paymentResponse = _zarinPalFactory.CreatePaymentRequest(
                    cart.PayAmount.ToString(CultureInfo.InvariantCulture), "", "",
                    "خرید از درگاه لوازم خانگی و دکوری", orderId);

                return Redirect(
                    $"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
            }

            var paymentResult = new PaymentResult();
            return RedirectToPage("/PaymentResult",
                paymentResult.Succeeded(
                    "سفارش شما با موفقیت ثبت شد. پس از تماس کارشناسان ما و پرداخت وجه، سفارش ارسال خواهد شد.", null));
        }
        public IActionResult OnGetCallBack([FromQuery] string authority, [FromQuery] string status,
            [FromQuery] long oId)
        {
            var orderAmount = _orderApplication.GetAmountBy(oId);
            var verificationResponse =
                _zarinPalFactory.CreateVerificationRequest(authority,
                    orderAmount.ToString(CultureInfo.InvariantCulture));

            var result = new PaymentResult();
            if (status == "OK" && verificationResponse.Status >= 100)
            {
                var issueTrackingNo = _orderApplication.PaymentSucceeded(oId, verificationResponse.RefID);
                Response.Cookies.Delete("cart-items");
                result = result.Succeeded("پرداخت با موفقیت انجام شد.", issueTrackingNo);
                return RedirectToPage("/PaymentResult", result);
            }

            result = result.Failed(
                "پرداخت با موفقیت انجام نشد. درصورت کسر وجه از حساب، مبلغ تا 24 ساعت دیگر به حساب شما بازگردانده خواهد شد.");
            return RedirectToPage("/PaymentResult", result);
        }
    }
}
