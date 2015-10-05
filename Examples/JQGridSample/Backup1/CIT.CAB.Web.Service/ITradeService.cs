using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CIT.CAB.Web.Domain.Model;

namespace CIT.CAB.Web.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITradeService" in both code and config file together.
    [ServiceContract]
    public interface ITradeService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getTrades/{tradingGroupId}/{tradeDate}")]
        IEnumerable<Trade> GetTrades(string tradingGroupId, string tradeDate);
    }
}
