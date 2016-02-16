using System;
using System.Linq;
using System.Data.Linq;
using System.Data.Entity;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CNSirindar.Models
{
    [Table("TblDependencias")]
    public class Dependencia : TableDbConventions
    {
        public Dependencia()
        {
            this.Deportistas = new HashSet<Deportista>();
            this.Entrenadores = new HashSet<Entrenador>();
        }

        public virtual int DependenciaId { get; set; }

        [Required]
        [MaxLength(50)]
        public virtual string Nombre { get; set; }

        [Required]
        [MaxLength(10)]
        public virtual string Clave { get; set; }

        public virtual ICollection<Deportista> Deportistas { get; set; }
        public virtual ICollection<Entrenador> Entrenadores { get; set; }

        public static Dependencia Read(int id)
        {
            var entity = new Dependencia();
            using (var db = new SirindarDbContext())
            {
                try
                {
                    db.ClasificacionesDeportes.Find(id);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return entity;
        }

        public static bool Create(Dependencia entity)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    db.Entry(entity).State = EntityState.Added;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Update(Dependencia entity)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Delete(int id)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    var entity = db.Dependencias.Find(id);
                    entity.EsActivo = false;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public static IList<Dependencia> List()
        {
            var list = new List<Dependencia>();
            using (var db = new SirindarDbContext())
            {
                list = db.Dependencias.ToList();
            }
            return list;
        }
    }
}
