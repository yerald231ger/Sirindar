using CNSirindar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CNSirindar.Extensions;

namespace CNSirindar.Repositories
{
    public class DeporteDeportistaRepository : IRepository<DeporteDeportista, int>
    {
        public bool Create(DeporteDeportista entity)
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

        public DeporteDeportista Read(int? id)
        {
            var entity = new DeporteDeportista();
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity = db.DeportesDeportistas
                        .Include(dd => dd.Deporte)
                        .Include(dd => dd.Deportista)
                        .First(dd => dd.DeporteDeportistaId == id);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return entity;
        }

        public bool Update(DeporteDeportista entity)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity.EsActivo = true;
                    entity.FechaModificacion = DateTime.Now;
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
                    var entity = db.DeportesDeportistas.Find(id);
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

        public List<DeporteDeportista> List()
        {
            var list = new List<DeporteDeportista>();
            using (var db = new SirindarDbContext())
            {
                db.DeportesDeportistas
                    .Include(d => d.Deporte)
                    .Include(d => d.Deportista)
                    .WhereIsActive().OrderBy(dd => dd.Deportista.Matricula).ToList().ForEach(d =>
                    {
                        var deporteDeportista = new DeporteDeportista
                        {
                            DeporteDeportistaId = d.DeporteDeportistaId,
                            DeportistaId = d.DeportistaId,
                            DeporteId = d.DeporteId,
                            Deporte = new Deporte
                            {
                                DeporteId = d.Deporte.DeporteId,
                                Nombre = d.Deporte.Nombre
                            },
                            Deportista = new Deportista
                            {
                                DeportistaId = d.Deportista.DeportistaId,
                                Nombre = d.Deportista.Nombre,
                                Apellidos = d.Deportista.Apellidos,
                                Matricula = d.Deportista.Matricula
                            },
                            IniciaEntrenamiento = d.IniciaEntrenamiento,
                            FinalizaEntrenamiento = d.FinalizaEntrenamiento
                        };

                        list.Add(deporteDeportista);

                    }
                    );
            }
            return list;
        }


    }
}