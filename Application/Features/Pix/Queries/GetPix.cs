using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;

namespace Application.Features.Pix.Queries;

public class GetPixQuery : IRequest<Response<PixDto?>>
{
    public int Id { get; set; }
}

public class GetPixHandler : IRequestHandler<GetPixQuery, Response<PixDto?>>
{
    private readonly IPixRepository _pixRepository;
    private readonly IMapper _mapper;

    public GetPixHandler(
        IPixRepository pixRepository,
        IMapper mapper)
    {
        _pixRepository = pixRepository;
        _mapper = mapper;
    }

    public async Task<Response<PixDto?>> Handle(GetPixQuery request, CancellationToken cancellationToken)
    {
        var pix = await _pixRepository.GetByIdAsync(request.Id);

        var pixDto = _mapper.Map<PixDto>(pix);

        return new Response<PixDto?>(pixDto);
    }
}