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

namespace Application.Features.PreguntaPsicologica.Queries
{
    public class ListaPreguntasPsicologicasQuery: IRequest<Response<IList<PreguntaPsicologicaDto>>>
    {
        public int EvaPsiId { get; set; }
        public int EvaPsiEstId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class ListaPreguntasYRespuestasPsicologicasHandler : IRequestHandler<ListaPreguntasPsicologicasQuery, Response<IList<PreguntaPsicologicaDto>>>
    {
        private readonly IPreguntaPsicologicaRepository _preguntaPsicologicaRepository;
        private readonly IRespuestaPsicologicaRepository _respuestaPsicologicaRepository;
        private readonly IMapper _mapper;

        public ListaPreguntasYRespuestasPsicologicasHandler(
                       IPreguntaPsicologicaRepository preguntaPsicologicaRepository,
                       IRespuestaPsicologicaRepository respuestaPsicologicaRepository,
                                  IMapper mapper)
        {
            _preguntaPsicologicaRepository = preguntaPsicologicaRepository;
            _respuestaPsicologicaRepository = respuestaPsicologicaRepository;
            _mapper = mapper;
        }

        public async Task<Response<IList<PreguntaPsicologicaDto>>> Handle(ListaPreguntasPsicologicasQuery request, CancellationToken cancellationToken)
        {
            var evaPsiId = request.EvaPsiId;
            var pageNumber = request.PageNumber;
            var pageSize = request.PageSize;
            var evaPsiEstId = request.EvaPsiEstId;

            var preguntasPsicologicas = await _preguntaPsicologicaRepository.PreguntaPsicologicasPaginadas(evaPsiId, pageSize, pageNumber);
            var preguntasPsicologicasDto = _mapper.Map<IList<PreguntaPsicologicaDto>>(preguntasPsicologicas);
            foreach (var pregunta in preguntasPsicologicasDto)
            {
                pregunta.Respuesta = await _respuestaPsicologicaRepository.RespuestaDeUnaPregunta(evaPsiEstId, pregunta.Id);
            }

            return new Response<IList<PreguntaPsicologicaDto>>(preguntasPsicologicasDto);
        }
    }
}
