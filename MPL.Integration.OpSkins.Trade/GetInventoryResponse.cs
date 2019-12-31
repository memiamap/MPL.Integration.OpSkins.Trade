using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines a response to a GetInventory request.
    /// </summary>
    [DataContract()]
    public class GetInventoryResponse : ResponseBase
    {
        public GetInventoryResponse()
        { }

        #region Declarations
        #region _Members_
        private ItemList _Items;
        private List<SortParameter> _SortParameters;
        private int _Total;

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets the inventory items.
        /// </summary>
        [DataMember(Name = "items")]
        public ItemList Items
        {
            get { return _Items; }
            private set { _Items = value; }
        }

        [DataMember(Name = "items_in_active_offers", IsRequired = false, EmitDefaultValue = false)]
        public object ItemsInOffers { get; private set; }
        [DataMember(Name = "user_data", IsRequired = false, EmitDefaultValue = false)]
        public object User { get; private set; }

        /// <summary>
        /// Gets the sort parameters.
        /// </summary>
        [DataMember(Name = "sort_parameters")]
        public List<SortParameter> SortParameters
        {
            get { return _SortParameters; }
            private set { _SortParameters = value; }
        }

        /// <summary>
        /// Gets the total number of items that matched the input filters.
        /// </summary>
        [DataMember(Name = "total")]
        public int Total
        {
            get { return _Total; }
            private set { _Total = value; }
        }

        #endregion
    }
}