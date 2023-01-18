using DevFreela.Core.DTOs;

namespace DevFreela.Infrastructure.CloudServices.Interfaces
{
    public interface IPaymentsService
    {
        public Task<bool> ProcessPayment(PaymentInfoDTO paymentInfoDTO);
    }
}