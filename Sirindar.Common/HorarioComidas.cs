namespace Sirindar.Common
{
    public class HorarioComidas : TableDbConventions
    {
        public int DeportistaId { get; set; }
        public NumeroComidas Cantidad { get; set; }
        public bool Lunes { get; set; }
        public bool Martes { get; set; }
        public bool Miercoles { get; set; }
        public bool Jueves { get; set; }
        public bool Viernes { get; set; }
        public bool Sabado { get; set; }
        public bool Domingo { get; set; }
        public virtual Deportista Deportista { get; set; }
    }

    public enum NumeroComidas
    {
        Uno = 1,
        Dos = 2,
        Tres = 3
    }
}