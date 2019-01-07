using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines a response to a AcceptTrade request.
    /// </summary>
    [DataContract()]
    public class AcceptTradeResponse : ResponseBase
    {
        #region Declarations
        #region _Members_
        private int _FailedCases;
        private List<Item> _NewItems;
        private TradeOffer _TradeOffer;

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets the number of failed cases.
        /// </summary>
        [DataMember(Name = "failed_cases")]
        public int FailedCases
        {
            get { return _FailedCases; }
            private set { _FailedCases = value; }
        }

        /// <summary>
        /// Gets any new items created as part of the trade.
        /// </summary>
        [DataMember(Name = "new_items")]
        public List<Item> NewItems
        {
            get { return _NewItems; }
            private set { _NewItems = value; }
        }

        /// <summary>
        /// Gets the accepted trade offer.
        /// </summary>
        [DataMember(Name = "offer")]
        public TradeOffer TradeOffer
        {
            get { return _TradeOffer; }
            private set { _TradeOffer = value; }
        }

        #endregion
    }
}