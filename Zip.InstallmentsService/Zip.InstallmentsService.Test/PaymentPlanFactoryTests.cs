using Shouldly;
using Xunit;

namespace Zip.InstallmentsService.Test
{
    public class PaymentPlanFactoryTests
    {
        [Fact]
        public void WhenCreatePaymentPlanWithValidOrderAmount_ShouldReturnValidPaymentPlan()
        {
            // Arrange
            var paymentPlanFactory = new PaymentPlanFactory();
            
            // Act
            var paymentPlan = paymentPlanFactory.CreatePaymentPlan(100,5);
            
            //Assert
            Assert.NotNull(paymentPlan);
            Assert.Equal(20, paymentPlan.Installments[0].Amount);
            Assert.Equal(5, paymentPlan.Installments.Length);
            
        }
    }
}
