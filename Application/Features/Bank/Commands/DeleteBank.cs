using Application.Exceptions;
using Application.Wrappers;
using Contracts.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Bank.Commands;

public class DeleteBankRequest : IRequest<Response<bool>>
{
    public int Id { get; set; }
}

public class DeleteBankHandler : IRequestHandler<DeleteBankRequest, Response<bool>>
{
    private readonly IBankRepository _bankRepository;
    private readonly IPixRepository _accountRepository;

    public DeleteBankHandler(
        IBankRepository bankRepository,
        IPixRepository accountRepository)
    {
        _bankRepository = bankRepository;
        _accountRepository = accountRepository;
    }

    public async Task<Response<bool>> Handle(
        DeleteBankRequest request,
        CancellationToken cancellationToken)
    {
        var bank = await _bankRepository.GetByIdAsync(request.Id);

        if (bank == null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.Bank));
        }

        var hasPixies = await _accountRepository.HasPixiesByBankIdAsync(request.Id);

        if (hasPixies)
        {
            throw new EntityCannotBeRemovedByDependencyException(nameof(Pix));
        }

        await _bankRepository.DeleteAsync(bank);

        return new Response<bool>(true);
    }
}