using System;
namespace Sirindar.Common
{
    public class Horario : TableDbConventions
    {
        public int HorarioId { get; set; }
        public ComidasDia Nombre { get; set; }
        public TimeSpan Inicia { get; set; }
        public TimeSpan Finaliza { get; set; }      
    }

    public enum ComidasDia
    {
        Desayuno,
        Comida,
        Cena,
        Ninguna
    }
}