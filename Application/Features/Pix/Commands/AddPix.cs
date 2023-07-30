using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;

namespace Application.Features.Pix.Commands;

public class AddPixRequest : IRequest<Response<PixDto>>
{
    public string? BankCode { get; set; }
    public string? Key { get; set; }
}

public class AddPixHandler : IRequestHandler<AddPixRequest, Response<PixDto>>
{
    private readonly IPixRepository _pixRepository;
    private readonly IBankRepository _bankRepository;
    private readonly IMapper _mapper;

    public AddPixHandler(
        IPixRepository pixRepository,
        IBankRepository bankRepository,
        IMapper mapper)
    {
        _pixRepository = pixRepository;
        _bankRepository = bankRepository;
        _mapper = mapper;
    }

    public async Task<Response<PixDto>> Handle(
        AddPixRequest request,
        CancellationToken cancellationToken)
    {
        if (request.BankCode == null)
        {
            throw new PropertyCannotBeNullException(nameof(AddPixRequest.BankCode));
        }

        var bank = await _bankRepository.GetByCodeAsync(request.BankCode);

        if (bank == null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.Bank));
        }

        if (string.IsNullOrEmpty(request.Key))
        {
            throw new PropertyCannotBeNullException(nameof(AddPixRequest.Key));
        }

        var pix = await _pixRepository.GetByKeyAsync(request.Key);

        if (pix != null)
        {
            throw new AlreadyExistAKeyWithThisValueException(request.Key);
        }

        pix = new Domain.Entities.Pix(bank.Id, request.Key);

        await _pixRepository.InsertAsync(pix);

        var pixDto = _mapper.Map<PixDto>(pix);

        return new Response<PixDto>(pixDto);
    }
}