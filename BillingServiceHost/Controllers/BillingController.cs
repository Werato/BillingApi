using Microsoft.AspNetCore.Mvc;
using BillingCore.Models;
using BillingServiceHost.Services;
using System;

namespace BillingServiceHost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        /// <summary>
        /// Test endpoint to check if the Billing API is running.
        /// </summary>
        [HttpGet("process")]
        public ActionResult<Receipt> Process()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Processes an order and returns a receipt.
        /// </summary>
        /// <param name="order">Order data to process</param>
        /// <returns>Receipt object containing payment details</returns>
        [HttpPost("process")]
        public ActionResult<Receipt> Process([FromBody] Order order)
        {
            try
            {
                var receipt = _billingService.ProcessOrder(order);
                return Ok(receipt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
