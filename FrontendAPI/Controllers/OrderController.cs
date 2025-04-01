using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            var response = await _httpClient.PostAsJsonAsync("api/billing/process", order);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Error in order processing, please contact support team: +(371)27138452");
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
