using System;
using System.Collections.Generic;
using System.Linq;

namespace AguaDeMaria.Configuration.Mappers
{
    public class MapperConfiguration
    {
        public static void Configure()
        {
            OrderMapperConfiguration.ConfigureOrder();
            DeliveryReceiptMapperConfiguration.ConfigureDeliveryReceipt();
            PickupSlipMapperConfiguration.ConfigurePickupSlip();
            SalesInvoiceMapperConfiguration.ConfigureSalesInvoice();
        }

        #region Common Functions

        public static void MapChildToCollection<TCollection, TChild, TSource>(TCollection collectionObject,
                                                           TSource sourceObject,
                                                           Func<TChild, bool> existenceCheckPredicate,
                                                           Action<TChild, TSource> mappingPredicate)
            where TCollection : ICollection<TChild>
            where TChild : class , new()
        {
            var child = collectionObject.FirstOrDefault(existenceCheckPredicate);
            if (child == null)
            {
                child = new TChild();
                collectionObject.Add(child);
            }
            mappingPredicate.Invoke(child, sourceObject);
        }
        #endregion
    }
}