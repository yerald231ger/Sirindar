using System;
using CNSirindar.Extensions;
using System.Linq;
using System.Data.Linq;
using System.Data.Entity;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CNSirindar.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data.Entity.Infrastructure;

namespace CNSirindar.Models
{
    [Table("TblDeportes")]
    public class Deporte : TableDbConventions
    {
        public Deporte()
        {
            this.Entrenadores = new HashSet<Entrenador>();
            this.AsignacionesBloques = new HashSet<AsignacionBloque>();
            this.DeportesDeportistas = new HashSet<DeporteDeportista>();
        }

        public virtual int DeporteId { get; set; }

        [MaxLength(50)]
        public virtual string Nombre { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual Energia TipoEnergia { get; set; }

        /// <summary>
        /// Propiedad escalar que se utiliza para mapear la propiedad de navegacion "Clasificacion"
        /// </summary>
        public virtual int ClasificacionDeporteId { get; set; }

        public virtual ClasificacionDeporte Clasificacion { get; set; }
        public virtual ICollection<Entrenador> Entrenadores { get; set; }
        public virtual ICollection<DeporteDeportista> DeportesDeportistas { get; set; }
        public virtual ICollection<AsignacionBloque> AsignacionesBloques { get; set; }
        
        //deporte.Clasificacion = new ClasificacionDeporte { ClasificacionDeporteId = (int)clasificacionDeporteId };
        //var prevClasificacion = crud.Read(deporte.DeporteId).Clasificacion;
        //Deporte.Update(deporte, prevClasificacion);

        //public static bool Update(Deporte entity, ClasificacionDeporte prevClasificacion)
        //{
        //    using (var db = new SirindarDbContext())
        //    {
        //        entity.EsActivo = true;
        //        db.Entry(entity).State = EntityState.Modified;
        //        db.Entry(entity.Clasificacion).State = EntityState.Unchanged;

        //        try
        //        {
        //            db.Entry(prevClasificacion).State = EntityState.Unchanged;

        //            ((IObjectContextAdapter)db).ObjectContext
        //              .ObjectStateManager
        //              .ChangeRelationshipState(
        //                entity,
        //                entity.Clasificacion,
        //                l => l.Clasificacion,
        //                EntityState.Added);

        //            ((IObjectContextAdapter)db).ObjectContext
        //              .ObjectStateManager
        //              .ChangeRelationshipState(
        //                entity,
        //                prevClasificacion,
        //                l => l.Clasificacion,
        //                EntityState.Deleted);
        //            db.SaveChanges();
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}

    }

    public enum Energia
    {
        Aerobico,
        Anaerobico,
        AerobicoAnaerobico
    }
}
