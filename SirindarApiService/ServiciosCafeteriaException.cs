using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosCafeteria
{
    public class ServiciosCafeteriaException : ApplicationException
    {
        public ServiciosCafeteriaException(string message) : base(message) 
        {            
        }
    }
}
