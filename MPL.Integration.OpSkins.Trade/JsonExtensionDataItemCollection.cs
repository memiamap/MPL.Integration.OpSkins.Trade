using System;
using System.Collections.Generic;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines a collection of JSON extension data items.
    /// </summary>
    public class JsonExtensionDataItemCollection : List<JsonExtensionDataItem>
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        internal JsonExtensionDataItemCollection()
        { }

        #endregion

        #region Methods
        #region _Public_
        /// <summary>
        /// Gets an item with the specified name.
        /// </summary>
        /// <param name="name">A string containing the name of the item.</param>
        /// <returns>A JsonExtensionDataItem with the specified name.</returns>
        public JsonExtensionDataItem GetItemByName(string name)
        {
            JsonExtensionDataItem ReturnValue = null;

            foreach (JsonExtensionDataItem Item in this)
                if (Item.Name == name)
                {
                    ReturnValue = Item;
                    break;
                }
            return ReturnValue;
        }

        #endregion
        #endregion
    }
}