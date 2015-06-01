using System;
using System.Linq;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Model;
using AguaDeMaria.Model.Dto;

namespace AguaDeMaria.Service.Implementation
{
    public class InventoryLedgerService : IInventoryLedgerService
    {
        private readonly IRepository<InventoryLedger> inventoryLedgerRepository;

        public InventoryLedgerService(IRepository<InventoryLedger> inventoryLedgerRepository)
        {
            this.inventoryLedgerRepository = inventoryLedgerRepository;
        }

        public void PostToLedger(DeliveryReceipt deliveryReceipt)
        {
            foreach (var deliveryReceiptDetail in deliveryReceipt.DeliveryReceiptDetails)
            {
                PostToLedger(deliveryReceipt.DeliveryReceiptId, deliveryReceipt.CustomerId,
                    DataConstants.InventoryLedgerType.Delivery,
                    deliveryReceipt.DRDate, deliveryReceiptDetail.ProductTypeId, deliveryReceiptDetail.Quantity,
                    string.Empty);
            }
        }

        public void PostToLedger(PickupSlip pickupSlip)
        {
            foreach (var pickupSlipDetail in pickupSlip.PickupSlipDetails)
            {
                PostToLedger(pickupSlip.PickupSlipId, pickupSlip.CustomerId,
                    DataConstants.InventoryLedgerType.PickupSlip,
                    pickupSlip.PickupDate, pickupSlipDetail.ProductTypeId, -1*pickupSlipDetail.Quantity, string.Empty);
            }
        }

        public void SetBeginningBalance(InventoryLedgerDto inventoryLedgerDto)
        {
            //Slim
            PostToLedger(DataConstants.Inventory.BeginningBalanceId, inventoryLedgerDto.CustomerId,
                DataConstants.InventoryLedgerType.Adjustment, inventoryLedgerDto.PostDate,
                DataConstants.ProductTypes.Slim, inventoryLedgerDto.SlimQty, inventoryLedgerDto.Notes);
            //Round
            PostToLedger(DataConstants.Inventory.BeginningBalanceId, inventoryLedgerDto.CustomerId,
                DataConstants.InventoryLedgerType.Adjustment, inventoryLedgerDto.PostDate,
                DataConstants.ProductTypes.Round, inventoryLedgerDto.RoundQty, inventoryLedgerDto.Notes);
        }

        public InventoryLedgerDto GetBeginningBalance(int customerId)
        {
            var inventoryLedger = new InventoryLedgerDto
            {
                CustomerId = customerId,
                Notes = "Beginning Balance",
                PostDate = DateTime.UtcNow
            };

            var ledgerEntries =
                inventoryLedgerRepository.Get(
                    x =>
                        x.CustomerId == customerId && x.ReferenceId == DataConstants.Inventory.BeginningBalanceId &&
                        x.InventoryLedgerTypeId == DataConstants.InventoryLedgerType.Adjustment);
            foreach (var entry in ledgerEntries)
            {
                if (entry.ProductTypeId == DataConstants.ProductTypes.Slim)
                {
                    inventoryLedger.SlimQty = entry.Quantity;
                }
                else if (entry.ProductTypeId == DataConstants.ProductTypes.Round)
                {
                    inventoryLedger.RoundQty = entry.Quantity;
                }
                inventoryLedger.PostDate = entry.PostDate;
            }
            return inventoryLedger;
        }

        private void PostToLedger(int referenceId, int customerId, DataConstants.InventoryLedgerType ledgerType, DateTime postDate, int productTypeId, int quantity, string notes)
        {
            var ledgerEntry =
                inventoryLedgerRepository.Get(
                    x =>
                        x.ReferenceId == referenceId &&
                        x.InventoryLedgerTypeId == ledgerType &&
                        x.ProductTypeId == productTypeId && 
                        x.CustomerId == customerId).FirstOrDefault();

            if (ledgerEntry == null)
            {
                inventoryLedgerRepository.Insert(new InventoryLedger
                {
                    CustomerId = customerId,
                    InventoryLedgerTypeId = ledgerType,
                    PostDate = postDate,
                    ProductTypeId = productTypeId,
                    ReferenceId = referenceId,
                    Quantity = quantity,
                    Notes = notes
                });
            }
            else
            {
                ledgerEntry.PostDate = postDate;
                ledgerEntry.Quantity = quantity;
                ledgerEntry.Notes = notes;
                inventoryLedgerRepository.Update(ledgerEntry);
            }
            inventoryLedgerRepository.Commit();
        }
    }
}