using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using CIT.CAB.Web.Entity;

namespace CIT.CAB.Web.Domain.Model
{
    [DataContract]
    public class Trade
    {
        public Trade()
        {
        }

        public Trade(CAB_Trade cabTrade)
        {
            this.TradeId = cabTrade.TradeId;
            this.MessageId = cabTrade.MessageId;
            this.TradeTime = cabTrade.TradeTime;
            this.TradeGroup = cabTrade.TradeGroupName;
            this.TradeRef = cabTrade.MarketTradeId;
            this.TradingGroup = cabTrade.TradingGroupName;
            this.Product = cabTrade.InternalProductName;
        }

        [DataMember]
        public int TradeId { get; set; }
        [DataMember]
        public int MessageId { get; set; }
        [DataMember]
        public DateTime TradeTime { get; set; }
        [DataMember]
        public string TradeGroup { get; set; }
        [DataMember]
        public string TradeRef { get; set; }
        [DataMember]
        public string TradingGroup { get; set; }
        [DataMember]
        public string Product { get; set; }

    }
}
