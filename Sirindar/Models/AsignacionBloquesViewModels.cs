using System.ComponentModel.DataAnnotations;

namespace Sirindar.Models
{
    public class CreateAsignacionBloquesViewModel
    {
        [Required]
        [IsDeportista(ErrorMessage = "El Deportista seleccionado no exitste")]
        public virtual int DeportistaId { get; set; }

        [Required]
        [IsDeporte(ErrorMessage = "El dDorte seleccionado no existe")]
        public virtual int DeporteId { get; set; }

        [Required]
        [IsBloque(ErrorMessage = "El Bloque seleccionado no exitste")]
        public virtual int BloqueId { get; set; }
    }

    public class UpdateAsignacionBloquesViewModel
    {
        public virtual int AsignacionBloqueId { get; set; }

        [Required]
        [IsDeportista(ErrorMessage = "El Deportista seleccionado no exitste")]
        public virtual int DeportistaId { get; set; }

        [Required]
        [IsDeporte(ErrorMessage = "El dDorte seleccionado no existe")]
        public virtual int DeporteId { get; set; }

        [Required]
        [IsBloque(ErrorMessage = "El Bloque seleccionado no exitste")]
        public virtual int BloqueId { get; set; }

    }
}