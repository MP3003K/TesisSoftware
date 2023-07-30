using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;

namespace Application.Features.Bank.Commands;

public class UpdateBankRequest : BankDto, IRequest<Response<BankDto>>
{
}

public class UpdateBankHandler : IRequestHandler<UpdateBankRequest, Response<BankDto>>
{
    private readonly IBankRepository _bankRepository;
    private readonly IMapper _mapper;

    public UpdateBankHandler(
        IBankRepository bankRepository,
        IMapper mapper)
    {
        _bankRepository = bankRepository;
        _mapper = mapper;
    }

    public async Task<Response<BankDto>> Handle(
        UpdateBankRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Name))
        {
            throw new PropertyCannotBeNullException(nameof(UpdateBankRequest.Name));
        }

        if (string.IsNullOrEmpty(request.Code))
        {
            throw new PropertyCannotBeNullException(nameof(UpdateBankRequest.Code));
        }

        var checkBank = _bankRepository.GetByCodeAsync(request.Code);

        if (checkBank != null && checkBank.Id != request.Id)
        {
            throw new AlreadyExistAnEntityWithThisValueException(nameof(Domain.Entities.Bank), request.Code);
        }

        var bank = await _bankRepository.GetByIdAsync(request.Id);

        if (bank == null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.Bank));
        }

        bank.Update(request.Name, request.Code);

        await _bankRepository.UpdateAsync(bank);

        var bankDto = _mapper.Map<BankDto>(bank);

        return new Response<BankDto>(bankDto);
    }
}