using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using AguaDeMaria.Model;
using AguaDeMaria.Model.Dto;
using System;

namespace AguaDeMaria.Report
{
    public class DeliveryReceiptPdf : BaseReport
    {
        DeliveryDto deliveryReceipt;
        InventorySummary inventorySummary;
        string fileNameTemplate;
        public DeliveryReceiptPdf(DeliveryDto receipt,  InventorySummary inventorySummary, string fileNameDrTemplate)
            : base(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false), PageSize.LETTER.Rotate())
        {
            deliveryReceipt = receipt;
            fileNameTemplate = fileNameDrTemplate;
            this.inventorySummary = inventorySummary;
        }

        protected override void SetupTextContent()
        {
            SetFont(11);
            //DR Number
            PrintText(deliveryReceipt.DRNumber, 300, 445);
            PrintText(deliveryReceipt.DRNumber, 670, 445);

            //DR Date
            PrintText("Customer's Copy", 40, 445);
            PrintText("Merchant's Copy", 410, 445);

            //DR Date
            PrintText(deliveryReceipt.DRDate.ToString("MMM-dd-yyy"), 290, 425);
            PrintText(deliveryReceipt.DRDate.ToString("MMM-dd-yyy"), 660, 425);

            //Customer Name
            PrintText(deliveryReceipt.CustomerName, 70, 400);
            PrintText(deliveryReceipt.CustomerName, 440, 400);

            //Customer Name
            PrintText(deliveryReceipt.CustomerAddress, 70, 378);
            PrintText(deliveryReceipt.CustomerAddress, 440, 378);

            if (deliveryReceipt.SlimQty > 0)
            {
                //Slim Qty
                PrintText(deliveryReceipt.SlimQty.ToString(), 40, 315);
                PrintText(deliveryReceipt.SlimQty.ToString(), 410, 315);

                //Slim Amount
                PrintText(deliveryReceipt.SlimAmount.ToString("#,##0.00"), 320, 315);
                PrintText(deliveryReceipt.SlimAmount.ToString("#,##0.00"), 685, 315);
            }

            if (deliveryReceipt.RoundQty > 0)
            {
                //Round Qty
                PrintText(deliveryReceipt.RoundQty.ToString(), 40, 295);
                PrintText(deliveryReceipt.RoundQty.ToString(), 410, 295);

                //Slim Amount
                PrintText(deliveryReceipt.RoundAmount.ToString("#,##0.00"), 320, 295);
                PrintText(deliveryReceipt.RoundAmount.ToString("#,##0.00"), 685, 295);
            }


            //Total Amount
            PrintText((deliveryReceipt.RoundAmount + deliveryReceipt.SlimAmount).ToString("#,##0.00"), 320, 225);
            PrintText((deliveryReceipt.RoundAmount + deliveryReceipt.SlimAmount).ToString("#,##0.00"), 685, 225);

            //Print Inventory Balance
            PrintText(inventorySummary.Slim.ToString(), 160, 100);
            PrintText(inventorySummary.Round.ToString(), 280, 100);

            PrintText(inventorySummary.Slim.ToString(), 540, 100);
            PrintText(inventorySummary.Round.ToString(), 650, 100);

            //Print Date
            SetFont(7);
            PrintText(DateTime.Now.ToString("MMM-dd-yyyy hh:mm:ss tt"), 300, 30);
            PrintText(DateTime.Now.ToString("MMM-dd-yyyy hh:mm:ss tt"), 675, 30);

            //PrintRuler(true, true);
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
