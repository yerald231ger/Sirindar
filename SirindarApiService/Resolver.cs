using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ServiciosCafeteria.Interfaces;
using ServiciosCafeteria.Instancias;

namespace ServiciosCafeteria
{
    public class ApiResolver
    {
        private ISirindarApi api;

        public ISirindarApi Api { get { return api; } private set { api = value; } }

        public ApiResolver(string typeName)
        {
            Type resolvedType = Type.GetType(typeName);

            if (resolvedType.IsEquivalentTo(typeof(SirindarApi)))
                Api = SirindarApi.Instance;
            else
                Api = null;
        }
    }

    public class ImpresoraResolver 
    {
        private IImpresora impresora;

        public IImpresora Impresora
        {
            get { return impresora; }
            private set { impresora = value; }
        }

        public ImpresoraResolver(string typeName) 
        {
            Type resolvedType = Type.GetType(typeName);

            if (resolvedType.IsEquivalentTo(typeof(Impresora)))
                Impresora = new Impresora();
            else
                Impresora = null;
        }
    }
}
