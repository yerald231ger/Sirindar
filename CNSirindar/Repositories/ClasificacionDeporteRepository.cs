using System;
using System.Linq;
using System.Data.Entity;
using CNSirindar.Models;
using System.Collections.Generic;
using CNSirindar.Extensions;

namespace CNSirindar.Repositories
{
    public class ClasificacionDeporteRepository: IRepository<ClasificacionDeporte,int>
    {
        public ClasificacionDeporte Read(int? id)
        {
            var entity = new ClasificacionDeporte();
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity = db.ClasificacionesDeportes.Find(id);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return entity;
        }

        public bool Create(ClasificacionDeporte entity)
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

        public bool Update(ClasificacionDeporte entity)
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

        public bool Delete(int? id)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    var entity = db.ClasificacionesDeportes.Find();
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

        public List<ClasificacionDeporte> List()
        {
            var list = new List<ClasificacionDeporte>();
            using (var db = new SirindarDbContext())
            {
                db.ClasificacionesDeportes.WhereIsActive().ForEach(cd =>
                list.Add(new ClasificacionDeporte
                {
                    Abreviatura = cd.Abreviatura,
                    Descripcion = cd.Descripcion,
                    ClasificacionDeporteId = cd.ClasificacionDeporteId
                }
                    ));
            }
            return list;
        }
    }
}