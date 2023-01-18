using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.CloudServices.Interfaces;
using MediatR;

namespace DevFreela.Application.Commands.FinishProject
{
    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IPaymentsService _paymentsService;
        public FinishProjectCommandHandler(IProjectRepository projectRepository, IPaymentsService paymentsService)
        {
            _projectRepository = projectRepository;
            _paymentsService = paymentsService;
        }
        public async Task<bool> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.IdProject);

            project.Finish();

            var paymentInfoDTO = new PaymentInfoDTO
            {
                IdProject = request.IdProject,
                CreditCardNumber = request.CreditCardNumber,
                Cvv = request.Cvv,
                ExpiresAt = request.ExpiresAt,
                Amount = request.Amount,
                FullName = request.FullName
            };

            var result = await _paymentsService.ProcessPayment(paymentInfoDTO);

            if (!result)
                project.SetPaymentPending();

            await _projectRepository.SaveChangesAsync();

            return result;
        }
    }
}