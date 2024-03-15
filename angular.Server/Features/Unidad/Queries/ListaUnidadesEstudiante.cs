using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using DTOs;
using Interfaces.Repositories;
using MediatR;

namespace Application.Features.Unidad.Queries
{
    public class ListaUnidadesEstudianteQuery : IRequest<Response<IList<UnidadDto>>>
    {
        public int EstudianteId;
    }
    public class ListaUnidadesEstudianteHandler : IRequestHandler<ListaUnidadesEstudianteQuery, Response<IList<UnidadDto>>>
    {
        private readonly IEvaluacionPsicologicaEstudianteRepository _evaluacionPsicologicaEstudiante;
        private readonly IMapper _mapper;

        public ListaUnidadesEstudianteHandler(
            IEvaluacionPsicologicaEstudianteRepository evaluacionPsicologicaEstudianteRepository,
            IMapper mapper)
        {
            _evaluacionPsicologicaEstudiante = evaluacionPsicologicaEstudianteRepository; 
            _mapper = mapper;
        }
        public async Task<Response<IList<UnidadDto>>> Handle(ListaUnidadesEstudianteQuery request, CancellationToken cancellationToken)
        {            
            var unidades = await _evaluacionPsicologicaEstudiante.ListaUnidadesDeEvaPsiEstPorIdEstudiante(request.EstudianteId) ?? throw new EntidadNoEncontradaException(nameof(Entities.Unidad));
   
            var unidadesDto = _mapper.Map<IList<UnidadDto>>(unidades);
            return new Response<IList<UnidadDto>>(unidadesDto);
        }
    }
}
