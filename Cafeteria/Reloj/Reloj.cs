using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;
using CNSirindar.Models;

namespace Cafeteria.Cloak
{
    public class Reloj
    {
        private int hora;
        private int minuto;
        private int segundo;
        private ComidasDia horario;
        private List<Horario> horarios;

        public int Hora
        {
            get
            {
                return hora;
            }

            private set
            {
                if (EnCambiaHora != null)
                    EnCambiaHora(this, new CambiaHoraEventArgs(value.ToString()));
                hora = value;
            }
        }

        public int Minuto
        {
            get
            {
                return minuto;
            }
            private set
            {
                if (EnCambiaMinuto != null)
                    EnCambiaMinuto(this, new CambiaMinutoEventArgs(string.Format("{0:mm}", DateTime.Now)));
                minuto = value;
            }
        }

        public int Segundo
        {
            get
            {
                return segundo;
            }
            private set
            {
                if (EnCambiaSegundo != null)
                    EnCambiaSegundo(this, new CambiaSegundoEventArgs(string.Format("{0:ss}", DateTime.Now)));
                segundo = value;
            }
        }

        public ComidasDia Horario
        {
            get
            {
                return horario;
            }
            set
            {
                if (EnCambiaHorario != null)
                    EnCambiaHorario(this, new CambiaHorarioEventArgs(value));
                horario = value;
            }
        }

        public event CambiaHora EnCambiaHora;
        public event CambiaMinuto EnCambiaMinuto;
        public event CambiaSegundo EnCambiaSegundo;
        public event CambiaHorario EnCambiaHorario;

        private readonly static DispatcherTimer timer = new DispatcherTimer();

        private static readonly Lazy<Reloj> instance =
           new Lazy<Reloj>(() => new Reloj());

        public static Reloj Instance { get { return instance.Value; } }

        private Reloj()
        {
            hora = DateTime.Now.Hour;
            minuto = DateTime.Now.Minute;
            segundo = DateTime.Now.Second;
            horario = ComidasDia.Ninguna;
            timer.Tick += Timer_Tick;
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        public void RegisterHorarios(IEnumerable<Horario> horarios)
        {
            this.horarios = horarios.ToList();
            this.Horario = ComidasDia.Ninguna;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var time = DateTime.Now;

            Segundo = time.Second;

            if (time.Hour != hora)
                Hora = time.Hour;

            if (time.Minute != minuto)
                Minuto = time.Minute;

            if (horarios != null)
            {
                var timeNow = DateTime.Now.TimeOfDay;

                var horario = horarios.Where(h => timeNow >= h.Inicia && timeNow <= h.Finaliza).FirstOrDefault();

                if (horario == null)
                {
                    if (Horario != ComidasDia.Ninguna)
                        Horario = ComidasDia.Ninguna;
                }
                else
                {
                    if (horario.Nombre != Horario)
                    {
                        switch (horario.Nombre)
                        {
                            case ComidasDia.Desayuno:
                                Horario = horario.Nombre;
                                break;
                            case ComidasDia.Comida:
                                Horario = horario.Nombre;
                                break;
                            case ComidasDia.Cena:
                                Horario = horario.Nombre;
                                break;
                        }
                    }
                }
            }
        }
    }



    public delegate void CambiaHora(object sender, CambiaHoraEventArgs e);

    public class CambiaHoraEventArgs : EventArgs
    {
        public CambiaHoraEventArgs(string hora)
        {
            Hora = hora;
        }
        public string Hora { get; set; }
    }

    public delegate void CambiaMinuto(object sender, CambiaMinutoEventArgs e);

    public class CambiaMinutoEventArgs
    {
        public CambiaMinutoEventArgs(string minuto)
        {
            Minuto = minuto;
        }
        public string Minuto { get; set; }
    }

    public delegate void CambiaSegundo(object sender, CambiaSegundoEventArgs e);

    public class CambiaSegundoEventArgs
    {
        public CambiaSegundoEventArgs(string segundo)
        {
            Segundo = segundo;
        }
        public string Segundo { get; set; }
    }

    public delegate void CambiaHorario(object sender, CambiaHorarioEventArgs e);

    public class CambiaHorarioEventArgs
    {
        public CambiaHorarioEventArgs(ComidasDia horario)
        {
            Horario = horario;
        }
        public ComidasDia Horario { get; set; }
    }
}
