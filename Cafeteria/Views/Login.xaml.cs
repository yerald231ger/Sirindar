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
        private SirindarApi api;

        private Login(SirindarApi api)
        {
            this.api = api;
        }

        public Login() : this(SirindarApi.Instance)
        {
            InitializeComponent();
        }

        public async void GetLogin(object sender, RoutedEventArgs e)
        {
            pbrLogin.Visibility = Visibility.Visible;
            var result = await api.LogIn(new LoginModel(tbxUserName.Text, tbxContraseña.Text));
            if (result)
            {
                NavigationService.Navigate(new Home());
            }

            lblLoginFail.Visibility = Visibility.Visible;
            lblLoginFail.Content = "Usuario o contraseña incorrectos";
            pbrLogin.Visibility = Visibility.Hidden;
        }

    }
}
