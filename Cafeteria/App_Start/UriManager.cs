using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Globalization;

namespace Cafeteria.App_Start
{
    public static class UriManager
    {
        public static Uri Get(string appConfigKey)
        {
            var keyIndex = getKey(appConfigKey);
            if (keyIndex == -1)
                throw new UriManagerException("No se encontro '" + appConfigKey + "' en 'appSettings'");
            Uri uri;
            if (!Uri.TryCreate(getUri(keyIndex), UriKind.Absolute, out uri))
                throw new UriManagerException("Se encontro '" + appConfigKey + "' en 'appSettings' pero el value no es una Uri valida");
            return uri;
        }

        private static int getKey(string key)
        {
            var keyIndex = -1;
            foreach (var _key in ConfigurationManager.AppSettings)
            {
                keyIndex++;
                if (key.ToLower() == _key.ToString().ToLower())
                    return keyIndex;
            }
            return -1;
        }

        private static string getUri(int keyindex) 
        {
            return ConfigurationManager.AppSettings[keyindex];
        }
    }

    public class UriManagerException : ApplicationException
    {
        public UriManagerException(string message) : base(message) 
        {
        }
    }
}
