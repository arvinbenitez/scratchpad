using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Script.Services;
using CIT.CAB.Web.Domain.Manager;

namespace CIT.CAB.Web.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TradeService" in code, svc and config file together.
    public class TradeService : ITradeService
    {

        #region ITradeService Members

        /// <summary>
        /// Get the list of trades for the given trad
        /// </summary>
        /// <param name="tradingGroupId"></param>
        /// <param name="tradeDate"></param>
        /// <returns></returns>
        public IEnumerable<Domain.Model.Trade> GetTrades(string tradingGroupId, string tradeDate)
        {
            DateTime date = DateTime.ParseExact(tradeDate, "yyyyMMdd", null);
            TradeManager tradeManager = new TradeManager();
            return tradeManager.GetTrade(date, Convert.ToInt32(tradingGroupId));
        }

        #endregion
    }
}
