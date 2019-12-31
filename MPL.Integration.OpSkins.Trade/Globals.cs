using System;

namespace MPL.Integration.OpSkins.Trade
{
    #region Declarations
    #region _Enumerations_
    /// <summary>
    /// An enumeration that defines the sort order of items.
    /// </summary>
    public enum ItemSorts : int
    {
        /// <summary>
        /// The sorted order is undefined.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Order by id ASC (oldest first by creation).
        /// </summary>
        ByIdentifierAscending,

        /// <summary>
        /// Order by id DESC (newest first by creation).
        /// </summary>
        ByIdentifierDescending,

        /// <summary>
        /// Order by name ASC (alphabetical, z first).
        /// </summary>
        ByNameAscending = 1,

        /// <summary>
        /// Order by name DESC (alphabetical, a first).
        /// </summary>
        ByNameDescending = 2,

        /// <summary>
        /// Order by last_update ASC (oldest first by update).
        /// </summary>
        ByLastUpdateAscending = 3,

        /// <summary>
        /// Order by last_update DESC (newest first by update).
        /// </summary>
        ByLastUpdateDescending = 4,

        /// <summary>
        /// Order by suggested price ASC (lowest first).
        /// </summary>
        BySuggestedPriceAscending = 5,

        /// <summary>
        /// Order by suggested price DESC (highest first).
        /// </summary>
        BySuggestedPriceDescending = 6
    }

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

    /// <summary>
    /// An enumeration that defines the type of a trade offer.
    /// </summary>
    public enum TradeOfferTypes : int
    {
        /// <summary>
        /// The trade offer type is undefined.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// The trade offer type is both sent and received.
        /// </summary>
        Both,

        /// <summary>
        /// The trade offer type is received.
        /// </summary>
        Received,

        /// <summary>
        /// The trade offer type is sent.
        /// </summary>
        Sent
    }

    /// <summary>
    /// An enumeration that defines a triple-state boolean.
    /// </summary>
    public enum Troolean : int
    {
        /// <summary>
        /// The value is not set.
        /// </summary>
        NotSet = 0,

        /// <summary>
        /// The value is false.
        /// </summary>
        False,

        /// <summary>
        /// The value is true.
        /// </summary>
        True
    }

    #endregion
    #endregion
}