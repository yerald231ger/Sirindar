using System;
using System.Linq;
using System.Data.Linq;
using System.Data.Entity;
using System.Collections;
using System.Collections.Generic;
using CNSirindar.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CNSirindar.Models;
using CNSirindar.Repositories;

namespace CNSirindar.Models
{
    [Table("TblDeportistas")]
    public class Deportista : Persona
    {
        private ICollection<Deporte> deportes;
        
        public Deportista()
        {
            this.Asistencias = new HashSet<Asistencia>();
            this.AsignacionesBloques = new HashSet<AsignacionBloque>();
            this.DeportesDeportistas = new HashSet<DeporteDeportista>();
        }

        public Deportista(int deportistaId)
        {
            this.DeportistaId = deportistaId;
            this.Asistencias = new HashSet<Asistencia>();
            this.AsignacionesBloques = new HashSet<AsignacionBloque>();
            this.DeportesDeportistas = new HashSet<DeporteDeportista>();
            this.deportes = GeneralRepository.GetDeportes(deportistaId);
        }

        public virtual int DeportistaId { get; set; }
        public virtual int DependenciaId { get; set; }

        [Required]
        [MaxLength(10)]
        public virtual string Matricula { get; set; }

        [Required]
        public virtual Status Status { get; set; }

        [DataType(DataType.DateTime)]
        public virtual Nullable<DateTime> FechaRegistro { get; set; }

        public virtual Dependencia Dependencia { get; set; }

        public virtual CantidadComidas CantidadComidas { get; set; }

        [NotMapped]
        public virtual ICollection<Deporte> Deportes
        {
            get
            {
                if (deportes == null)
                {
                    deportes = GeneralRepository.GetDeportes(this.DeportistaId);
                    return deportes;
                }
                else
                    return deportes;
            }
        }

        public virtual ICollection<Asistencia> Asistencias { get; set; }
        public virtual ICollection<AsignacionBloque> AsignacionesBloques { get; set; }
        public virtual ICollection<DeporteDeportista> DeportesDeportistas { get; set; }

    }

    public enum Status
    {
        Baja,
        Alta
    }
}
