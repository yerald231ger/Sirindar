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
        public void Imprimir(Ticket ticket)
        {
            Printing();
        }
        private Font _printFont;
        private StreamReader _streamToPrint;
        public static string FilePath;

        // The PrintPage event is raised for each page to be printed.
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            var count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            string line = null;

            // Calculate the number of lines per page.
            var linesPerPage = ev.MarginBounds.Height /
                                 _printFont.GetHeight(ev.Graphics);

            // Iterate over the file, printing each line.
            while (count < linesPerPage &&
               ((line = _streamToPrint.ReadLine()) != null))
            {
                var yPos = topMargin + (count * _printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, _printFont, Brushes.Black,
                   leftMargin, yPos, new StringFormat());
                count++;
            }

            // If more lines exist, print another page.
            ev.HasMorePages = line != null;
        }

        // Print the file.
        public void Printing()
        {
            try
            {
                _streamToPrint = new StreamReader(@"..\..\App.config");
                try
                {
                    _printFont = new Font("Arial", 10);
                    var pd = new PrintDocument();
                    pd.PrintPage += pd_PrintPage;
                    // Print the document.
                    pd.Print();
                }
                finally
                {
                    _streamToPrint.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}


