using System;
namespace Sirindar.Common
{
    public class Horario : TableDbConventions
    {
        public int HorarioId { get; set; }
        public ComidasDia Nombre { get; set; }
        public virtual TimeSpan Inicia { get; set; }
        public virtual TimeSpan Finaliza { get; set; }      
    }

    public enum ComidasDia
    {
        Desayuno,
        Comida,
        Cena,
        Ninguna
    }
}