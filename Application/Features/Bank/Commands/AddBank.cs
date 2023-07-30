using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;

namespace Application.Features.Bank.Commands;

public class AddBankRequest : IRequest<Response<BankDto>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class AddBankHandler : IRequestHandler<AddBankRequest, Response<BankDto>>
{
    private readonly IBankRepository _bankRepository;
    private readonly IMapper _mapper;

    public AddBankHandler(
        IBankRepository bankRepository,
        IMapper mapper)
    {
        _bankRepository = bankRepository;
        _mapper = mapper;
    }

    public async Task<Response<BankDto>> Handle(
        AddBankRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Name))
        {
            throw new PropertyCannotBeNullException(nameof(AddBankRequest.Name));
        }

        if (string.IsNullOrEmpty(request.Code))
        {
            throw new PropertyCannotBeNullException(nameof(AddBankRequest.Code));
        }

        var checkBank = _bankRepository.GetByCodeAsync(request.Code);

        if (checkBank != null)
        {
            throw new AlreadyExistAnEntityWithThisValueException(nameof(Domain.Entities.Bank), request.Code);
        }

        var bank = new Domain.Entities.Bank(request.Name, request.Code);

        await _bankRepository.InsertAsync(bank);

        var bankDto = _mapper.Map<BankDto>(bank);

        return new Response<BankDto>(bankDto);
    }
}