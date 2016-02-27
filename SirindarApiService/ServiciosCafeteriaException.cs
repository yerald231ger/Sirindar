using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosCafeteria
{
    public class ServiciosCafeteriaException : ApplicationException
    {
        public ServiciosCafeteriaException(string message)
            : base(message)
        {
        }
    }

    public class ServiciosCafeteriaBadRequestException : ServiciosCafeteriaException
    {
        private HttpContent content;

        public HttpContent Content
        {
            get { return content; }
            set { content = value; }
        }

        public ServiciosCafeteriaBadRequestException(HttpContent content, string message)
            : base(message)
        {
            this.content = content;
        }
    }
}
