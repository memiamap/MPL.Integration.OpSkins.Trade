using System;

namespace MPL.Integration.OpSkins.Trade
{
    #region Declarations
    #region _Enumerations_
    /// <summary>
    /// An enumeration that defines the state of a trade offer.
    /// </summary>
    public enum TradeOfferState : int
    {
        /// <summary>
        /// The state is unknown.
        /// </summary>
        Unknown = 0,

        Accepted = 3,

        Active = 2,

        Cancelled = 6,

        CaseOpenPending = 9,

        CaseOpenExpired = 10,

        CaseOpenFailed = 12,

        Declined = 7,

        Expired = 5,

        InvalidItems = 8,
    }

    #endregion
    #endregion
}