using System;
using System.Runtime.Serialization;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines a response to a SendOffer request.
    /// </summary>
    [DataContract()]
    public class SendOfferResponse : ResponseBase
    {
        #region Declarations
        #region _Members_
        private TradeOffer _TradeOffer;

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets the trade offer.
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