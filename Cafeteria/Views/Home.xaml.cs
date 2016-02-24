﻿using System;
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
using System.Configuration;
using System;

namespace Cafeteria.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private Reloj reloj;
        private ISirindarApi api;
        private readonly List<Horario> horarios;

        private Home(Reloj reloj)
        {
            this.api = new Resolver(ConfigurationManager.AppSettings["ISirindarApi"]).Api;
            this.reloj = reloj;
        }

        public Home() : this(Reloj.Instance)
        {
            InitializeComponent();
            txbScanner.Focus();
            reloj.EnCambiaMinuto += reloj_EnCambiaMinuto;
            var task = api.Horarios();

            task.ContinueWith(t =>
            {
                switch (t.Status)
                {
                    case TaskStatus.Faulted:
                        stkpHorarios.DataContext = null;
                        break;
                    case TaskStatus.RanToCompletion:
                        stkpHorarios.DataContext = t.Result;
                        break;
                    default:
                        break;
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        void reloj_EnCambiaMinuto(object sender, CambiaMinutoEventArgs e)
        {
            
        }
    }
}
