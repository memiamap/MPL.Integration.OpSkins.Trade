using System;
using System.Runtime.Serialization;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines a response to a GetOffer request.
    /// </summary>
    [DataContract()]
    public class GetOfferResponse : ResponseBase
    {
        #region Properties
        /// <summary>
        /// Gets the trade offers.
        /// </summary>
        [DataMember(Name = "offer")]
        public TradeOffer TradeOffer { get; private set; }

        #endregion
    }
}