using CNSirindar.Models;
using System;
using System.Collections.Generic;
using CNSirindar.Extensions;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CNSirindar.Repositories
{
    public class GrupoRepository : IRepository<Grupo, int>
    {
        public Grupo Read(int? id)
        {
            var entity = new Grupo();
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity = db.Grupos.Include("GrupoAlimenticio").Include("Bloque").First(g => g.GrupoId == id);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return entity;
        }

        public bool Create(Grupo entity)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity.EsActivo = true;
                    entity.FechaAlta = DateTime.Now;
                    db.Entry(entity).State = EntityState.Added;
                    db.SaveChanges();

                    GeneralRepository.SumaKilocalorias(entity.BloqueId);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Update(Grupo entity)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity.EsActivo = true;
                    entity.FechaModificacion = DateTime.Now;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();

                    GeneralRepository.SumaKilocalorias(entity.BloqueId);
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
                    var entity = db.Grupos.Find(id);
                    entity.FechaModificacion = DateTime.Now;
                    entity.EsActivo = false;
                    db.SaveChanges();

                    GeneralRepository.SumaKilocalorias(entity.BloqueId);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Grupo> List()
        {
            var list = new List<Grupo>();
            using (var db = new SirindarDbContext())
            {
                db.Grupos.WhereIsActive().ForEach(gb =>
                        list.Add(new Grupo
                        {
                            GrupoId = gb.GrupoId,
                            Equivalencias = gb.Equivalencias,
                            Nombre = gb.Nombre,
                            Gramos = gb.Gramos,
                            GrupoAlimenticio = new GrupoAlimenticio { Grupo = gb.GrupoAlimenticio.Grupo, GrupoAlimenticioId = gb.GrupoAlimenticio.GrupoAlimenticioId },
                            Kilocalorias = gb.Kilocalorias,
                            Porcentaje = gb.Porcentaje,
                            BloqueId = gb.BloqueId,
                            Bloque = new Bloque { BloqueId = gb.BloqueId, KilocaloriasTotales = gb.Kilocalorias, Nombre = gb.Nombre }
                        })
                    );
            }
            return list;
        }
    }
}