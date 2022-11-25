using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zip.InstallmentsService.Interfaces
{
    public interface IPaymentPlanFactory
    {
        PaymentPlan CreatePaymentPlan(decimal purchaseAmount, int numberOfInstallments);
    }
}
