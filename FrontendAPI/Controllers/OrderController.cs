using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FrontendAPI.Models;
using System.Net.Http;

namespace FrontendAPI.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;

        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BillingService");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new Order());
        }

        [HttpPost]
        public async Task<IActionResult> Index(Order order)
        {
            order.UserId = Guid.NewGuid().ToString();
            var response = await _httpClient.PostAsJsonAsync("api/billing/process", order);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Order processing failed. Please try again later.");
                return View(order);
            }

            var receipt = await response.Content.ReadAsAsync<Receipt>();
            return View("Result", receipt);
        }
        [HttpGet]
        public async Task<IActionResult> Result(Receipt receipt)
        {
            return View(receipt);
        }
    }
}
