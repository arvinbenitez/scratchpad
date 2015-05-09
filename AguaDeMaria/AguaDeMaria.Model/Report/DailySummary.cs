using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AguaDeMaria.Model.Report
{
    [Table("DailySummary")]
    public class DailySummary
    {
        [Key]
        public DateTime? Date { get; set; }

        public string Day {
            get
            {
                if (Date.HasValue)
                {
                    return Date.Value.DayOfWeek.ToString();
                }
                return string.Empty;
            }
        }
        public int? RoundOrderQty { get; set; }
        public int? SlimOrderQty { get; set; }
        public int? RoundDeliveredQty { get; set; }
        public int? SlimDeliveredQty { get; set; }

        public decimal? AmountPaid { get; set; }

        public decimal? PendingAmount
        {
            get
            {
                decimal? pendingAmount = TotalDeliveredAmt - AmountPaid;
                return pendingAmount.HasValue && pendingAmount > 0 ? pendingAmount : 0;
            }
        }

        public int? TotalDeliveredQty
        {
            get
            {
                if (RoundDeliveredQty.HasValue || SlimDeliveredQty.HasValue)
                {
                    return RoundDeliveredQty.GetValueOrDefault() + SlimDeliveredQty.GetValueOrDefault();
                }
                return null;
            }
        }

        public decimal? UnitPrice {
            get
            {
                if (TotalDeliveredQty.HasValue)
                {
                    return TotalDeliveredAmt.GetValueOrDefault()/TotalDeliveredQty.GetValueOrDefault();
                }
                return null;
            }
        }
        public decimal? RoundDeliveredAmt { get; set; }
        public decimal? SlimDeliveredAmt { get; set; }

        public decimal? TotalDeliveredAmt
        {
            get
            {
                if (RoundDeliveredAmt.HasValue || SlimDeliveredAmt.HasValue)
                {
                    return RoundDeliveredAmt.GetValueOrDefault() + SlimDeliveredAmt.GetValueOrDefault();
                }
                return null;
            }
        }

        public decimal? CollectionAmount { get; set; }
        public int? RoundPickupQty { get; set; }
        public int? SlimPickupQty { get; set; }
    }
}
