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
using ServiciosCafeteria;
using ServiciosCafeteria.Interfaces;
using ServiciosCafeteria.AppModels;
using System.Configuration;

namespace Cafeteria.Views
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private ISirindarApi api;

        public Login()
        {
            this.api = new ApiResolver(ConfigurationManager.AppSettings["ISirindarApi"]).Api;
            InitializeComponent();
            MainWindow.Exit = true;
            tbxUserName.Focus();
        }

        private async void login()
        {
            btnLogIn.IsEnabled = false;
            pbrLogin.Visibility = Visibility.Visible;

            try
            {
                var result = await api.LogIn(new LoginModel(tbxUserName.Text, tbxContraseña.Password));
                if (result)
                    NavigationService.Navigate(new Home());
                lblLoginFail.Content = "Usuario o contraseña incorrectos";
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                MessageBox.Show(ex.Message + ": " + ex.StackTrace);
                lblLoginFail.Content = "FUERA DE SERVICIO, Intenta mas tarde";
            }
            finally
            {
                btnLogIn.IsEnabled = true;
                pbrLogin.Visibility = Visibility.Hidden;
                lblLoginFail.Visibility = Visibility.Visible;
            }

        }

        public void GetLogin(object sender, RoutedEventArgs e)
        {
            login();
        }

        private void tbxContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                login();
        }

    }
}
