using System;
using System.Linq;
using System.Data.Linq;
using System.Data.Entity;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CNSirindar.Models;

namespace CNSirindar.Models
{
    [Table("TblEntrenadores")]
    public class Entrenador : Persona
    {
        public Entrenador()
        {
            this.Dependencias = new HashSet<Dependencia>();
        }

        public virtual int EntrenadorId { get; set; }

        public virtual ICollection<Deporte> Deportes { get; set; }
        public virtual ICollection<Dependencia> Dependencias { get; set; }

        public static Entrenador Read(int id)
        {
            var entity = new Entrenador();
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity = db.Entrenadores.Find(id);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return entity;
        }

        public static bool Create(Entrenador entity)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    db.Entry(entity);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Update(Entrenador entity)
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
                    var entity = db.Entrenadores.Find(id);
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

        public static IList<Entrenador> list()
        {
            var list = new List<Entrenador>();
            using (var db = new SirindarDbContext())
            {
                list = db.Entrenadores.ToList();
            }
            return list;
        }
    }
}
