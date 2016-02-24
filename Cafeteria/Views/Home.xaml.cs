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
using SirindarApiService.AppModels;
using SirindarApiService;
using Cafeteria.Cloak;
using CNSirindar.Models;

namespace Cafeteria.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private Reloj reloj;
        private SirindarApi api;
        private readonly List<Horario> horarios;

        public Home(SirindarApi api, Reloj reloj)
        {
            this.api = api;
            this.reloj = reloj;
        }

        public Home() : this(SirindarApi.Instance, Reloj.Instance)
        {
            InitializeComponent();
            txbScanner.Focus();
            stkpHorarios.DataContext = horarios;
            reloj.EnCambiaMinuto += reloj_EnCambiaMinuto;
        }

        void reloj_EnCambiaMinuto(object sender, CambiaMinutoEventArgs e)
        {
            
        }
    }
}
