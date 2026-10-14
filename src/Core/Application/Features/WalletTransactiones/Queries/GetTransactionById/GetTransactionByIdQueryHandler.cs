using AutoMapper;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Application.Features.WalletTransactiones.Dtos;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.WalletTransactiones.Queries.GetTransactionById
{
    public sealed class GetTransactionByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetTransactionByIdQuery, GetWalletTransactionDto>
    {
        public async Task<GetWalletTransactionDto> Handle(GetTransactionByIdQuery request, CancellationToken cts)
        {
            var transaction =
                await unitOfWork.Repository<WalletTransaction>().GetByIdAsync(request.TransactionId, cts);

            if (transaction is null)
                throw new NotFoundException(nameof(WalletTransaction), request.TransactionId);

            return mapper.Map<GetWalletTransactionDto>(transaction);
        }
    }
}
