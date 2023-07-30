using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;

namespace Application.Features.Bank.Queries;

public class ListBankQuery : IRequest<Response<IList<BankDto>>>
{
}

public class ListBankHandler : IRequestHandler<ListBankQuery, Response<IList<BankDto>>>
{
    private readonly IBankRepository _bankRepository;
    private readonly IMapper _mapper;

    public ListBankHandler(
        IBankRepository bankRepository,
        IMapper mapper)
    {
        _bankRepository = bankRepository;
        _mapper = mapper;
    }

    public async Task<Response<IList<BankDto>>> Handle(ListBankQuery request, CancellationToken cancellationToken)
    {
        var banks = await _bankRepository.ListAllAsync();

        var banksDto = _mapper.Map<IList<BankDto>>(banks);

        return new Response<IList<BankDto>>(banksDto);
    }
}