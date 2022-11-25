using System;
using Zip.InstallmentsService.Interfaces;

namespace Zip.InstallmentsService
{
    /// <summary>
    /// This class is responsible for building the PaymentPlan according to the Zip product definition.
    /// </summary>
    public class PaymentPlanFactory : IPaymentPlanFactory
    {
        /// <summary>
        /// Builds the PaymentPlan instance.
        /// </summary>
        /// <param name="purchaseAmount">The total amount for the purchase that the customer is making.</param>
        /// <returns>The PaymentPlan created with all properties set.</returns>
        public PaymentPlan CreatePaymentPlan(decimal purchaseAmount, int numberOfInstallments)
        {
            DateTime time = DateTime.Now;
            DateTime futureIntallmentDate = DateTime.Now;
            var paymentPlan= new PaymentPlan();
            Installment[] installments= new Installment[numberOfInstallments];
            if(purchaseAmount>0)
            {
                paymentPlan.Id = Guid.NewGuid();
                paymentPlan.PurchaseAmount = purchaseAmount;
                decimal installmentValue = purchaseAmount / numberOfInstallments;
                for (int i = 0; i < numberOfInstallments; i++)
                    { 
                        installments[i] = new Installment();
                        installments[i].Id = Guid.NewGuid();
                        installments[i].Amount = installmentValue;
                        futureIntallmentDate = time.AddDays(14);
                        time = futureIntallmentDate;
                        installments[i].DueDate = Convert.ToDateTime(futureIntallmentDate.ToString("yyyy-MM-dd"));
                    }
            }
            if(installments.Length>0)
            { paymentPlan.Installments= installments;}
            return paymentPlan;
        }
    }
}
