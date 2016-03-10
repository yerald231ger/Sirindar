using System;
using System.Net.Http;

namespace ServiciosCafeteria
{
    public class ServiciosCafeteriaException : ApplicationException
    {
        public ServiciosCafeteriaException(string message)
            : base(message)
        {
        }
    }

    public class ServiciosCafeteriaInternalServerErrorException : ServiciosCafeteriaException
    {
        public HttpContent Content { get; set; }

        public ServiciosCafeteriaInternalServerErrorException(HttpContent content, string message)
            : base(message)
        {
            Content = content;
        }
    }

    public class ServiciosCafeteriaBadRequestException : ServiciosCafeteriaException
    {
        public HttpContent Content { get; set; }

        public ServiciosCafeteriaBadRequestException(HttpContent content, string message)
            : base(message)
        {
            Content = content;
        }
    }
}
