using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Sirindar.Core;

namespace Sirindar.Models
{
    public class DeportistaViewModel
    {
        [Required]
        [MaxLength(10)]
        [Remote("MatriculaExist", "Validations", ErrorMessage = "La matricula ya existe")]
        public virtual string Matricula { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public virtual string Nombre { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public virtual string Apellidos { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual Generos Genero { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public virtual DateTime FechaNacimiento { get; set; }

        [Required]
        public virtual Status Status { get; set; }

        [Required]
        [Display(Name = "Dependencia")]
        [Remote("IsDependencia", "Validations", ErrorMessage = "La dependencia no existe")]
        public virtual int DependenciaId { get; set; }

        [Required]
        [Display(Name = "Deporte")]
        [Remote("IsDeporte", "Validations", ErrorMessage = "El deporte no existe")]
        public virtual int DeporteId { get; set; }

        [Display(Name = "Inicia")]
        [DataType(DataType.Time, ErrorMessage = "No es un formato correcto")]
        public virtual TimeSpan IniciaEntrenamiento { get; set; }

        [Display(Name = "Finaliza")]
        [DataType(DataType.Time)]
        public virtual TimeSpan FinalizaEntrenamiento { get; set; }
    }

    public class DeportistaEditViewModel
    {
        [Required]
        public int DeportistaId { get; set; }

        [Required]
        [MaxLength(10)]
        public virtual string Matricula { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public virtual string Nombre { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public virtual string Apellidos { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual Generos Genero { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public virtual DateTime FechaNacimiento { get; set; }

        [Required]
        public virtual Status Status { get; set; }

        [Required]
        [Display(Name = "Dependencia")]
        [Remote("IsDependencia", "Validations", ErrorMessage = "La dependencia no existe")]
        public virtual int DependenciaId { get; set; }
    }

    public class CreateDeporteDeportistaViewModel
    {
        public virtual int DeportistaId { get; set; }

        [Required]
        [Remote("IsDeporte", "Validations", ErrorMessage = "El deporte no existe")]
        public virtual int DeporteId { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan IniciaEntrenamiento { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan FinalizaEntrenamiento { get; set; }
    }
    
}