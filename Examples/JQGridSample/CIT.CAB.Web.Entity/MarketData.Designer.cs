﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]

namespace CIT.CAB.Web.Entity
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class MarketDataContext : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new MarketDataContext object using the connection string found in the 'MarketDataContext' section of the application configuration file.
        /// </summary>
        public MarketDataContext() : base("name=MarketDataContext", "MarketDataContext")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new MarketDataContext object.
        /// </summary>
        public MarketDataContext(string connectionString) : base(connectionString, "MarketDataContext")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new MarketDataContext object.
        /// </summary>
        public MarketDataContext(EntityConnection connection) : base(connection, "MarketDataContext")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Market> Markets
        {
            get
            {
                if ((_Markets == null))
                {
                    _Markets = base.CreateObjectSet<Market>("Markets");
                }
                return _Markets;
            }
        }
        private ObjectSet<Market> _Markets;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<TradeGroup> TradeGroups
        {
            get
            {
                if ((_TradeGroups == null))
                {
                    _TradeGroups = base.CreateObjectSet<TradeGroup>("TradeGroups");
                }
                return _TradeGroups;
            }
        }
        private ObjectSet<TradeGroup> _TradeGroups;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<TradingGroup> TradingGroups
        {
            get
            {
                if ((_TradingGroups == null))
                {
                    _TradingGroups = base.CreateObjectSet<TradingGroup>("TradingGroups");
                }
                return _TradingGroups;
            }
        }
        private ObjectSet<TradingGroup> _TradingGroups;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Markets EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToMarkets(Market market)
        {
            base.AddObject("Markets", market);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the TradeGroups EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToTradeGroups(TradeGroup tradeGroup)
        {
            base.AddObject("TradeGroups", tradeGroup);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the TradingGroups EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToTradingGroups(TradingGroup tradingGroup)
        {
            base.AddObject("TradingGroups", tradingGroup);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="MarketDataModel", Name="Market")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Market : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Market object.
        /// </summary>
        /// <param name="marketId">Initial value of the MarketId property.</param>
        /// <param name="lUTime">Initial value of the LUTime property.</param>
        /// <param name="lUUserId">Initial value of the LUUserId property.</param>
        /// <param name="isBookingMarket">Initial value of the IsBookingMarket property.</param>
        /// <param name="requireLogin">Initial value of the RequireLogin property.</param>
        /// <param name="isPermissionable">Initial value of the IsPermissionable property.</param>
        public static Market CreateMarket(global::System.Int32 marketId, global::System.DateTime lUTime, global::System.Int32 lUUserId, global::System.Boolean isBookingMarket, global::System.Boolean requireLogin, global::System.Boolean isPermissionable)
        {
            Market market = new Market();
            market.MarketId = marketId;
            market.LUTime = lUTime;
            market.LUUserId = lUUserId;
            market.IsBookingMarket = isBookingMarket;
            market.RequireLogin = requireLogin;
            market.IsPermissionable = isPermissionable;
            return market;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 MarketId
        {
            get
            {
                return _MarketId;
            }
            set
            {
                if (_MarketId != value)
                {
                    OnMarketIdChanging(value);
                    ReportPropertyChanging("MarketId");
                    _MarketId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("MarketId");
                    OnMarketIdChanged();
                }
            }
        }
        private global::System.Int32 _MarketId;
        partial void OnMarketIdChanging(global::System.Int32 value);
        partial void OnMarketIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String MarketName
        {
            get
            {
                return _MarketName;
            }
            set
            {
                OnMarketNameChanging(value);
                ReportPropertyChanging("MarketName");
                _MarketName = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("MarketName");
                OnMarketNameChanged();
            }
        }
        private global::System.String _MarketName;
        partial void OnMarketNameChanging(global::System.String value);
        partial void OnMarketNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description;
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime LUTime
        {
            get
            {
                return _LUTime;
            }
            set
            {
                OnLUTimeChanging(value);
                ReportPropertyChanging("LUTime");
                _LUTime = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("LUTime");
                OnLUTimeChanged();
            }
        }
        private global::System.DateTime _LUTime;
        partial void OnLUTimeChanging(global::System.DateTime value);
        partial void OnLUTimeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 LUUserId
        {
            get
            {
                return _LUUserId;
            }
            set
            {
                OnLUUserIdChanging(value);
                ReportPropertyChanging("LUUserId");
                _LUUserId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("LUUserId");
                OnLUUserIdChanged();
            }
        }
        private global::System.Int32 _LUUserId;
        partial void OnLUUserIdChanging(global::System.Int32 value);
        partial void OnLUUserIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String DisplayName
        {
            get
            {
                return _DisplayName;
            }
            set
            {
                OnDisplayNameChanging(value);
                ReportPropertyChanging("DisplayName");
                _DisplayName = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("DisplayName");
                OnDisplayNameChanged();
            }
        }
        private global::System.String _DisplayName;
        partial void OnDisplayNameChanging(global::System.String value);
        partial void OnDisplayNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean IsBookingMarket
        {
            get
            {
                return _IsBookingMarket;
            }
            set
            {
                OnIsBookingMarketChanging(value);
                ReportPropertyChanging("IsBookingMarket");
                _IsBookingMarket = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("IsBookingMarket");
                OnIsBookingMarketChanged();
            }
        }
        private global::System.Boolean _IsBookingMarket;
        partial void OnIsBookingMarketChanging(global::System.Boolean value);
        partial void OnIsBookingMarketChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean RequireLogin
        {
            get
            {
                return _RequireLogin;
            }
            set
            {
                OnRequireLoginChanging(value);
                ReportPropertyChanging("RequireLogin");
                _RequireLogin = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("RequireLogin");
                OnRequireLoginChanged();
            }
        }
        private global::System.Boolean _RequireLogin;
        partial void OnRequireLoginChanging(global::System.Boolean value);
        partial void OnRequireLoginChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean IsPermissionable
        {
            get
            {
                return _IsPermissionable;
            }
            set
            {
                OnIsPermissionableChanging(value);
                ReportPropertyChanging("IsPermissionable");
                _IsPermissionable = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("IsPermissionable");
                OnIsPermissionableChanged();
            }
        }
        private global::System.Boolean _IsPermissionable;
        partial void OnIsPermissionableChanging(global::System.Boolean value);
        partial void OnIsPermissionableChanged();

        #endregion
    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="MarketDataModel", Name="TradeGroup")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class TradeGroup : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new TradeGroup object.
        /// </summary>
        /// <param name="tradeGroupId">Initial value of the TradeGroupId property.</param>
        /// <param name="displayName">Initial value of the DisplayName property.</param>
        /// <param name="isPowerTradeGroup">Initial value of the IsPowerTradeGroup property.</param>
        /// <param name="dataCacheTypeId">Initial value of the DataCacheTypeId property.</param>
        public static TradeGroup CreateTradeGroup(global::System.Int32 tradeGroupId, global::System.String displayName, global::System.Boolean isPowerTradeGroup, global::System.Int32 dataCacheTypeId)
        {
            TradeGroup tradeGroup = new TradeGroup();
            tradeGroup.TradeGroupId = tradeGroupId;
            tradeGroup.DisplayName = displayName;
            tradeGroup.IsPowerTradeGroup = isPowerTradeGroup;
            tradeGroup.DataCacheTypeId = dataCacheTypeId;
            return tradeGroup;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 TradeGroupId
        {
            get
            {
                return _TradeGroupId;
            }
            set
            {
                if (_TradeGroupId != value)
                {
                    OnTradeGroupIdChanging(value);
                    ReportPropertyChanging("TradeGroupId");
                    _TradeGroupId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("TradeGroupId");
                    OnTradeGroupIdChanged();
                }
            }
        }
        private global::System.Int32 _TradeGroupId;
        partial void OnTradeGroupIdChanging(global::System.Int32 value);
        partial void OnTradeGroupIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String DisplayName
        {
            get
            {
                return _DisplayName;
            }
            set
            {
                OnDisplayNameChanging(value);
                ReportPropertyChanging("DisplayName");
                _DisplayName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("DisplayName");
                OnDisplayNameChanged();
            }
        }
        private global::System.String _DisplayName;
        partial void OnDisplayNameChanging(global::System.String value);
        partial void OnDisplayNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean IsPowerTradeGroup
        {
            get
            {
                return _IsPowerTradeGroup;
            }
            set
            {
                OnIsPowerTradeGroupChanging(value);
                ReportPropertyChanging("IsPowerTradeGroup");
                _IsPowerTradeGroup = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("IsPowerTradeGroup");
                OnIsPowerTradeGroupChanged();
            }
        }
        private global::System.Boolean _IsPowerTradeGroup;
        partial void OnIsPowerTradeGroupChanging(global::System.Boolean value);
        partial void OnIsPowerTradeGroupChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 DataCacheTypeId
        {
            get
            {
                return _DataCacheTypeId;
            }
            set
            {
                OnDataCacheTypeIdChanging(value);
                ReportPropertyChanging("DataCacheTypeId");
                _DataCacheTypeId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("DataCacheTypeId");
                OnDataCacheTypeIdChanged();
            }
        }
        private global::System.Int32 _DataCacheTypeId;
        partial void OnDataCacheTypeIdChanging(global::System.Int32 value);
        partial void OnDataCacheTypeIdChanged();

        #endregion
    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="MarketDataModel", Name="TradingGroup")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class TradingGroup : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new TradingGroup object.
        /// </summary>
        /// <param name="tradingGroupId">Initial value of the TradingGroupId property.</param>
        /// <param name="shortCode">Initial value of the ShortCode property.</param>
        /// <param name="statusId">Initial value of the StatusId property.</param>
        public static TradingGroup CreateTradingGroup(global::System.Int32 tradingGroupId, global::System.String shortCode, global::System.Byte statusId)
        {
            TradingGroup tradingGroup = new TradingGroup();
            tradingGroup.TradingGroupId = tradingGroupId;
            tradingGroup.ShortCode = shortCode;
            tradingGroup.StatusId = statusId;
            return tradingGroup;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 TradingGroupId
        {
            get
            {
                return _TradingGroupId;
            }
            set
            {
                if (_TradingGroupId != value)
                {
                    OnTradingGroupIdChanging(value);
                    ReportPropertyChanging("TradingGroupId");
                    _TradingGroupId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("TradingGroupId");
                    OnTradingGroupIdChanged();
                }
            }
        }
        private global::System.Int32 _TradingGroupId;
        partial void OnTradingGroupIdChanging(global::System.Int32 value);
        partial void OnTradingGroupIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String ShortCode
        {
            get
            {
                return _ShortCode;
            }
            set
            {
                OnShortCodeChanging(value);
                ReportPropertyChanging("ShortCode");
                _ShortCode = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("ShortCode");
                OnShortCodeChanged();
            }
        }
        private global::System.String _ShortCode;
        partial void OnShortCodeChanging(global::System.String value);
        partial void OnShortCodeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Byte StatusId
        {
            get
            {
                return _StatusId;
            }
            set
            {
                OnStatusIdChanging(value);
                ReportPropertyChanging("StatusId");
                _StatusId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("StatusId");
                OnStatusIdChanged();
            }
        }
        private global::System.Byte _StatusId;
        partial void OnStatusIdChanging(global::System.Byte value);
        partial void OnStatusIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String BarcapCounterpartyName
        {
            get
            {
                return _BarcapCounterpartyName;
            }
            set
            {
                OnBarcapCounterpartyNameChanging(value);
                ReportPropertyChanging("BarcapCounterpartyName");
                _BarcapCounterpartyName = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("BarcapCounterpartyName");
                OnBarcapCounterpartyNameChanged();
            }
        }
        private global::System.String _BarcapCounterpartyName;
        partial void OnBarcapCounterpartyNameChanging(global::System.String value);
        partial void OnBarcapCounterpartyNameChanged();

        #endregion
    
    }

    #endregion
    
}
