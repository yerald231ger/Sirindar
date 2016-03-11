using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CNSirindar.Models
{
    public class Persona : TableDbConventions
    {
        [Required]
        [DataType(DataType.Text)]
        public virtual string Nombre { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public virtual string Apellidos { get; set; }

        [DataType(DataType.Date)]
        public virtual Nullable<DateTime> FechaNacimiento { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual Generos Genero { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}",Nombre, Apellidos);
        }
    }

    public enum Generos {
        Hombre,
        Mujer
    }

    //public enum Estados {
    //    Chihuahua,
    //    Sonora,
    //    Coahuila,
    //    Durango,
    //    Oaxaca,
    //    Tamaulipas,
    //    Jalisco,
    //    Zacatecas,
    //    BajaCaliforniaSur,
    //    Chiapas,
    //    Veracruz,
    //    BajaCalifornia,
    //    NuevoLeon,
    //    Guerrero,
    //    SanLuisPotosi,
    //    Michoacan,
    //    Sinaloa,
    //    Campeche,
    //    QuintanaRoo,
    //    Yucatan,
    //    Puebla,
    //    Guanajuato,
    //    Nayarit,
    //    Tabasco,
    //    Mexico,
    //    Hidalgo,
    //    Queretaro,
    //    Colima,
    //    Aguascalientes,
    //    Morelos,
    //    Tlaxcala,
    //    DistritoFederal
    //}
}
