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
using Cafeteria.Views;
using System.Timers;
using System.Globalization;
using System.Diagnostics;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using Cafeteria;
using Cafeteria.Cloak;

namespace Cafeteria
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly static Cloak.Reloj reloj = Cloak.Reloj.Instance;

        public MainWindow()
        {            
            InitializeComponent();
            reloj.Start();
            lblHora.Content = reloj.Hora;
            lblMinutos.Content = string.Format("{0:mm}", DateTime.Now);
            lblSegundos.Content = reloj.Segundo;

            reloj.EnCambiaHora += Reloj_EnCambiaHora;
            reloj.EnCambiaMinuto += Reloj_EnCambiaMinuto;
            reloj.EnCambiaSegundo += Reloj_EnCambiaSegundo;
        }

        private void Reloj_EnCambiaHora(object sender, Cloak.CambiaHoraEventArgs e)
        {           
            lblHora.Content = e.Hora;
        }

        private void Reloj_EnCambiaMinuto(object sender, Cloak.CambiaMinutoEventArgs e)
        {
            lblMinutos.Content = e.Minuto;
        }

        private void Reloj_EnCambiaSegundo(object sender, Cloak.CambiaSegundoEventArgs e)
        {
            lblSegundos.Content = e.Segundo;
        }

        private void Salir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void frmA_ContentRendered(object sender, EventArgs e)
        {
            frmA.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }
    }



}
