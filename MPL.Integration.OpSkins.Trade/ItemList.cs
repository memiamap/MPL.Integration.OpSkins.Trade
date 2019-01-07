using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MPL.Integration.OpSkins.Trade
{
    [CollectionDataContract()]
    public class ItemList : List<Item>
    {
        #region Methods
        #region _Public_
        /// <summary>
        /// Gets an item by it's asset identifier.
        /// </summary>
        /// <param name="assetID">A string containing the asset identifier of the item to get.</param>
        /// <returns>An Item with with asset identifier.</returns>
        public Item GetItemByAssetID(string assetID)
        {
            int AssetID;
            Item ReturnValue = null;

            if (int.TryParse(assetID, out AssetID))
            {
                foreach (Item Item in this)
                    if (Item.ID == AssetID)
                    {
                        ReturnValue = Item;
                        break;
                    }
            }

            return ReturnValue;
        }

        #endregion
        #endregion
    }
}