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
            : base(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false), PageSize.LETTER.Rotate())
        {
            deliveryReceipt = receipt;
            fileNameTemplate = fileNameDrTemplate;
        }

        protected override void SetupTextContent()
        {
            SetFont(11);
            //DR Number
            PrintText(deliveryReceipt.DRNumber, 300, 445);
            PrintText(deliveryReceipt.DRNumber, 670, 445);
            //DR Date
            PrintText(deliveryReceipt.DRDate.ToString("MMM-dd-yyy"), 290, 425);
            PrintText(deliveryReceipt.DRDate.ToString("MMM-dd-yyy"), 660, 425);

            //Customer Name
            PrintText(deliveryReceipt.Customer.CustomerName, 70, 400);
            PrintText(deliveryReceipt.Customer.CustomerName, 440, 400);

            //Customer Name
            PrintText(deliveryReceipt.Customer.Address, 70, 378);
            PrintText(deliveryReceipt.Customer.Address, 440, 378);


            PrintRuler(true, true);
        }

        protected override void SetupImageContent()
        {
            PrintImage(fileNameTemplate, 20, 40, 380, 600);
            PrintImage(fileNameTemplate, 390, 40, 380, 600);
        }

        protected override void SetupLineContent()
        {
            PrintLine(395, 10, 395, 600);
        }
    }
}
