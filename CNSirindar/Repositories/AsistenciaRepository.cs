using System;
using System.Linq;
using System.Data.Entity;
using CNSirindar.Models;
using System.Collections.Generic;
using CNSirindar.Extensions;

namespace CNSirindar.Repositories
{
    public class AsistenciaRepository: IRepository<Asistencia,int>
    {
        #region CRUD
        public Asistencia Read(int? id)
        {
            var entity = new Asistencia();
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity = db.Asistencias.Find(id);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return entity;
        }

        public bool Create(Asistencia entity)
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

        public bool Update(Asistencia entity)
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
                    var entity = db.Asistencias.Find(id);
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
        #endregion        
    

        public List<Asistencia> List()
        {
            throw new NotImplementedException();
        }
    }
}