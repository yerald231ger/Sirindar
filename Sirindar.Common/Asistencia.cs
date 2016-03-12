using System;
namespace Sirindar.Core
{
    public class Asistencia : TableDbConventions
    {
        public int AsistenciaId { get; set; }
        public DateTime HoraAsistencia { get; set; }
        public int DeportistaId { get; set; }
        public int HorarioId { get; set; }        
        public virtual Deportista Deportista { get; set; }
        public virtual Horario Horario { get; set; }   
    }

}
