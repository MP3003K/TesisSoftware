using Application.Exceptions;
using Application.Wrappers;
using Contracts.Repositories;
using MediatR;

namespace Application.Features.Pix.Commands;

public class DeletePixRequest : IRequest<Response<bool>>
{
    public string? Key { get; set; }
}

public class DeletePixHandler : IRequestHandler<DeletePixRequest, Response<bool>>
{
    private readonly IPixRepository _pixRepository;

    public DeletePixHandler(
        IPixRepository pixRepository)
    {
        _pixRepository = pixRepository;
    }

    public async Task<Response<bool>> Handle(
        DeletePixRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Key == null)
        {
            throw new PropertyCannotBeNullException(nameof(DeletePixRequest.Key));
        }

        var pix = await _pixRepository.GetByKeyAsync(request.Key);

        if (pix == null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.Pix));
        }

        await _pixRepository.DeleteAsync(pix);

        return new Response<bool>(true);
    }
}