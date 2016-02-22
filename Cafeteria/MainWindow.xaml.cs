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

namespace Cafeteria
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly static Reloj.Reloj Reloj = new Reloj.Reloj(autoStart: true);

        public MainWindow()
        {
            
            InitializeComponent();
            lblHora.Content = Reloj.Hora;
            lblMinutos.Content = string.Format("{0:mm}", DateTime.Now);
            lblSegundos.Content = Reloj.Segundo;

            Reloj.EnCambiaHora += Reloj_EnCambiaHora;
            Reloj.EnCambiaMinuto += Reloj_EnCambiaMinuto;
            Reloj.EnCambiaSegundo += Reloj_EnCambiaSegundo;
        }

        private void Reloj_EnCambiaHora(object sender, Reloj.CambiaHoraEventArgs e)
        {           
            lblHora.Content = e.Hora;
        }

        private void Reloj_EnCambiaMinuto(object sender, Reloj.CambiaMinutoEventArgs e)
        {
            lblMinutos.Content = e.Minuto;
        }

        private void Reloj_EnCambiaSegundo(object sender, Reloj.CambiaSegundoEventArgs e)
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
