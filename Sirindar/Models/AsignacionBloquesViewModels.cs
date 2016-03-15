using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Sirindar.Models
{
    public class CreateAsignacionBloquesViewModel
    {
        [Required]
        [Remote("IsDeportista", "Validations", ErrorMessage = "El deportista no existe")]
        public virtual int DeportistaId { get; set; }

        [Required]
        [Remote("IsDeporte", "Validations", ErrorMessage = "El deporte no existe")]
        public virtual int DeporteId { get; set; }

        [Required]
        [Remote("IsBloque", "Validations", ErrorMessage = "El deporte no existe")]
        public virtual int BloqueId { get; set; }
    }

    public class UpdateAsignacionBloquesViewModel
    {
        public virtual int AsignacionBloqueId { get; set; }

        [Required]
        [Remote("IsDeportista", "Validations", ErrorMessage = "El deportista no existe")]
        public virtual int DeportistaId { get; set; }

        [Required]
        [Remote("IsDeporte", "Validations", ErrorMessage = "El deporte no existe")]
        public virtual int DeporteId { get; set; }

        [Required]
        [Remote("IsBloque", "Validations", ErrorMessage = "El deporte no existe")]
        public virtual int BloqueId { get; set; }

    }
}