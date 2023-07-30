using Application.Exceptions;
using Application.Wrappers;
using Contracts.Repositories;
using MediatR;

namespace Application.Features.Transaction.Command;

public class AddTransferRequest : IRequest<Response<bool>>
{
    public string? Key { get; set; }
    public double? Value { get; set; }
}

public class AddTransferHandler : IRequestHandler<AddTransferRequest, Response<bool>>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IPixRepository _pixRepository;

    public AddTransferHandler(
        ITransactionRepository transactionRepository,
        IPixRepository pixRepository)
    {
        _transactionRepository = transactionRepository;
        _pixRepository = pixRepository;
    }

    public async Task<Response<bool>> Handle(
        AddTransferRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Key))
        {
            throw new PropertyCannotBeNullException(nameof(AddTransferRequest.Key));
        }

        var pix = await _pixRepository.GetByKeyAsync(request.Key);

        if (pix == null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.Pix));
        }

        if (request.Value is null or <= 0)
        {
            throw new ValueNeedsBeGreaterThanZeroException();
        }

        var transaction = new Domain.Entities.Transaction(pix.Id, (double) request.Value);

        await _transactionRepository.InsertAsync(transaction);

        return new Response<bool>(true);
    }
}