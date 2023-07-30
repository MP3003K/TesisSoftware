using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;

namespace Application.Features.Pix.Queries;

public class ListPixQuery : IRequest<Response<IList<PixDto>>>
{
}

public class ListPixHandler : IRequestHandler<ListPixQuery, Response<IList<PixDto>>>
{
    private readonly IPixRepository _pixRepository;
    private readonly IMapper _mapper;

    public ListPixHandler(
        IPixRepository accountRepository,
        IMapper mapper)
    {
        _pixRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<Response<IList<PixDto>>> Handle(ListPixQuery request, CancellationToken cancellationToken)
    {
        var pixes = await _pixRepository.ListAllAsync();

        var pixesDto = _mapper.Map<IList<PixDto>>(pixes);

        return new Response<IList<PixDto>>(pixesDto);
    }
}