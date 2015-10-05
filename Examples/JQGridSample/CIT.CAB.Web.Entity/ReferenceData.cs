using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CIT.CAB.Web.Entity
{
    public class ReferenceData
    {
        static MarketDataContext context = new MarketDataContext();

        private static IEnumerable<TradeGroup> _tradeGroups;
        public static IEnumerable<TradeGroup> TradeGroups
        {
            get
            {
                if (_tradeGroups == null)
                {
                    _tradeGroups = context.TradeGroups.OrderBy(x => x.DisplayName);
                }
                return _tradeGroups;
            }
        }

        private static IEnumerable<TradingGroup> _tradingGroups;
        public static IEnumerable<TradingGroup> TradingGroups
        {
            get
            {
                if (_tradingGroups == null)
                {
                    _tradingGroups = context.TradingGroups.OrderBy(x => x.ShortCode);
                }
                return _tradingGroups;
            }
        }

    }
}
