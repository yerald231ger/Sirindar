using System;
using ServiciosCafeteria.Interfaces;
using ServiciosCafeteria.Instancias;

namespace ServiciosCafeteria
{
    public class ApiResolver
    {
        private ISirindarApi _api;

        // ReSharper disable once ConvertToAutoProperty
        public ISirindarApi Api { get { return _api; } private set { _api = value; } }

        public ApiResolver(string typeName)
        {
            var resolvedType = Type.GetType(typeName);
            if(resolvedType == null)
                throw new ServiciosCafeteriaException("Referencia Nulla en ApiReolver");

            if (resolvedType.IsEquivalentTo(typeof(SirindarApi)))
                Api = SirindarApi.Instance;
            else if(resolvedType.IsEquivalentTo(typeof(SirindarApi2)))
                Api = SirindarApi.Instance;
        }
    }

    public class ImpresoraResolver 
    {
        private IImpresora _impresora;

        // ReSharper disable once ConvertToAutoProperty
        public IImpresora Impresora
        {
            get { return _impresora; }
            private set { _impresora = value; }
        }

        public ImpresoraResolver(string typeName) 
        {
            var resolvedType = Type.GetType(typeName);

            if (resolvedType == null)
                throw new ServiciosCafeteriaException("Referencia Nulla en ApiReolver");

            if (resolvedType.IsEquivalentTo(typeof(Impresora)))
                Impresora = new Impresora();
            else if(resolvedType.IsEquivalentTo(typeof(Impresora2)))
                Impresora = new Impresora2();
        }
    }
}
