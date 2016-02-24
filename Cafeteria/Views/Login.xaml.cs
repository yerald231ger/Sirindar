using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using Cafeteria;
using Cafeteria.App_Start;
using Cafeteria.AppModels;

namespace Cafeteria.Views
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Page
    {
      

        public Login() 
        {
            InitializeComponent();
        }

        public void GetLogin(object sender, RoutedEventArgs e)
        {            
            pbrLogin.Visibility = Visibility.Visible;
            var d  = App_Start.SirindarApi.Client;
            pbrLogin.Visibility = Visibility.Hidden;
        }

    }
}
