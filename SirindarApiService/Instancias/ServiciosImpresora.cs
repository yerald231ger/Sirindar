using System;
using System.IO;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;
using ServiciosCafeteria.AppModels;
using ServiciosCafeteria.Interfaces;
using System.Drawing;

namespace ServiciosCafeteria.Instancias
{
    public class Impresora : IImpresora
    {
        public void Imprimir(Ticket ticket)
        {
            var report = new LocalReport { ReportPath = @"..\..\Ticket.rdlc" };
            report.DataSources.Add(new ReportDataSource("Ticket", new List<Ticket> { ticket }));
            Export(report);
            Print();
        }

        private IList<Stream> _mStreams;

        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            _mStreams.Add(stream);
            return stream;
        }
        // Export the given report as an EMF (Enhanced Metafile) file.
        private void Export(LocalReport report)
        {
            string deviceInfo =
                @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>7.6cm</PageWidth>
                <PageHeight>10cm</PageHeight>
                <MarginTop>0.25cm</MarginTop>
                <MarginLeft>0.25cm</MarginLeft>
                <MarginRight>0.25cm</MarginRight>
                <MarginBottom>0.25cm</MarginBottom>
            </DeviceInfo>";

            Warning[] warnings;

            _mStreams = new List<Stream>();

            report.Render("Image", deviceInfo, CreateStream, out warnings);

            foreach (Stream stream in _mStreams)
                stream.Position = 0;
        }
        // Handler for PrintPageEvents
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            var pageImage = new
                Metafile(_mStreams[0]);

            // Adjust rectangular area with printer margins.
            var adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);
        }

        private void Print()
        {
            if (_mStreams == null || _mStreams.Count == 0)
                throw new Exception("Error: no stream to print.");
            var printDoc = new PrintDocument();

            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += PrintPage;
                printDoc.Print();
            }
        }

        public void Dispose()
        {
            if (_mStreams == null)
                return;
            foreach (var stream in _mStreams)
                stream.Close();
            _mStreams = null;
        }
    }

    public class Impresora2 : IImpresora
    {
        public static Font PrintFont { get; } = new Font("Arial", 10);
        private Ticket _ticketToPrint;

        // The PrintPage event is raised for each page to be printed.
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            float rightMargin = ev.MarginBounds.Right;
            var lineSpaceing = PrintFont.GetHeight(ev.Graphics) + 1;

            ev.Graphics.DrawString(_ticketToPrint.Fecha.ToString("G"), PrintFont, Brushes.DodgerBlue, rightMargin, topMargin, new StringFormat(StringFormatFlags.DirectionRightToLeft));

            ev.Graphics.DrawString("Deportista", PrintFont, Brushes.DodgerBlue, leftMargin, topMargin + 2 * lineSpaceing);
            ev.Graphics.DrawString(_ticketToPrint.Deportista, PrintFont, Brushes.DodgerBlue, leftMargin + 100, topMargin + 2 * lineSpaceing);

            ev.Graphics.DrawString("Deporte", PrintFont, Brushes.DodgerBlue, leftMargin, topMargin + 3 * lineSpaceing);
            ev.Graphics.DrawString(_ticketToPrint.Deporte, PrintFont, Brushes.DodgerBlue, leftMargin + 100, topMargin + 3 * lineSpaceing);

            ev.Graphics.DrawString("Dependencia", PrintFont, Brushes.DodgerBlue, leftMargin, topMargin + 4 * lineSpaceing);
            ev.Graphics.DrawString(_ticketToPrint.Dependencia, PrintFont, Brushes.DodgerBlue, leftMargin + 100, topMargin + 4 * lineSpaceing);

            ev.Graphics.DrawString("Comida", PrintFont, Brushes.DodgerBlue, leftMargin, topMargin + 5 * lineSpaceing);
            ev.Graphics.DrawString(_ticketToPrint.Comida, PrintFont, Brushes.DodgerBlue, leftMargin + 100, topMargin + 5 * lineSpaceing);

            var sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            ev.Graphics.DrawString("Grupo y Raciones", PrintFont, Brushes.DodgerBlue, leftMargin + 140, topMargin + 8 * lineSpaceing, sf);
            ev.Graphics.DrawString(_ticketToPrint.GrupoRaciones, PrintFont, Brushes.DodgerBlue, leftMargin + 140, topMargin + 9 * lineSpaceing, sf);


            ev.Graphics.DrawString("Proteninas", PrintFont, Brushes.DodgerBlue, leftMargin + (float)46.66666666666667, topMargin + 14 * lineSpaceing, sf);
            ev.Graphics.DrawString("Carbohidratos", PrintFont, Brushes.DodgerBlue, leftMargin + 140, topMargin + 14 * lineSpaceing, sf);
            ev.Graphics.DrawString("Lipidos", PrintFont, Brushes.DodgerBlue, leftMargin + (float)233.33333333333333, topMargin + 14 * lineSpaceing, sf);

            ev.Graphics.DrawString(_ticketToPrint.GramosProteina.ToString(), PrintFont, Brushes.DodgerBlue, leftMargin + (float)46.66666666666667, topMargin + 15 * lineSpaceing, sf);
            ev.Graphics.DrawString(_ticketToPrint.GramosCarboHidratos.ToString(), PrintFont, Brushes.DodgerBlue, leftMargin + 140, topMargin + 15 * lineSpaceing, sf);
            ev.Graphics.DrawString(_ticketToPrint.GramosLipidos.ToString(), PrintFont, Brushes.DodgerBlue, leftMargin + (float)233.33333333333333, topMargin + 15 * lineSpaceing, sf);

            ev.Graphics.DrawString("Observaciones", PrintFont, Brushes.DodgerBlue, leftMargin, topMargin + 16 * lineSpaceing);
            ev.Graphics.DrawString(_ticketToPrint.GrupoRaciones, PrintFont, Brushes.DodgerBlue, leftMargin, topMargin + 17 * lineSpaceing);
        }
        
        public void Printing(Ticket ticket)
        {
            try
            {
                _ticketToPrint = ticket;
                var pd = new PrintDocument();
                var paper = new PaperSize("Custom", 300, 394);
                pd.DefaultPageSettings.PaperSize = paper;
                pd.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);
                pd.PrintPage += pd_PrintPage;
                // Print the document.
                pd.Print();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Imprimir(Ticket ticket)
        {
            Printing(ticket);
        }
    }
}


