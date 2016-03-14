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
        [Display(Name="Fecha de nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public virtual DateTime FechaNacimiento { get; set; }

        [Required]
        public virtual Status Status { get; set; }

        [Required]
        [IsDependencia(ErrorMessage="La dependencia sseleccionada no existe")]
        [Display(Name = "Dependencia")]
        public virtual int DependenciaId { get; set; }
        
        [Required]
        [Display(Name="Deporte")]
        [IsDeporte(ErrorMessage="El deporte seleccionado no existe")]
        public virtual int DeporteId { get; set; }

        [Display(Name = "Inicia")]
        [DataType(DataType.Time,ErrorMessage="No es un formato correcto")]
        public virtual Nullable<TimeSpan> IniciaEntrenamiento { get; set; }

        [Display(Name = "Finaliza")]
        [DataType(DataType.Time)]
        public virtual Nullable<TimeSpan> FinalizaEntrenamiento { get; set; }
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
        [IsDependencia(ErrorMessage = "La dependencia sseleccionada no existe")]
        [Display(Name = "Dependencia")]
        public virtual int DependenciaId { get; set; }
    }

    public class CreateDeporteDeportistaViewModel
    {
        public virtual int DeportistaId { get; set; }

        [Required]
        [IsDeporte(ErrorMessage = "El deporte seleccionado no existe")]
        public virtual int DeporteId { get; set; }
        
        [DataType(DataType.Time)]
        public Nullable<TimeSpan> IniciaEntrenamiento { get; set; }

        [DataType(DataType.Time)]
        public Nullable<TimeSpan> FinalizaEntrenamiento { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed class IsDependenciaAttribute : ValidationAttribute
    {
        public IsDependenciaAttribute() { }

        public override bool IsValid(object value)
        {
            return GeneralRepository.IsDependencia((int)value);
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed class IsDeporteAttribute : ValidationAttribute
    {
        public IsDeporteAttribute() { }

        public override bool IsValid(object value)
        {
            return GeneralRepository.IsDeporte((int)value);
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class IsDeportistaAttribute : ValidationAttribute
    {
        public IsDeportistaAttribute() { }

        public override bool IsValid(object value)
        {
            return GeneralRepository.IsDeportista((int)value);
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class IsBloqueAttribute : ValidationAttribute
    {
        public IsBloqueAttribute() { }

        public override bool IsValid(object value)
        {
            return GeneralRepository.IsBloque((int)value);
        }
    }

}