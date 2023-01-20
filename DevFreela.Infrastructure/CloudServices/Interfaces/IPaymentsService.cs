using DevFreela.Core.DTOs;

namespace DevFreela.Infrastructure.CloudServices.Interfaces
{
    public interface IPaymentsService
    {
        void ProcessPayment(PaymentInfoDTO paymentInfoDTO);
    }
}