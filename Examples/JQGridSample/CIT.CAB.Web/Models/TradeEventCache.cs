using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using CIT.CAB.Web.Entity;
using System.Threading;
using System.Data.Objects;

namespace CIT.CAB.Web.Models
{
    public class TradeEventCache
    {
        private static TradeEventCache _instance;


        private TradeEventCache()
        {
            //start a Thread to query the database
            Thread updateThread = new Thread(GetUpdates);
            updateThread.IsBackground = true;
            updateThread.Start();
        }

        private MessageBrokerContext context = new MessageBrokerContext();
        ConcurrentDictionary<int, CabTradeEvent> cache = new ConcurrentDictionary<int, CabTradeEvent>();
        private int lastEventId = -1;


        private void GetUpdates()
        {
            DateTime today = DateTime.Today;
            while (true)
            {

                if(today != DateTime.Today)
                {
                    today = DateTime.Today;
                    //let's clear the cache
                    cache.Clear();
                }

                //get all message ids that have updates from the lastUpdate
                IList<int> updatedTrades = new List<int>();

                if (lastEventId == -1) //initial load?, let's get the latest event
                {
                    var latestEvent =
                        context.SYS_TableEvent.OrderByDescending(y => y.EventId).FirstOrDefault();
                    if (latestEvent != null)
                    {
                        lastEventId = latestEvent.EventId;
                    }
                }

                if (lastEventId > -1)
                {
                    var events = (from ev in context.SYS_TableEvent
                                  where ev.EventId > lastEventId
                                  select ev); 

                    foreach(var cabEvent in events)
                    {
                        updatedTrades.Add(int.Parse(cabEvent.KeyValue));
                        if (cabEvent.EventId > lastEventId)
                        {
                            lastEventId = cabEvent.EventId;
                        }
                    }

                    //now get all of trades
                    var trades =
                        (from x in context.CabTrades
                         where updatedTrades.Contains(x.MessageId)
                         select new CabTradeEvent() { Trade = x }).ToList();

                    //now let's update the Cache
                    try
                    {
                        foreach (var trade in trades)
                        {
                            trade.LastUpdate = ConvertToGMT(DateTime.Now);
                            cache.AddOrUpdate(trade.Trade.MessageId, trade, (key, value) => { return trade; });
                        }
                    }
                    catch
                    {
                    }
                }
                Thread.Sleep(5000);
            }
        }

        /// <summary>
        /// Converts the time from LOCAL to GMT
        /// </summary>
        /// <param name="lastUpdate"></param>
        /// <returns></returns>
        private static DateTime ConvertToGMT(DateTime lastUpdate)
        {
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            return TimeZoneInfo.ConvertTime(lastUpdate, TimeZoneInfo.Local, zone);
        }

        public static TradeEventCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TradeEventCache();
                }
                return _instance;
            }
        }

        public IEnumerable<CabTrade> GetTradeUpdates(DateTime lastUpdate, IList<int> tradingGroups, DateTime tradeDate)
        {
            lastUpdate = lastUpdate.AddSeconds(-2); // just give some allowance for network latency
            IList<CabTrade> updatedTrades = new List<CabTrade>();
            try
            {
                (from x in cache.Values
                 where x.LastUpdate >= lastUpdate &&
                       tradingGroups.Contains(x.Trade.TradingGroupId) &&
                       x.Trade.TradeTime.Date == tradeDate.Date
                 orderby x.LastUpdate
                 select x.Trade).ToList().ForEach(x => updatedTrades.Add(x));
            }
            catch
            {
                //thread timed-out
            }
            return updatedTrades;
        }
    }

    internal class CabTradeEvent
    {
        public CabTrade Trade { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}