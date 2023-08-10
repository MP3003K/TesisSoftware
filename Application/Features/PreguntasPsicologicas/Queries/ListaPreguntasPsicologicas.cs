using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RespuestasPsicologicas.Queries
{
    public class ListaPreguntasPsicologicasQuery: IRequest<Response<IList<PreguntaPsicologicaDto>>>
    {
    }
    public class ListaPreguntasPsicologicasHandler : IRequestHandler<ListaPreguntasPsicologicasQuery, Response<IList<PreguntaPsicologicaDto>>>
    {
        private readonly IPreguntaPsicologicaRepository _preguntaPsicologicaRepository;
        private readonly IMapper _mapper;

        public ListaPreguntasPsicologicasHandler(
                       IPreguntaPsicologicaRepository preguntaPsicologicaRepository,
                                  IMapper mapper)
        {
            _preguntaPsicologicaRepository = preguntaPsicologicaRepository;
            _mapper = mapper;
        }

        public async Task<Response<IList<PreguntaPsicologicaDto>>> Handle(ListaPreguntasPsicologicasQuery request, CancellationToken cancellationToken)
        {
            var preguntasPsicologicas = await _preguntaPsicologicaRepository.ListAllAsync();

            var preguntasPsicologicasDto = _mapper.Map<IList<PreguntaPsicologicaDto>>(preguntasPsicologicas);

            return new Response<IList<PreguntaPsicologicaDto>>(preguntasPsicologicasDto);
        }
    }
}
