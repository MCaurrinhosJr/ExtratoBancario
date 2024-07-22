using ExtratoBancario.Core.Models;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.IO;


namespace ExtratoBancario.Core.Helper
{
    public class PdfCreater
    {
        public static byte[] GeneratePdf(IList<Transaction> transactions)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var document = new PdfDocument())
                {
                    var page = document.AddPage();
                    var gfx = XGraphics.FromPdfPage(page);

                    var font = new XFont("Verdana", 12, XFontStyleEx.Regular);

                    // Title
                    gfx.DrawString("Extrato de Transações", font, XBrushes.Black,
                        new XRect(0, 0, page.Width, 40),
                        XStringFormats.Center);

                    // Draw Table Header
                    var yPosition = 60;
                    gfx.DrawString("Data", font, XBrushes.Black,
                        new XRect(50, yPosition, page.Width, 20),
                        XStringFormats.TopLeft);

                    gfx.DrawString("Tipo da Transação", font, XBrushes.Black,
                        new XRect(150, yPosition, page.Width, 20),
                        XStringFormats.TopLeft);
                    gfx.DrawString("Valor", font, XBrushes.Black,
                        new XRect(300, yPosition, page.Width, 20),
                        XStringFormats.TopLeft);

                    // Draw Table Rows
                    yPosition += 20;
                    foreach (var transaction in transactions)
                    {
                        gfx.DrawString(transaction.Data.ToString("dd/MM"), font, XBrushes.Black,
                            new XRect(50, yPosition, page.Width, 20),
                            XStringFormats.TopLeft);

                        gfx.DrawString(transaction.TipoTransacao, font, XBrushes.Black,
                            new XRect(150, yPosition, page.Width, 20),
                            XStringFormats.TopLeft);

                        gfx.DrawString(transaction.Valor.ToString("C"), font, XBrushes.Black,
                            new XRect(300, yPosition, page.Width, 20),
                            XStringFormats.TopLeft);

                        yPosition += 20;
                    }

                    document.Save(memoryStream);
                }

                return memoryStream.ToArray();
            }
        }
    }
}
