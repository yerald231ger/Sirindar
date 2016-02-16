using System;
using System.Linq;
using System.Data.Entity;
using CNSirindar.Models;
using System.Collections.Generic;
using CNSirindar.Extensions;

namespace CNSirindar.Repositories
{
    public class BloqueRepository : IRepository<Bloque, int>
    {
        public Bloque Read(int? id)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    var entity = db.Bloques.WhereIsActive().First(b => b.BloqueId == id);
                    entity.Grupos = entity.Grupos
                        .WhereIsActive()
                        .Select(g => new Grupo
                        {
                            GrupoAlimenticio = new GrupoAlimenticio { Grupo = g.GrupoAlimenticio.Grupo },
                            BloqueId = g.BloqueId,
                            GrupoId = g.GrupoId,
                            Gramos = g.Gramos,
                            Kilocalorias = g.Kilocalorias,
                            Nombre = g.Nombre,
                            Porcentaje = g.Porcentaje,
                            Equivalencias = g.Equivalencias
                        }).ToList();
                    return entity;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool Create(Bloque entity)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity.EsActivo = true;
                    entity.FechaAlta = DateTime.Now;
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

        public bool Update(Bloque entity)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity.EsActivo = true;
                    entity.FechaAlta = DateTime.Now;
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

        public bool Delete(int? id)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    var entity = db.Bloques.Find(id);
                    entity.EsActivo = false;
                    entity.FechaModificacion = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Bloque> List()
        {
            using (var db = new SirindarDbContext())
            {
                return db.Bloques.WhereIsActive().Select(gn =>
                        new Bloque
                        {
                            BloqueId = gn.BloqueId,
                            KilocaloriasTotales = gn.KilocaloriasTotales,
                            Nombre = gn.Nombre
                        }
                    ).ToList();
            }
        }

    }
}