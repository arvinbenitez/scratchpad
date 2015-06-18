using iTextSharp.text;
using iTextSharp.text.pdf;
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
            PrintText(deliveryReceipt.DRNumber, 300, 465);
            PrintText(deliveryReceipt.DRNumber, 670, 465);

            //DR Date
            PrintText("Customer's Copy", 40, 465);
            PrintText("Merchant's Copy", 410, 465);

            //DR Date
            PrintText(deliveryReceipt.DRDate.ToString("MMM-dd-yyy"), 290, 445);
            PrintText(deliveryReceipt.DRDate.ToString("MMM-dd-yyy"), 660, 445);

            //Customer Name
            PrintText(deliveryReceipt.CustomerName, 70, 425);
            PrintText(deliveryReceipt.CustomerName, 440, 425);

            //Customer Name
            PrintText(deliveryReceipt.CustomerAddress, 70, 400);
            PrintText(deliveryReceipt.CustomerAddress, 440, 400);

            if (deliveryReceipt.SlimQty > 0)
            {
                //Slim Qty
                PrintText(deliveryReceipt.SlimQty.ToString(), 40, 350);
                PrintText(deliveryReceipt.SlimQty.ToString(), 410, 350);

                //Slim Amount
                PrintText(deliveryReceipt.SlimAmount.ToString("#,##0.00"), 320, 350);
                PrintText(deliveryReceipt.SlimAmount.ToString("#,##0.00"), 685, 350);
            }

            if (deliveryReceipt.RoundQty > 0)
            {
                //Round Qty
                PrintText(deliveryReceipt.RoundQty.ToString(), 40, 332);
                PrintText(deliveryReceipt.RoundQty.ToString(), 410, 332);

                //Slim Amount
                PrintText(deliveryReceipt.RoundAmount.ToString("#,##0.00"), 320, 332);
                PrintText(deliveryReceipt.RoundAmount.ToString("#,##0.00"), 685, 332);
            }


            //Total Amount
            PrintText((deliveryReceipt.RoundAmount + deliveryReceipt.SlimAmount).ToString("#,##0.00"), 320, 265);
            PrintText((deliveryReceipt.RoundAmount + deliveryReceipt.SlimAmount).ToString("#,##0.00"), 685, 265);

            var previousSlimBalance = inventorySummary.Slim - deliveryReceipt.SlimQty;
            var previousRoundBalance = inventorySummary.Round - deliveryReceipt.RoundQty;
            //Print Inventory Balance
            PrintText(previousSlimBalance.ToString(), 160, 218);
            PrintText(previousRoundBalance.ToString(), 280, 218);

            PrintText(previousSlimBalance.ToString(), 540, 218);
            PrintText(previousRoundBalance.ToString(), 650, 218);
            
            //current delivery
            PrintText(deliveryReceipt.SlimQty.ToString(), 160, 202);
            PrintText(deliveryReceipt.RoundQty.ToString(), 280, 202);

            PrintText(deliveryReceipt.SlimQty.ToString(), 540, 202);
            PrintText(deliveryReceipt.RoundQty.ToString(), 650, 202);

            //new balance
            PrintText(inventorySummary.Slim.ToString(), 160, 184);
            PrintText(inventorySummary.Round.ToString(), 280, 184);

            PrintText(inventorySummary.Slim.ToString(), 540, 184);
            PrintText(inventorySummary.Round.ToString(), 650, 184);

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
