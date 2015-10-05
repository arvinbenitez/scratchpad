using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Article.Pdf {


    public class DiplomaPrinter {

        // ------------------------------
        // Class fields
        // ------------------------------
        
        private static readonly BaseFont font = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);


        // ------------------------------
        // Instance fields
        // ------------------------------

        private readonly string _name;
        private readonly string _distance;
        private readonly string _date;
        private readonly string _raceName;
        private readonly bool _showRulers;

        private PdfContentByte _pcb;

        // ------------------------------
        // Constructors
        // ------------------------------

        public DiplomaPrinter(string name, string distance, string date, string raceName, bool showRulers) {
            _name = name;
            _distance = distance;
            _date = date;
            _raceName = raceName;
            _showRulers = showRulers;
        }

        // ------------------------------
        // Instance methods
        // ------------------------------
        
        public void Create(Stream output) {
            Document document = new Document();
            PdfReader readerBicycle = null;

            try {
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                document.Open();

                // Load the background image and add it to the document structure
                readerBicycle = new PdfReader(Resources.GetBicycle());
                PdfTemplate background = writer.GetImportedPage(readerBicycle, 1);

                // Create a page in the document and add it to the bottom layer
                document.NewPage();
                _pcb = writer.DirectContentUnder;
                _pcb.AddTemplate(background, 0, 0);

                // Get the top layer and write some text
                _pcb = writer.DirectContent;
                _pcb.BeginText();

                if (_showRulers) {
                    PrintXAxis(800);
                    PrintXAxis(100);
                    PrintYAxis(40);
                    PrintYAxis(500);
                }
                SetFont36();
                PrintTextCentered(_raceName, 280, 680);
                PrintTextCentered(_name, 280, 190);

                SetFont18();
                PrintTextCentered(_date, 280, 640);
                PrintTextCentered("has completed a distance of " + _distance, 280, 160);

                _pcb.EndText();

                writer.Flush();
            }
            finally {
                if (readerBicycle!= null) {
                    readerBicycle.Close();
                }
                document.Close();
            }
        }

        private void SetFont7() {
            _pcb.SetFontAndSize(font, 7);
        }

        private void SetFont18() {
            _pcb.SetFontAndSize(font, 18);
        }

        private void SetFont36() {
            _pcb.SetFontAndSize(font, 36);
        }

        private void PrintText(string text, int x, int y) {
            _pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, text, x, y, 0);
        }

        private void PrintTextCentered(string text, int x, int y) {
            _pcb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, text, x, y, 0);
        }

        private void PrintXAxis(int y) {
            SetFont7();
            int x = 600;
            while(x>=0) {
                if (x%20==0) {
                    PrintTextCentered(""+x, x, y+8);
                    PrintTextCentered("|", x, y);
                }
                else {
                    PrintTextCentered(".",x,y);
                }
                x -= 5;
            }
        }

        private void PrintYAxis(int x) {
            SetFont7();
            int y = 800;
            while(y>=0) {
                if (y%20==0) {
                    PrintText("__ "+y, x, y);
                }
                else {
                    PrintText("_", x, y);
                }
                y = y - 5;
            }
        }
    }


}