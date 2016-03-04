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
using ServiciosCafeteria.AppModels;
using ServiciosCafeteria;
using Cafeteria.Cloak;
using CNSirindar.Models;
using System.Configuration;
using System.Windows.Media.Animation;
using ServiciosCafeteria.Interfaces;

namespace Cafeteria.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home
    {
        private Reloj reloj;
        private ISirindarApi api;
        private IImpresora impresora;
        private List<Horario> horarios;

        private Home(Reloj reloj)
        {
            api = new ApiResolver(ConfigurationManager.AppSettings["ISirindarApi"]).Api;
            impresora = new ImpresoraResolver(ConfigurationManager.AppSettings["IImpresora"]).Impresora;
            this.reloj = reloj;
            MainWindow.Exit = false;
        }

        public Home()
            : this(Reloj.Instance)
        {
            InitializeComponent();
            reloj.EnCambiaHorario += reloj_EnCambiaComida;
            var task = api.Horarios();

            task.ContinueWith(t =>
            {
                switch (t.Status)
                {
                    case TaskStatus.Faulted:
                        SetCafeteriaExcpetion(t.Exception.InnerException);
                        stkpHorarios.DataContext = null;
                        break;
                    case TaskStatus.RanToCompletion:
                        horarios = t.Result.ToList();
                        reloj.RegisterHorarios(horarios);
                        stkpHorarios.DataContext = horarios;
                        lbxHorarios.SelectedIndex = -1;
                        break;
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        void reloj_EnCambiaComida(object sender, CambiaHorarioEventArgs e)
        {
            lbxHorarios.SelectedIndex = horarios.FindIndex(h => h.Nombre == e.Horario);

            if (e.Horario == ComidasDia.Ninguna)
            {
                txbScanner.IsEnabled = false;
                txbSinHorario.Visibility = Visibility.Visible;
                rectErrores.Visibility = Visibility.Visible;
            }
            else
            {
                txbScanner.IsEnabled = true;
                txbScanner.Focus();
                txbSinHorario.Visibility = Visibility.Collapsed;
                rectErrores.Visibility = Visibility.Collapsed;
            }
        }

        private async void txbScanner_KeyDown(object sender, KeyEventArgs e)
        {
            int matricula;
            if(int.TryParse(txbScanner.Text,out matricula))
            {
                if (e.Key == Key.Enter)
                {
                    txbScanner.IsEnabled = false;
                    try
                    {
                        var deportista = await api.GetDeportista(matricula);

                        if (deportista != null)
                        {
                            SetTextLblDeportista(deportista.Nombre, deportista.Dependencia.Nombre);
                            var dateNow = DateTime.Now;
                            var result = await api.RegistrarAsistencia(new Asistencia
                            {
                                DeportistaId = deportista.DeportistaId,
                                HorarioId = horarios.First(h => h.Nombre == Reloj.Instance.Horario).HorarioId,
                                HoraAsistencia = dateNow
                            });
                            if (result.Aceptado)
                            {
                                SetTextLblDeportista("...", "...");
                                impresora.Imprimir(new Ticket 
                                {
                                   Comida = reloj.Horario.ToString(),
                                   Dependencia = deportista.Dependencia.Nombre,
                                   Deportista = deportista.Nombre,
                                   Deporte = "Futboll",
                                   Fecha = dateNow,
                                   GramosProteina = 230,
                                   GramosCarboHidratos = 200,
                                   GrupoRaciones = "Sin Grupo y Alimentacion",
                                   GramosLipidos = 500,
                                   KiloCalorias = 1000
                                }
                            );
                                txbScanner.IsEnabled = true;
                                txbScanner.Focus();
                            }
                            else
                            {
                                await SetErrorAsistencia(result.Razon);
                            }
                        }
                        else
                        {
                            await SetErrorAsistencia("Deportista no encontrado");
                        }
                    }
                    catch (ServiciosCafeteriaException ex)
                    {
                        SetCafeteriaExcpetion(ex);
                    }

                }                
            }            
        }

        private void SetCafeteriaExcpetion(Exception ex)
        {
            MessageBox.Show(ex.Message + ": " + ex.StackTrace);
            txbErrorAsistencia.Text = "FUERA DE SERVICIO";
            rectErrores.Visibility = Visibility.Visible;
            txbErrorAsistencia.Visibility = Visibility.Visible;        
        }

        private async Task SetErrorAsistencia(string texto)
        {
            txbErrorAsistencia.Text = texto;
            ShowErrorAsistencia(Visibility.Visible);
            await Task.Delay(1000);
            ShowErrorAsistencia(Visibility.Collapsed);
            txbScanner.IsEnabled = true;
            txbScanner.Focus();
        }

        private void ShowErrorAsistencia(Visibility valor)
        {
            rectErrores.Visibility = valor;
            txbErrorAsistencia.Visibility = valor;
        }

        private void SetTextLblDeportista(string nombre, string dependencia)
        {
            lblNombre.Content = nombre;
            lblDependencia.Content = dependencia;
        }
    }
}
