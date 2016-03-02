using System;
using System.IO;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Data;
using Microsoft.Reporting.WinForms;
using ServiciosCafeteria.AppModels;
using ServiciosCafeteria.Interfaces;

namespace ServiciosCafeteria.Instancias
{
    public class Impresora : IImpresora
    {
        public void Imprimir(Ticket ticket)
        {
            var table = new DataTable("Ticket");
            var report = new LocalReport { ReportPath = @"..\..\Ticket.rdlc" };
            report.DataSources.Add(new ReportDataSource("Ticket", new List<Ticket> { ticket }));
            Export(report);
            Print();
        }

        private int _mCurrentPageIndex;
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
            Metafile pageImage = new
                Metafile(_mStreams[_mCurrentPageIndex]);

            // Adjust rectangular area with printer margins.
            System.Drawing.Rectangle adjustedRect = new System.Drawing.Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(System.Drawing.Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            _mCurrentPageIndex++;
            ev.HasMorePages = (_mCurrentPageIndex < _mStreams.Count);
        }

        private void Print()
        {
            if (_mStreams == null || _mStreams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();

            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += PrintPage;
                _mCurrentPageIndex = 0;
                printDoc.Print();
            }
        }

        public void Dispose()
        {
            if (_mStreams != null)
            {
                foreach (var stream in _mStreams)
                    stream.Close();
                _mStreams = null;
            }
        }
    }
}


