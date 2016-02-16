using System;
using System.Linq;
using System.Data.Entity;
using CNSirindar.Models;
using System.Collections.Generic;
using CNSirindar.Extensions;

namespace CNSirindar.Repositories
{
    public class DeporteRepository : IRepository<Deporte, int>
    {
        public Deporte Read(int? id)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    return db.Deportes.Include(d => d.Clasificacion).First(d => d.DeporteId == id);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool Create(Deporte entity)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity.EsActivo = true;
                    entity.FechaAlta = DateTime.Now;
                    db.Entry(entity).State = EntityState.Added;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool Update(Deporte entity)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity.EsActivo = true;
                    entity.FechaModificacion = DateTime.Now;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool Delete(int? id)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    var entity = db.Deportes.Find(id);
                    entity.FechaModificacion = DateTime.Now;
                    entity.EsActivo = false;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<Deporte> List()
        {
            using (var db = new SirindarDbContext())
            {
                return db.Deportes.WhereIsActive().Select(d => new Deporte
                    {
                        DeporteId = d.DeporteId,
                        Nombre = d.Nombre,
                        TipoEnergia = d.TipoEnergia,
                        Clasificacion = new ClasificacionDeporte
                        {
                            Descripcion = d.Clasificacion.Descripcion,
                            ClasificacionDeporteId = d.Clasificacion.ClasificacionDeporteId
                        }
                    }).ToList();
            }
        }       

    }
}