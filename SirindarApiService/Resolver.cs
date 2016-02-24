using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SirindarApiService
{
    public class Resolver
    {
        private ISirindarApi api;

        public ISirindarApi Api { get { return api; } private set { api = value; } }

        public Resolver(string typeName) 
        {
            Type resolvedType = Type.GetType(typeName);

            if (resolvedType.IsEquivalentTo(typeof(SirindarApi)))
                Api = SirindarApi.Instance;
            else if (resolvedType.IsEquivalentTo(typeof(SirindarApi2)))
                Api = new SirindarApi2();
            else
                Api = null;
        }
    }
}
