using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;

namespace Application.Features.Bank.Queries;

public class GetBankQuery : IRequest<Response<BankDto?>>
{
    public int Id { get; set; }
}

public class GetBankHandler : IRequestHandler<GetBankQuery, Response<BankDto?>>
{
    private readonly IBankRepository _bankRepository;
    private readonly IMapper _mapper;

    public GetBankHandler(
        IBankRepository bankRepository,
        IMapper mapper)
    {
        _bankRepository = bankRepository;
        _mapper = mapper;
    }

    public async Task<Response<BankDto?>> Handle(GetBankQuery request, CancellationToken cancellationToken)
    {
        var bank = await _bankRepository.GetByIdAsync(request.Id);

        var bankDto = _mapper.Map<BankDto>(bank);

        return new Response<BankDto?>(bankDto);
    }
}