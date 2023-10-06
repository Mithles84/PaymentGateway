using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Models;
using Razorpay.Api;

namespace PaymentGateway.Controllers
{
    public class MyOrderController : Controller
    {
        [BindProperty]
        public EntityOrder _orderDetails { get; set; }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult CreateOrder()
        {


            string key = "<rzp_test_GuoTeNjQD5281J>";
            string secret = "<HE1lapQfySHaIRiI5d9QLDfs>";
            Random _random=new Random();
            string transactionID=_random.Next(0,3000).ToString();


            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount",  Convert.ToDecimal(_orderDetails.Amount)*100); // this amount should be same as transaction amount
            input.Add("currency", "INR");
            input.Add("receipt", transactionID);

           

            RazorpayClient client = new RazorpayClient(key, secret);


            Razorpay.Api.Order order = client.Order.Create(input);
            ViewBag.orderId = order["id"].ToString();

            return View("Payment", _orderDetails);

        }
        public ActionResult Payment(string razorpay_payment_id,string razorpay_order_id,string
            razorpay_signature)
        {
            //RazorpayClient client = new RazorpayClient(your_key_id, your_secret);

            Dictionary<string, string> attributes = new Dictionary<string, string>();

            attributes.Add("razorpay_payment_id", razorpay_payment_id);
            attributes.Add("razorpay_order_id", razorpay_order_id);
            attributes.Add("razorpay_signature", razorpay_signature);

            Utils.verifyPaymentSignature(attributes);
            EntityOrder orderDtl=new EntityOrder();
            orderDtl.TransactionID = razorpay_payment_id;
            orderDtl.OrderID= razorpay_order_id;

            return View("PaymentSuccess", orderDtl);
        }
    }
}
