using System;
namespace Sirindar.Common
{
    public class Persona : TableDbConventions
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Generos Genero { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}",Nombre, Apellidos);
        }
    }

    public enum Generos {
        Hombre,
        Mujer
    }

}
