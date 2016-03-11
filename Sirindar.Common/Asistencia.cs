using System;
namespace Sirindar.Common
{
    public class Asistencia : TableDbConventions
    {
        public virtual int AsistenciaId { get; set; }
        public virtual DateTime HoraAsistencia { get; set; }
        public int DeportistaId { get; set; }
        public int HorarioId { get; set; }        
        public Deportista Deportista { get; set; }
        public Horario Horario { get; set; }   
    }

}
