using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using AguaDeMaria.Model;

namespace AguaDeMaria.Report
{
    public class DeliveryReceiptPdf : BaseReport
    {
        DeliveryReceipt deliveryReceipt;
        string fileNameTemplate;
        public DeliveryReceiptPdf(DeliveryReceipt receipt, string fileNameDrTemplate)
            : base(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false), PageSize.LETTER)
        {
            deliveryReceipt = receipt;
            fileNameTemplate = fileNameDrTemplate;
        }

        protected override void SetupTextContent()
        {
            SetFont(20);
            PrintTextCentered("Delivery Receipt", 280, 680);
            SetFont(14);
            PrintTextCentered("No. " + deliveryReceipt.DRNumber, 280, 700);
            PrintRuler(true, true);
        }

        protected override void SetupImageContent()
        {
            PrintImage(fileNameTemplate, 0, 680);
        }
    }
}
