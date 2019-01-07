using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines a response to a GetOffers request.
    /// </summary>
    [DataContract()]
    public class GetOffersResponse : ResponseBase
    {
        #region Declarations
        #region _Members_
        private int _Total;
        private List<TradeOffer> _TradeOffers;

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets the total number of trade offers that matches the input filters.
        /// </summary>
        [DataMember(Name = "total")]
        public int Total
        {
            get { return _Total; }
            private set { _Total = value; }
        }
        
        /// <summary>
        /// Gets the trade offers.
        /// </summary>
        [DataMember(Name = "offers")]
        public List<TradeOffer> TradeOffers
        {
            get { return _TradeOffers; }
            private set { _TradeOffers = value; }
        }

        #endregion
    }
}