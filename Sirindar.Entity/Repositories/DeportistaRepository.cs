using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Entity.Repositories
{
    public class DeportistaRepository : Repository<Deportista>, IDeportistaRepository
    {
        private SirindarDbContext SirindarDbContext
        {
            get { return Context as SirindarDbContext; }
        }

        public DeportistaRepository(DbContext context) : base(context) { }

        public Deportista GetByMatricula(string matricula)
        {
            return SirindarDbContext.Deportistas.FirstOrDefault(d => d.Matricula == matricula && d.EsActivo);
        }

        public HorarioComidas GetHorarioComidas(int deportistaId)
        {
            return SirindarDbContext.Deportistas.Find(deportistaId).HorarioComidas;
        }

        public Deportista GetWithDeportes(int deportistaId)
        {
            var deportista = Get(deportistaId);
            deportista.Deportes = SirindarDbContext.DeportesDeportistas.Include(d => d.Deporte)
                .Where(d => d.DeportistaId == deportistaId && d.EsActivo)
                .Select(dd => dd.Deporte).ToList()
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
            return deportista;
        }

        public IEnumerable<Deporte> GetDeportes(string matricula)
        {
            var dictionary = new Dictionary<string, string>
            {
                {"@Matricula", matricula}
            };
            return SirindarDbContext.EjecutaSp<Deporte>("SPLDeportistaDeportes", dictionary).ToList();
        }

        public IEnumerable<Deportista> GetAllByExpression(string expression)
        {
            var dictionary = new Dictionary<string, string>
            {
                {"@Expression", expression}
            };
            return SirindarDbContext.EjecutaSp<Deportista>("SPLDeportistas", dictionary).ToList();
        }

        public IEnumerable<Deportista> SearchByMatricula(string matricula)
        {
            return Find(d => d
                .Matricula
                .ToUpperInvariant()
                .Contains(matricula.ToUpperInvariant())
                             && d.Status == Status.Alta)
                .OrderBy(d => d.Nombre).ToList();
        }

        public IEnumerable<Deportista> SearchByStringExpression(string expression)
        {
            expression = expression.ToLowerInvariant();
            return Find(d =>
                d.ToString().ToLowerInvariant().Contains(expression) ||
                d.Genero.ToString().ToLowerInvariant().Contains(expression) &&
                d.Status == Status.Alta)
                .OrderBy(d => d.Nombre).ToList();
        }

        public IEnumerable<Deportista> SearchByDeporte(string deporte)
        {
            return SirindarDbContext.Deportes.Where(d =>
                   d.Nombre.ToLowerInvariant().Contains(deporte.ToLowerInvariant()) && d.EsActivo)
                   .SelectMany(d => d.DeportesDeportistas, (dp, c) => new { c.DeportistaId })
                   .Join(GetAll(), x => x.DeportistaId, y => y.DeportistaId,
                            (i, j) => new Deportista
                            {
                                DeportistaId = j.DeportistaId,
                                Matricula = j.Matricula,
                                HorarioComidas = new HorarioComidas { Cantidad = j.HorarioComidas.Cantidad }
                            }).ToList();
        }

        public IEnumerable<Deportista> SearchByDependencia(string deporte)
        {
            return SirindarDbContext.Dependencias
                .Where(d => d.EsActivo)
                .Where(d => d.Nombre.ToLowerInvariant().Contains(deporte.ToLowerInvariant()))
                    .SelectMany(dpo => dpo.Deportistas, (x, y) =>
                    new Deportista
                    {
                        DeportistaId = y.DeportistaId,
                        Matricula = y.Matricula,
                        Nombre = y.Nombre,
                        HorarioComidas =
                       new HorarioComidas
                       {
                           Cantidad = y.HorarioComidas.Cantidad
                       }
                    }).ToList();
        }

        public void AddDeporte(int deportistaId, int deporteId, TimeSpan iniciaEntrenamiento,
            TimeSpan finalizaEntrenamiento)
        {
            SirindarDbContext.DeportesDeportistas.Add(new DeporteDeportista
            {
                DeporteId = deporteId,
                DeportistaId = deportistaId,
                IniciaEntrenamiento = iniciaEntrenamiento,
                FinalizaEntrenamiento = finalizaEntrenamiento
            });
        }

        public void RemoveDeporte(int deportistaId, int deporteId)
        {
            var deporteDeportista = SirindarDbContext.DeportesDeportistas.FirstOrDefault(
                dd => dd.DeportistaId == deportistaId && dd.DeporteId == deporteId).EsActivo = false;
        }

        public bool IsMatricula(string matricula)
        {
            return SingleOrDefault(d => d.Matricula == matricula) != null;
        }

        public bool IsDeportista(int deportistaId)
        {
            return SingleOrDefault(d => d.DeportistaId == deportistaId) != null;
        }
    }
}