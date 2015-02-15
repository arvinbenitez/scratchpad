using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System;

namespace AguaDeMaria.Report
{
    public abstract class BaseReport : IDisposable
    {
        private BaseFont baseFont;
        private Rectangle pageSize;
        PdfContentByte textContent;
        Document document;

        public BaseReport(BaseFont font, Rectangle pageSize)
        {
            baseFont = font;
            this.pageSize = pageSize;
            document = new Document(pageSize);
        }


        private void SetupDocument()
        {
        }

        #region Protected Methods
        protected void SetFont(float fontSize)
        {
            textContent.SetFontAndSize(baseFont, fontSize);
        }

        protected void PrintText(string text, int x, int y)
        {
            textContent.ShowTextAligned(PdfContentByte.ALIGN_LEFT, text, x, y, 0);
        }

        protected void PrintTextCentered(string text, int x, int y)
        {
            textContent.ShowTextAligned(PdfContentByte.ALIGN_CENTER, text, x, y, 0);
        }

        protected void PrintRuler(bool horizontal, bool vertical)
        {
            if (horizontal)
            {
                PrintXAxis((int)pageSize.Height - 100);
                PrintXAxis(100);
            }
            if (vertical)
            {
                PrintYAxis(40);
                PrintYAxis((int)pageSize.Width - 40);
            }
        }

        protected void PrintImage(string filePath, int x, int y)
        {
            PrintImage(filePath, x, y, 0, 0);
        }

        protected void PrintImage(string filePath, int x, int y, int scaleWidth, int scaleHeight)
        {
            Image image = Image.GetInstance(filePath);
            image.SetAbsolutePosition(x, y);
            if (scaleWidth > 0 && scaleHeight > 0)
            {
                image.ScaleToFit(scaleWidth, scaleHeight);
            }
            document.Add(image);
        }

        protected void PrintLine(int startX, int startY, int endX, int endY)
        {
            textContent.SetLineWidth(0.5f);
            textContent.MoveTo(startX, startY);
            textContent.LineTo(endX, endY);
            textContent.Stroke();
        }


        #endregion

        /// <summary>
        /// Method that should populate the contents of the report here
        /// </summary>
        protected abstract void SetupTextContent();
        protected abstract void SetupImageContent();
        protected abstract void SetupLineContent();

        public byte[] GenerateContent()
        {
            MemoryStream output = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, output);
            writer.CloseStream = false;
            document.Open();
            document.NewPage();

            SetupImageContent();
            SetupTextContent(writer);

            writer.Flush();
            document.Close();

            return ConventStreamToBytes(output); ;
        }

        #region Private Methods

        private static byte[] ConventStreamToBytes(MemoryStream output)
        {
            byte[] buffer = new byte[output.Position];
            output.Position = 0;
            output.Read(buffer, 0, buffer.Length);
            return buffer;
        }

        private void SetupTextContent(PdfWriter writer)
        {
            textContent = writer.DirectContent;
            textContent.BeginText();
            SetupTextContent();
            textContent.EndText();
            SetupLineContent();
        }

        private void PrintXAxis(int y)
        {
            SetFont(7);
            int x = 1200;
            while (x >= 0)
            {
                if (x % 20 == 0)
                {
                    PrintTextCentered("" + x, x, y + 8);
                    PrintTextCentered("|", x, y);
                }
                else
                {
                    PrintTextCentered(".", x, y);
                }
                x -= 5;
            }
        }

        private void PrintYAxis(int x)
        {
            SetFont(7);
            int y = 1200;
            while (y >= 0)
            {
                if (y % 20 == 0)
                {
                    PrintText("__ " + y, x, y);
                }
                else
                {
                    PrintText("_", x, y);
                }
                y = y - 5;
            }
        }
        #endregion Private Methods

        public void Dispose()
        {
         //   document.Dispose();
        }
    }
}
