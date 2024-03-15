using System.ComponentModel.DataAnnotations;

namespace Entities;

public class ReporteEstudiante
{

    [Key]
    public int IdEstudiante { get; set; }
    public string Nombres { get; set; }
    public int IdDimension { get; set; }
    public string NombreDimension { get; set; }
    public int IdEscala { get; set; }
    public string NombreEscala { get; set; }
    public int IdIndicador { get; set; }
    public string NombreIndicador { get; set; }
    public decimal PromedioIndicador { get; set; }
    public decimal PromedioEscala { get; set; }
    public decimal PromedioDimension { get; set; }

}