using System.Collections.Generic;

namespace Sirindar.Entity.Migrations
{
    using Core;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SirindarDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SirindarDbContext context)
        {
            var listDependencias = new List<Dependencia>
            {
                new Dependencia { DependenciaId = 1, Clave = "1", Nombre = "Fac. de Agronomía", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 2, Clave = "2", Nombre = "Fac. de Arquitectura", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 3, Clave = "3", Nombre = "Fac. de Artes Escénicas", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 4, Clave = "4", Nombre = "Fac. de Artes Visuales", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 5, Clave = "5", Nombre = "Fac. de C. de la Comunicación", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 6, Clave = "6", Nombre = "Fac. de C. Físico-Matemáticas", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 7, Clave = "7", Nombre = "Fac. de C. Físico-Matemáticas", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 8, Clave = "8", Nombre = "Fac. de C. Políticas y Admón. Pública", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 9, Clave = "9", Nombre = "Fac. de Ciencias Biológicas", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 10, Clave = "10", Nombre = "Fac. de Ciencias de la Tierra", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 11, Clave = "11", Nombre = "Fac. de Ciencias Forestales", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 12, Clave = "12", Nombre = "Fac. de Ciencias Químicas", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 13, Clave = "13", Nombre = "Fac. de Contaduría Pública y Admón", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 14, Clave = "14", Nombre = "Fac. de Derecho y Criminología", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 15, Clave = "15", Nombre = "Fac. de Economía", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 16, Clave = "16", Nombre = "Fac. de Enfermería", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 17, Clave = "17", Nombre = "Fac. de Filosofía y Letras", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 18, Clave = "18", Nombre = "Fac. de Ingeniería Civil", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 19, Clave = "19", Nombre = "Fac. de Ingeniería Mecánica y Eléc", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 20, Clave = "20", Nombre = "Fac. de Medicina", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 21, Clave = "21", Nombre = "Fac. de Medicina Veterinaria y Zootecnia", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 22, Clave = "22", Nombre = "Fac. de Música", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 23, Clave = "23", Nombre = "Fac. de Odontología", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 24, Clave = "24", Nombre = "Fac. de Organización Deportiva", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 25, Clave = "25", Nombre = "Fac. de Psicología", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 26, Clave = "26", Nombre = "Fac. de Salud Pública y Nutrición", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Dependencia { DependenciaId = 27, Clave = "27", Nombre = "Fac. de Trabajo Social y Desarrollo Humano", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  }
            };

            listDependencias.ForEach(i => context.Dependencias.AddOrUpdate(d => d.DependenciaId, i));

            var listClasificaciones = new List<ClasificacionDeporte>
            {
                new ClasificacionDeporte { ClasificacionDeporteId = 1, Descripcion = "Arte Competitivo", Abreviatura = "AC", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new ClasificacionDeporte { ClasificacionDeporteId = 2, Descripcion = "Fuerza Rápida", Abreviatura = "FR", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new ClasificacionDeporte { ClasificacionDeporteId = 3, Descripcion = "Resistencia", Abreviatura = "R", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new ClasificacionDeporte { ClasificacionDeporteId = 4, Descripcion = "Juego de Pelota", Abreviatura = "JP", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new ClasificacionDeporte { ClasificacionDeporteId = 5, Descripcion = "Combate", Abreviatura = "C", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  }
            };

            listClasificaciones.ForEach(i => context.ClasificacionesDeportes.AddOrUpdate(c => c.ClasificacionDeporteId, i));

            var listBloques = new List<Bloque>
            {
                new Bloque{ BloqueId = 1, Nombre = "Bloque A", EsActivo = true, KilocaloriasTotales = 350, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Bloque{ BloqueId = 2, Nombre = "Bloque B", EsActivo = true, KilocaloriasTotales = 450, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Bloque{ BloqueId = 3, Nombre = "Bloque C", EsActivo = true, KilocaloriasTotales = 550, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new Bloque{ BloqueId = 4, Nombre = "Bloque D", EsActivo = true, KilocaloriasTotales = 150, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  }
            };

            listBloques.ForEach(i => context.Bloques.AddOrUpdate(b => b.BloqueId, i));

            var listGruposAlimenticios = new List<GrupoAlimenticio>
            {
                new GrupoAlimenticio { GrupoAlimenticioId = 1, Grupo = "Verduras", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new GrupoAlimenticio { GrupoAlimenticioId = 2, Grupo = "Frutas", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new GrupoAlimenticio { GrupoAlimenticioId = 3, Grupo = "Cereales y Tuberculos", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new GrupoAlimenticio { GrupoAlimenticioId = 4, Grupo = "Leguminosas", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new GrupoAlimenticio { GrupoAlimenticioId = 5, Grupo = "Alimentos de origen animal", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new GrupoAlimenticio { GrupoAlimenticioId = 6, Grupo = "Leche", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new GrupoAlimenticio { GrupoAlimenticioId = 7, Grupo = "Aceites y grasas", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new GrupoAlimenticio { GrupoAlimenticioId = 8, Grupo = "Azúcares", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new GrupoAlimenticio { GrupoAlimenticioId = 9, Grupo = "Líquidos", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new GrupoAlimenticio { GrupoAlimenticioId = 10, Grupo = "Condimentos", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  },
                new GrupoAlimenticio { GrupoAlimenticioId = 11, Grupo = "Bebidas alcoholicas", EsActivo = true, FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now  }
            };

            listGruposAlimenticios.ForEach(i => context.GruposAlimenticios.AddOrUpdate(ga => ga.GrupoAlimenticioId, i));

            var listHorarios = new List<Horario>
            {
                new Horario { HorarioId = 1, Nombre = ComidasDia.Desayuno, Inicia = new TimeSpan(6,0,0), Finaliza = new TimeSpan(11,0,0),FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now , EsActivo = true  },
                new Horario { HorarioId = 2, Nombre = ComidasDia.Comida, Inicia = new TimeSpan(1,0,0), Finaliza = new TimeSpan(15,0,0), FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now , EsActivo = true  },
                new Horario { HorarioId = 3, Nombre = ComidasDia.Cena, Inicia = new TimeSpan(17,0,0), Finaliza = new TimeSpan(21,0,0), FechaAlta = DateTime.Now, FechaModificacion = DateTime.Now , EsActivo = true  }
            };

            listHorarios.ForEach(i => context.Horarios.AddOrUpdate(h => h.HorarioId, i));

            //var listDeportistas = new List<Deportista>
            //{
            //    new Deportista 
            //    { 
            //        Matricula = "23746594",
            //        Nombre = "Juan",
            //        Apellidos = "Martinez Martinez",
            //        DependenciaId = 1,
            //        Depo
            //        FechaAlta = DateTime.Now,
            //        FechaNacimiento = DateTime.Now,
            //        EsActivo = true,
            //        FechaModificacion = DateTime.Now,
            //        FechaRegistro = DateTime.Now,
            //        Genero = Generos.Hombre,
            //        Status = Status.Alta,
            //        CantidadComidas = new CantidadComidas{ Cantidad = 2}
            //    },
            //};

            //listDeportistas.ForEach(i => context.Deportistas.AddOrUpdate(d => d.DeportistaId, i));
            context.SaveChanges();

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
