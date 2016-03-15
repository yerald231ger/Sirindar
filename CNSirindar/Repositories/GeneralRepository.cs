using CNSirindar.Models;
using CNSirindar.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace CNSirindar.Repositories
{
    public class GeneralRepository
    {
        public static bool IsDependencia(int id)
        {
            using (var db = new SirindarDbContext())
            {
                if (db.Dependencias.Find(id) != null)
                    return true;
                else
                    return false;
            }
        }

        public static bool IsDeportista(int id)
        {
            using (var db = new SirindarDbContext())
            {
                if (db.Deportistas.Find(id) != null)
                    return true;
                else
                    return false;
            }
        }

        public static bool IsDeporte(int id)
        {
            using (var db = new SirindarDbContext())
            {
                if (db.Deportes.Find(id) != null)
                    return true;
                else
                    return false;
            }
        }

        public static bool IsBloque(int id)
        {
            using (var db = new SirindarDbContext())
            {
                if (db.Bloques.Find(id) != null)
                    return true;
                else
                    return false;
            }
        }

        public static bool IsMatricula(string matricula)
        {
            using (var db = new SirindarDbContext())
            {
                return (db.Deportistas.FirstOrDefault(d => d.Matricula == matricula) != null) ? true : false;
            }
        }

        public static ICollection<Deporte> GetDeportes(int id)
        {
            using (var db = new SirindarDbContext())
            {
                return db.DeportesDeportistas
                    .Include(d => d.Deporte)
                    .WhereIsActive(d => d.DeportistaId == id)
                    .Select(dd => dd.Deporte)
                    .ToList()
                    .Select(d => new Deporte
                    {
                        DeporteId = d.DeporteId,
                        TipoEnergia = d.TipoEnergia,
                        Nombre = d.Nombre,
                        Clasificacion = new ClasificacionDeporte
                        {
                            ClasificacionDeporteId = d.Clasificacion.ClasificacionDeporteId,
                            Abreviatura = d.Clasificacion.Abreviatura,
                            Descripcion = d.Clasificacion.Descripcion
                        }
                    }).ToList();
            }
        }

        public static ICollection<Deportista> SerchDeportistasByNombre(string nombre)
        {
            using (var db = new SirindarDbContext())
            {
                return db.Deportistas
                    .WhereIsActive
                    (
                        d =>
                            (
                                d.Nombre.Contains(nombre) ||
                                d.Apellidos.Contains(nombre)
                            )
                            &&
                            (
                                d.Status == Status.Alta
                            )
                     )
                     .OrderBy(d => d.Nombre)
                     .ToList();
            }
        }

        public static ICollection<dynamic> SearchDeportistasByMatricula(string matricula)
        {
            using (var db = new SirindarDbContext())
            {
                return db.Deportistas
                    .WhereIsActive(d => d.Matricula
                        .ToUpperInvariant()
                        .Contains(matricula.ToLowerInvariant())
                        && d.Status == Status.Alta)
                    .OrderBy(d => d.Nombre)
                    .Select(d => new { d.DeportistaId, d.Matricula, d.Nombre, d.CantidadComidas.Cantidad })
                    .ToList<dynamic>();
            }
        }

        public static ICollection<dynamic> SearchDeportistasByStringExpression(string expression)
        {
            using (var db = new SirindarDbContext())
            {
                expression = expression.ToLowerInvariant();
                return db.Deportistas
                    .WhereIsActive(d =>
                        d.ToString().ToLowerInvariant().Contains(expression) ||
                        d.Genero.ToString().ToLowerInvariant().Contains(expression)
                        && d.Status == Status.Alta)
                    .OrderBy(d => d.Nombre)
                    .Select(d => new { d.DeportistaId, d.Matricula, d.Nombre, d.CantidadComidas.Cantidad })
                    .ToList<dynamic>();
            }
        }

        public static ICollection<dynamic> SearchDeportistasByDeporteName(string deporte)
        {
            using (var db = new SirindarDbContext())
            {
                return db.Deportes
                    .WhereIsActive(d => d.Nombre
                        .ToLowerInvariant()
                        .Contains(deporte.ToLowerInvariant()))
                    .SelectMany(d => d.DeportesDeportistas, (dp, c) => new { c.DeportistaId })
                    .Join(db.Deportistas, x => x.DeportistaId, y => y.DeportistaId,
                        (i, j) => new { j.DeportistaId, j.Matricula, j.Nombre, j.CantidadComidas.Cantidad }).ToList<dynamic>();
            }
        }

        public static object SearchDeportistasByDeporteId(int deporteId)
        {
            using (var db = new SirindarDbContext())
            {
                return db.Deportes
                   .WhereIsActive(d => d.DeporteId == deporteId)
                   .SelectMany(d => d.DeportesDeportistas, (dp, c) => new { c.DeportistaId })
                   .Join(db.Deportistas, x => x.DeportistaId, y => y.DeportistaId,
                       (i, j) => new { j.DeportistaId, j.Matricula, j.Nombre, j.CantidadComidas.Cantidad }).ToList<dynamic>();
            }
        }

        public static ICollection<Deportista> SearchDeportistasByDependencia(int dependenciaId)
        {
            using (var db = new SirindarDbContext())
            {
                return db.Deportistas.WhereIsActive
                    (
                        d =>
                            d.DependenciaId == dependenciaId && d.Status == Status.Alta
                    )
                    .OrderBy(d => d.Nombre)
                    .ToList();
            }
        }

        public static ICollection<Deportista> SearchDeportistasByDeporte(int deporteId)
        {
            using (var db = new SirindarDbContext())
            {
                return db.Deportistas.WhereIsActive
                    (
                        d =>
                            d.DeportistaId == deporteId && d.Status == Status.Alta
                    )
                    .OrderBy(d => d.Nombre).ToList();
            }
        }

        public static ICollection<Deportista> SearchDeportistasByDates(DateTime desde, DateTime hasta)
        {
            using (var db = new SirindarDbContext())
            {
                return db.Deportistas.WhereIsActive
                    (
                        d =>
                            d.FechaAlta >= desde
                            &&
                            d.FechaAlta <= hasta
                            &&
                            d.Status == Status.Alta
                    )
                    .OrderBy(d => d.Nombre).ToList();
            }
        }

        public static string GetFullName(int deportistaId)
        {
            using (var db = new SirindarDbContext())
            {
                return db.Deportistas.Where(d => d.DeportistaId == deportistaId).Select(d => d.Nombre + " " + d.Apellidos).First();
            }
        }

        public static ICollection<dynamic> SearchDeportistasByDepenenciaNombre(string dependenciaNombre)
        {
            using (var db = new SirindarDbContext())
            {
                return db.Dependencias
                    .WhereIsActive(d => d.Nombre.ToLowerInvariant().Contains(dependenciaNombre.ToLowerInvariant()))
                    .SelectMany(dpo => dpo.Deportistas, (x, y) => new { y.DeportistaId, y.Matricula, y.Nombre, y.CantidadComidas.Cantidad }).ToList<dynamic>();
            }
        }

        public static DeporteDeportista FindByDeporteId(int deporteId)
        {
            using (var db = new SirindarDbContext())
            {
                var deporteDeportista = db.DeportesDeportistas.Include(dd => dd.Deporte).WhereIsActive(dd => dd.DeporteId == deporteId).First();
                return deporteDeportista;
            }
        }

        public static ICollection<dynamic> SearchDeportesByName(string deporte)
        {
            using (var db = new SirindarDbContext())
            {
                return db.Deportes
                    .WhereIsActive(d => d.Nombre
                        .ToLowerInvariant()
                        .Contains(deporte.ToLowerInvariant()))
                    .Select(d => new { d.DeporteId, d.Nombre })
                    .ToList<dynamic>();
            }
        }

        public static object SearchDependenciasByName(string facultad)
        {
            using (var db = new SirindarDbContext())
            {
                return db.Dependencias
                    .WhereIsActive(d => d.Nombre
                        .ToLowerInvariant()
                        .Contains(facultad.ToLowerInvariant()))
                    .OrderBy(d => d.Nombre)
                    .Select(d => new { d.DependenciaId, d.Nombre })
                    .ToList();
            }
        }

        public static void SumaKilocalorias(int bloqueId)
        {
            using (var db = new SirindarDbContext())
            {
                var bloque = db.Bloques.Find(bloqueId);
                bloque.KilocaloriasTotales = 0;
                bloque.Grupos.Where(g => g.EsActivo).ToList().ForEach(g => bloque.KilocaloriasTotales += g.Kilocalorias);
                db.SaveChanges();
            }
        }

        public static bool IsDeporteDeportista(AsignacionBloque deporteDeportista)
        {
            using (var db = new SirindarDbContext())
            {
                if (db.AsigancionesBloques
                    .FirstOrDefaultIsActive(dd =>
                        dd.DeporteId == deporteDeportista.DeporteId
                        &&
                        dd.DeportistaId == deporteDeportista.DeportistaId
                        &&
                        dd.BloqueId == deporteDeportista.BloqueId) == null)
                    return false;
                else
                    return true;
            }
        }

        public static int CantidadComidas(int deportistaId)
        {
            using (var db = new SirindarDbContext())
            {
                return (int)db.CantidadComidas.FirstIsActive(c => c.DeportistaId == deportistaId).Cantidad;
            }
        }

        public static CantidadComidas LoadHorarioDeportista(int deportistaId) 
        {
            using (var db = new SirindarDbContext()) 
            {
                return db.CantidadComidas.FirstIsActive(c => c.DeportistaId == deportistaId);
            }
        }

        public static bool UpdateCantidadComidasByDeporte(int deporteId, CantidadComidas cantidadComidas)
        {
            using (var db = new SirindarDbContext())
            {
                db.Deportes
                  .WhereIsActive(d => d.DeporteId == deporteId)
                  .SelectMany(d => d.DeportesDeportistas, (dp, c) => new { c.DeportistaId })
                  .Join(db.Deportistas, x => x.DeportistaId, y => y.DeportistaId,
                      (i, j) => new { j.DeportistaId }).ToList()
                  .ForEach(dpo =>
                  {
                      var deportista = db.Deportistas.Find(dpo.DeportistaId);
                      deportista.CantidadComidas.FechaModificacion = DateTime.Now;
                      deportista.CantidadComidas.Cantidad = cantidadComidas.Cantidad;
                      deportista.CantidadComidas.Lunes = cantidadComidas.Lunes;
                      deportista.CantidadComidas.Martes = cantidadComidas.Martes;
                      deportista.CantidadComidas.Miercoles = cantidadComidas.Miercoles;
                      deportista.CantidadComidas.Jueves = cantidadComidas.Jueves;
                      deportista.CantidadComidas.Viernes = cantidadComidas.Viernes;
                      deportista.CantidadComidas.Sabado = cantidadComidas.Sabado;
                      deportista.CantidadComidas.Domingo = cantidadComidas.Domingo;
                  });
                db.SaveChanges();
                return true;
            }
        }

        public static bool UpdateCantidadComidasByDependencia(int dependenciaId, CantidadComidas cantidadComidas)
        {
            using (var db = new SirindarDbContext())
            {
                db.Dependencias.Find(dependenciaId).Deportistas
                    .ToList()
                    .ForEach(dep =>
                    {
                        dep.CantidadComidas.FechaModificacion = DateTime.Now;
                        dep.CantidadComidas.Cantidad = cantidadComidas.Cantidad;
                        dep.CantidadComidas.Lunes = cantidadComidas.Lunes;
                        dep.CantidadComidas.Martes = cantidadComidas.Martes;
                        dep.CantidadComidas.Miercoles = cantidadComidas.Miercoles;
                        dep.CantidadComidas.Jueves = cantidadComidas.Jueves;
                        dep.CantidadComidas.Viernes = cantidadComidas.Viernes;
                        dep.CantidadComidas.Sabado = cantidadComidas.Sabado;
                        dep.CantidadComidas.Domingo = cantidadComidas.Domingo;
                    });
                db.SaveChanges();
                return true;
            }
        }

        public static bool UpdateCantidadComidas(CantidadComidas entity)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity.FechaModificacion = DateTime.Now;
                    db.CantidadComidas.Attach(entity);
                    db.Entry(entity).Property(p => p.FechaModificacion).IsModified = true;
                    db.Entry(entity).Property(p => p.Cantidad).IsModified = true;
                    db.Entry(entity).Property(p => p.Lunes).IsModified = true;
                    db.Entry(entity).Property(p => p.Martes).IsModified = true;
                    db.Entry(entity).Property(p => p.Miercoles).IsModified = true;
                    db.Entry(entity).Property(p => p.Jueves).IsModified = true;
                    db.Entry(entity).Property(p => p.Viernes).IsModified = true;
                    db.Entry(entity).Property(p => p.Sabado).IsModified = true;
                    db.Entry(entity).Property(p => p.Domingo).IsModified = true;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static TableAsignacionBloques GridAsignaciones()
        {
            using (var db = new SirindarDbContext())
            {
                var groupDeportistas = db.AsigancionesBloques
                        .Include(d => d.Deporte)
                        .Include(d => d.Deportista)
                        .Include(d => d.Bloque)
                        .WhereIsActive().GroupBy(dd => dd.Deportista.Matricula).ToList();

                var tableAsignaciones = new TableAsignacionBloques();
                var listRowDeportistas = new List<RowDeportista>();
                var listRowDeportes = new List<RowDeporte>();
                var listRowBloques = new List<RowBloque>();

                tableAsignaciones.Deportistas = new List<RowDeportista>();

                foreach (var deportista in groupDeportistas)
                {
                    var key = deportista.Key;
                    var rowDeportista = deportista
                        .Where(x => x.Deportista.Matricula == key)
                        .Select(x => new RowDeportista
                        {
                            Matricula = x.Deportista.Matricula,
                            Nombre = x.Deportista.Nombre,
                            DeportistaId = x.DeportistaId
                        }).First();

                    rowDeportista.Deportes = new List<RowDeporte>();

                    var groupDeporte = deportista.Where(x => x.Deportista.Matricula == key).GroupBy(x => x.DeporteId).ToList();

                    var listIds = new List<int>();
                    foreach (var deporte in groupDeporte)
                    {
                        var key2 = deporte.Key;
                        var rowDeporte = deporte
                            .Where(x => x.DeporteId == key2)
                            .Select(x => new RowDeporte
                            {
                                DeporteId = x.DeporteId,
                                Nombre = x.Deporte.Nombre
                            }).First();

                        var listRowBloque = deporte
                            .Where(dd => dd.DeporteId == key2)
                            .Select(dd =>
                            {
                                if (dd.Bloque != null)
                                {
                                    return new RowBloque
                                    {
                                        Nombre = dd.Bloque.Nombre,
                                        BloqueId = dd.BloqueId.Value,
                                        DeporteDeportistaId = dd.AsignacionBloqueId
                                    };
                                }
                                else
                                {
                                    return new RowBloque
                                    {
                                        Nombre = "Sin bloque",
                                        DeporteDeportistaId = dd.AsignacionBloqueId
                                    };
                                }
                            }).ToList();

                        rowDeporte.Bloques = listRowBloque;
                        rowDeporte.RowSpan = listRowBloque.Select(lrb => lrb.DeporteDeportistaId).ToArray();
                        listIds.AddRange(listRowBloque.Select(lrb => lrb.DeporteDeportistaId).ToArray());
                        rowDeportista.Deportes.Add(rowDeporte);
                    }
                    rowDeportista.RowSpan = listIds.ToArray();
                    tableAsignaciones.Deportistas.Add(rowDeportista);
                }

                return tableAsignaciones;
            }
        }
    }

    public class TableAsignacionBloques
    {
        public List<RowDeportista> Deportistas { get; set; }
    }

    public class RowDeportista
    {
        public int[] RowSpan { get; set; }
        public int DeportistaId { get; set; }
        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public List<RowDeporte> Deportes { get; set; }
    }

    public class RowDeporte
    {
        public int[] RowSpan { get; set; }
        public int DeporteId { get; set; }
        public string Nombre { get; set; }
        public List<RowBloque> Bloques { get; set; }
    }

    public class RowBloque
    {
        public int BloqueId { get; set; }
        public int DeporteDeportistaId { get; set; }
        public string Nombre { get; set; }
    }
}

