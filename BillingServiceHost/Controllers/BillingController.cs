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
