using System;
using Sirindar.Core;
using Sirindar.Entity;

namespace UnitOfWorkTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var Unit = new UnitOfWork())
            {

                var d = Unit.Dependencias.GetAll();
                var depen = Unit.Dependencias.SingleOrDefault(dep => dep.DependenciaId == 2);
                Unit.Dependencias.Remove(depen);
                var dependencia = new Dependencia
                {
                    Clave = "16",
                    DependenciaId = 2,
                    EsActivo = true,
                    FechaAlta = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Nombre = "La Facu"
                };
                Unit.Complete();
            }
        }
    }
}
