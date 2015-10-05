using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using CIT.CAB.Web.Entity;
using CIT.CAB.Web.Models;

namespace CIT.CAB.Web.Controllers
{
    public class LogController : Controller
    {
        MessageBrokerContext context = new MessageBrokerContext();
        ////
        //// GET: /Log/
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Log(string messageId)
        {
            int id = int.Parse(messageId);
            var brokerLogs = ((from x in context.TMB_MessageLogging
                               join y in context.TMB_Logging on x.LoggingId equals y.LoggingId
                               where x.MessageId == id
                               select y).Union(
                                   from x in context.TMA_PublishedMessageLogging
                                   join y in context.TMB_Logging on x.LoggingId equals y.LoggingId
                                   join z in context.TMB_PublishedMessage on x.PublishedMessageId equals
                                       z.PublishedMessageId
                                   where z.MessageID == id
                                   select y)).OrderBy(x => x.TimeStamp).ToList();
            IList<TradeLog> tradeLogs = new List<TradeLog>();
            brokerLogs.ForEach( x=> tradeLogs.Add(new TradeLog(){LogMessage = x.LogMessage, TimeStamp = x.TimeStamp}));
            return PartialView("Log",tradeLogs);
        }

        public ActionResult PublishedMessage(string messageId)
        {
            int id = int.Parse(messageId);
            var publishedMessage = (from x in context.TMB_PublishedMessage
                                    where x.MessageID == id
                                    select x).FirstOrDefault();
            return PartialView("PublishedMessage", publishedMessage);
        }

        public ActionResult TradeMessage(string messageId)
        {
            int id = int.Parse(messageId);
            var publishedMessage = (from x in context.TMB_Message
                                    where x.MessageId == id
                                    select x).FirstOrDefault();
            return PartialView("TradeMessage", publishedMessage);
        }

        public static string FormatXml(string xmlString)
        {
            string formattedXml = string.Empty;
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlString);
            using (StringWriter sWriter = new StringWriter())
            {
                using (XmlTextWriter writer = new XmlTextWriter(sWriter))
                {
                    writer.Formatting = Formatting.Indented;
                    document.Save(writer);
                    writer.Flush();
                }
                formattedXml = sWriter.ToString();
            }
            return formattedXml;
        }
    }
}
