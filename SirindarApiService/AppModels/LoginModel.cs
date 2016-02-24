using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirindarApiService.AppModels
{
    public class LoginModel
    {

        public string grant_type { get { return "password"; } }
        public string username { get; set; }
        public string password { get; set; }

        public LoginModel(string username, string password) 
        {
            this.username = username;
            this.password = password;
        }       
       
    }
}
