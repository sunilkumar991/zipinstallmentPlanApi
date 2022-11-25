using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zip.InstallmentsApi.Models;
using Zip.InstallmentsApi.ZipBase;
using Zip.InstallmentsService.Interfaces;

namespace Zip.InstallmentsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstallmentPlanController : BaseClass
    {
        private readonly IPaymentPlanFactory _paymentPlanFactory;

        public InstallmentPlanController(IPaymentPlanFactory paymentPlanFactory)
        {
            _paymentPlanFactory = paymentPlanFactory;
        }

        [HttpGet("PaymentPlan/{purchaseAmount}/{numberOfInstallments}")]
        public IActionResult PaymentPlanList(decimal purchaseAmount, int numberOfInstallments)
        {
            return TryCatch(() => {
                var response = new DefaultResponseModel<object>();
                if (purchaseAmount <= 0)
                {
                    response.ErroList.Add("PurchaseAmount Should be greater than 0");
                    return Ok(response);
                }
                if (numberOfInstallments <= 0)
                {
                    response.ErroList.Add("Number Of Installments Should be greater than 0");
                    return Ok(response);
                }

                var result = _paymentPlanFactory.CreatePaymentPlan(purchaseAmount, numberOfInstallments);
                 response = new DefaultResponseModel<object>
                {
                    Data = _paymentPlanFactory.CreatePaymentPlan(purchaseAmount, numberOfInstallments),
                };
                return Ok(response); 
            });
        }
    }
}
