using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CIT.CAB.Web.Entity;
using System.Data.Objects.SqlClient;
using CIT.CAB.Web.Models;

namespace CIT.CAB.Web.Controllers
{
    public class CabController : Controller
    {
        IGenericRepository<CabTrade> _tradeRepository = new GenericRepository<CabTrade>();
        //
        // GET: /Cab/
        public ActionResult Index()
        {
            //Get the TradingGroups and add it into the viewBag
            var tradingGroups = new List<SelectListItem>();
            foreach (var tradingGroup in ReferenceData.TradingGroups)
            {
                tradingGroups.Add(new SelectListItem() { Text = tradingGroup.ShortCode, Value = tradingGroup.TradingGroupId.ToString() });
            }
            ViewBag.TradingGroups = tradingGroups;
            return View();
        }

        public ActionResult Trades(string tradeDate, string tradingGroups)
        {
            var groups = ConvertToGroupList(tradingGroups);
            DateTime cabTradeDate = tradeDate == null ? DateTime.Today : DateTime.Parse(tradeDate);
            var trades =
                _tradeRepository.Get(
                    x => EntityFunctions.TruncateTime(cabTradeDate) == EntityFunctions.TruncateTime(x.TradeTime) && groups.Contains(x.TradingGroupId),
                    y => y.OrderByDescending(x => x.MessageId));
            return Json(trades, JsonRequestBehavior.AllowGet);
        }

        private static IList<int> ConvertToGroupList(string tradingGroups)
        {
            IList<int> groups = new List<int>();
            foreach (var tradingGroup in tradingGroups.Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries))
            {
                int value = 0;
                if (int.TryParse(tradingGroup, out value))
                {
                    groups.Add(value);
                }
            }
            if (groups.Count == 0)
            {
                //add unknown
                groups.Add(0);
            }
            return groups;
        }

        public JsonResult TradeUpdates(string lastUpdate, string tradeDate, string tradingGroups)
        {

            var groups = ConvertToGroupList(tradingGroups);

            //if the last update time is not present, get the updates from the 
            DateTime time = string.IsNullOrEmpty(lastUpdate) ? DateTime.Now.AddMinutes(-1) : DateTime.Parse(lastUpdate);
            time = ConvertToGMT(time);
            DateTime cabTradeDate = tradeDate == null ? DateTime.Today : DateTime.Parse(tradeDate);

            var trades = TradeEventCache.Instance.GetTradeUpdates(time, groups, cabTradeDate);
            return Json(trades, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ContextMenuSample()
        {
            return PartialView();
        }

        /// <summary>
        /// Converts the time from UTC to GMT
        /// </summary>
        /// <param name="lastUpdate"></param>
        /// <returns></returns>
        private static DateTime ConvertToGMT(DateTime lastUpdate)
        {
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            return TimeZoneInfo.ConvertTime(lastUpdate, TimeZoneInfo.Utc, zone);
        }
    }
}
