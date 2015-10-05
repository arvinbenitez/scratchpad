using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LinqGrouping
{
    class Program
    {
        static void Main(string[] args)
        {
            var flatVersions = new List<FlatVersions>
            {
                new FlatVersions {VersionId = 1, NodeName = "Version 1", CustomFieldId = 1, CustomFieldValue = "Format A", DeliveryId = 1, DestinationName = "GMA"},
                new FlatVersions {VersionId = 1, NodeName = "Version 1", CustomFieldId = 2, CustomFieldValue = "Format A", DeliveryId = 1, DestinationName = "GMA"},
                new FlatVersions {VersionId = 1, NodeName = "Version 1", CustomFieldId = 1, CustomFieldValue = "Format A", DeliveryId = 2, DestinationName = "ABS"},
                new FlatVersions {VersionId = 1, NodeName = "Version 1", CustomFieldId = 2, CustomFieldValue = "Format A", DeliveryId = 2, DestinationName = "ABS"},
                new FlatVersions {VersionId = 2, NodeName = "Version 2", CustomFieldId = 1, CustomFieldValue = "Format B", DeliveryId = 3, DestinationName = "GMA"},
                new FlatVersions {VersionId = 2, NodeName = "Version 2", CustomFieldId = 2, CustomFieldValue = "Format B", DeliveryId = 3, DestinationName = "GMA"},
                new FlatVersions {VersionId = 2, NodeName = "Version 2", CustomFieldId = 1, CustomFieldValue = "Format B", DeliveryId = 4, DestinationName = "ABS"},
                new FlatVersions {VersionId = 2, NodeName = "Version 2", CustomFieldId = 2, CustomFieldValue = "Format B", DeliveryId = 5, DestinationName = "ABS"}
            };

            //var versions = from x in flatVersions
            //    group x by x.VersionId into versionGroup
            //    let versionGroups = from v in versionGroup
            //        select new VersionDto
            //        {
            //            Id = v.VersionId,
            //            KeyNumber = v.NodeName,
            //            CustomFields = (from cf in versionGroup
            //                select new CustomField {Id = cf.CustomFieldId, Value = cf.CustomFieldValue}).ToList(),
            //            Deliveries = (from d in versionGroup
            //                select new Delivery {Id = d.DeliveryId, DestinationName = d.DestinationName}).ToList()
            //        }
            //    select versionGroups;

            var versionx = from version in flatVersions
                group version by new {version.VersionId, version.NodeName}
                into versionGroup
                select new VersionDto
                {
                    Id = versionGroup.Key.VersionId,
                    KeyNumber = versionGroup.Key.NodeName,
                    CustomFields = (from cf in versionGroup
                                    group cf by new{cf.CustomFieldId, cf.CustomFieldValue} into cfg
                                    select new CustomField {Id = cfg.Key.CustomFieldId, Value = cfg.Key.
                                        CustomFieldValue}).ToList(),
                    Deliveries = (from d in versionGroup
                                  select new Delivery {Id = d.DeliveryId, DestinationName = d.DestinationName}).ToList()
                };

            foreach (var version in versionx)
            {
                Console.WriteLine("Id={0},Name={1}", version.Id, version.KeyNumber);
                foreach (var customField in version.CustomFields)
                {
                    Console.WriteLine("\tCustomFieldId={0},Value={1}", customField.Id, customField.Value);
                }
                foreach (var delivery in version.Deliveries)
                {
                    Console.WriteLine("\tDeliverId={0},Destination={1}", delivery.Id, delivery.DestinationName);
                }
            }
            Console.ReadLine();
        }
    }

    class FlatVersions
    {
        public int VersionId { get; set; }
        public string  NodeName { get; set; }

        public string  CustomFieldValue { get; set; }

        public int CustomFieldId { get; set; }

        public int DeliveryId { get; set; }

        public string DestinationName { get; set; }

    }

    class VersionDto
    {
        public int Id { get; set; }
        public string KeyNumber { get; set; }

        public IList<CustomField> CustomFields { get; set; }

        public IList<Delivery> Deliveries { get; set; }


    }

    class Delivery
    {
        public int Id { get; set; }
        public string DestinationName { get; set; }
    }

    class CustomField
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
