using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using CIT.CAB.Web.Domain.Model;
using CIT.CAB.Web.Entity;

namespace CIT.CAB.Web.Domain.Manager
{
    public class TradeManager
    {
        public IEnumerable<Trade> GetTrade(DateTime tradeDate, int tradingGroupId)
        {
            IList<Trade> trades = new List<Trade>();
            var tradeRepository = new GenericRepository<CAB_Trade>();
            var cabTrades = tradeRepository.Get(
                x =>
                EntityFunctions.TruncateTime(x.TradeTime) == EntityFunctions.TruncateTime(tradeDate) && x.TradingGroupId == tradingGroupId);
            foreach (var cabTrade in cabTrades)
            {
                trades.Add(new Trade(cabTrade));
            }
            return trades;
        }
    }
}
