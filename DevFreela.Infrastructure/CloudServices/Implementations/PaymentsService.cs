using System.Text;
using System.Text.Json;
using DevFreela.Core.DTOs;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.CloudServices.Interfaces;

namespace DevFreela.Infrastructure.CloudServices.Implementations
{
    public class PaymentsService : IPaymentsService
    {
        private readonly IMessageBusService _messageBusService;
        private const string QUEUE_NAME = "Payments";
        public PaymentsService(IMessageBusService messageBusService)
        {
            _messageBusService = messageBusService;
        }
        public void ProcessPayment(PaymentInfoDTO paymentInfoDTO)
        {
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);

            var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);

            _messageBusService.Publish(QUEUE_NAME, paymentInfoBytes);
        }
    }
}