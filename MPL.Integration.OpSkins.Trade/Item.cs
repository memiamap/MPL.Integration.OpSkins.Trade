using System;
using System.Runtime.Serialization;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines an item.
    /// </summary>
    [DataContract()]
    public class Item : IComparable<Item>, IComparable
    {
        #region Declarations
        #region _Members_
        private string _Category;
        private string _Colour;
        private string _EthereumInspectLink;
        private int _ID;
        private ItemImages _Images;
        private string _Inspect;
        private int _InternalAppID;
        private bool _IsMissing;
        private string _Name;
        private string _PaintIndex;
        private string _PatternIndex; 
        private ItemPreviewURLs _PreviewURLs;
        private string _Rarity;
        private int _Sku;
        private int _SuggestedPrice;
        private float _SuggestedPriceFloat;
        private int _SuggestedPriceFloor;
        private float _SuggestedPriceFloorFloat;
        private string _TradeHoldExpiry;
        private string _Type;
        private string _Wear;

        #endregion
        #endregion

        #region Methods
        #region _Public_
        public override string ToString()
        {
            return string.Format("{0} - {1}", _Name, _SuggestedPrice);
        }

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets the item's category.
        /// </summary>
        [DataMember(Name = "category")]
        public string Category
        {
            get { return _Category; }
            private set { _Category = value; }
        }

        /// <summary>
        /// Gets the item's colour.
        /// </summary>
        [DataMember(Name = "color")]
        public string Colour
        {
            get { return _Colour; }
            private set { _Colour = value; }
        }

        /// <summary>
        /// Gets a link to inspect the Ethereum blockchain used to generate the item.
        /// </summary>
        [DataMember(Name = "eth_inspect")]
        public string EthereumInspectLink
        {
            get { return _EthereumInspectLink; }
            private set { _EthereumInspectLink = value; }
        }

        /// <summary>
        /// Gets the item's identifier.
        /// </summary>
        [DataMember(Name = "id")]
        public int ID
        {
            get { return _ID; }
            private set { _ID = value; }
        }

        /// <summary>
        /// Gets the item's images.
        /// </summary>
        [DataMember(Name = "image")]
        public ItemImages Images
        {
            get { return _Images; }
            private set { _Images = value; }
        }

        /// <summary>
        /// Gets the an inspection link for the item.
        /// </summary>
        [DataMember(Name = "inspect")]
        public string Inspect
        {
            get { return _Inspect; }
            private set { _Inspect = value; }
        }

        /// <summary>
        /// Gets the item's internal application identifier.
        /// </summary>
        [DataMember(Name = "internal_app_id")]
        public int InternalAppID
        {
            get { return _InternalAppID; }
            private set { _InternalAppID = value; }
        }

        /// <summary>
        /// Gets an indication of whether the item is missing.
        /// </summary>
        [DataMember(Name = "missing")]
        public bool IsMissing
        {
            get { return _IsMissing; }
            private set { _IsMissing = value; }
        }

        /// <summary>
        /// Gets the item's name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name
        {
            get { return _Name; }
            private set { _Name = value; }
        }

        /// <summary>
        /// Gets the item's paint index.
        /// </summary>
        [DataMember(Name = "paint_index")]
        public string PaintIndex
        {
            get { return _PaintIndex; }
            private set { _PaintIndex = value; }
        }

        /// <summary>
        /// Gets the item's pattern index.
        /// </summary>
        [DataMember(Name = "pattern_index")]
        public string PatternIndex
        {
            get { return _PatternIndex; }
            private set { _PatternIndex = value; }
        }

        /// <summary>
        /// Gets the item's preview URLs.
        /// </summary>
        [DataMember(Name = "preview_urls")]
        public ItemPreviewURLs PreviewURLs
        {
            get { return _PreviewURLs; }
            private set { _PreviewURLs = value; }
        }

        /// <summary>
        /// Gets the item's rarity.
        /// </summary>
        [DataMember(Name = "rarity")]
        public string Rarity
        {
            get { return _Rarity; }
            private set { _Rarity = value; }
        }

        /// <summary>
        /// Gets the item's SKU.
        /// </summary>
        [DataMember(Name = "sku")]
        public int Sku
        {
            get { return _Sku; }
            private set { _Sku = value; }
        }

        /// <summary>
        /// Gets the suggested price for the item.
        /// </summary>
        [DataMember(Name = "suggested_price")]
        public int SuggestedPrice
        {
            get { return _SuggestedPrice; }
            private set { _SuggestedPrice = value; }
        }

        /// <summary>
        /// Gets the suggested price for the item.
        /// </summary>
        public float SuggestedPriceFloat
        {
            get 
            {
                if (_SuggestedPriceFloat == 0)
                    _SuggestedPriceFloat = (float)_SuggestedPrice / 100;

                return _SuggestedPriceFloat; 
            }
        }
        
        /// <summary>
        /// Gets the suggested price for the item (floor).
        /// </summary>
        [DataMember(Name = "suggested_price_floor")]
        public int SuggestedPriceFloor
        {
            get { return _SuggestedPriceFloor; }
            private set { _SuggestedPriceFloor = value; }
        }

        /// <summary>
        /// Gets the suggested price for the item.
        /// </summary>
        public float SuggestedPriceFloorFloat
        {
            get
            {
                if (_SuggestedPriceFloorFloat == 0)
                    _SuggestedPriceFloorFloat = (float)_SuggestedPriceFloor / 100;

                return _SuggestedPriceFloorFloat;
            }
        }

        /// <summary>
        /// Gets the timestamp of the trade hold expiry.
        /// </summary>
        [DataMember(Name = "trade_hold_expires")]
        public string TradeHoldExpiry
        {
            get { return _TradeHoldExpiry; }
            private set { _TradeHoldExpiry = value; }
        }

        /// <summary>
        /// Gets the item type.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type
        {
            get { return _Type; }
            private set { _Type = value; }
        }

        /// <summary>
        /// Gets the item wear float.
        /// </summary>
        [DataMember(Name = "wear")]
        public string Wear
        {
            get { return _Wear; }
            private set { _Wear = value; }
        }

        #endregion

        #region Interfaces
        #region _IComparable_
        int IComparable.CompareTo(object obj)
        {
            int ReturnValue = 0;

            if (obj != null && obj is Item)
                ReturnValue = ((IComparable<Item>)this).CompareTo((Item)obj);

            return ReturnValue;
        }

        #endregion
        #region _IComparable<Item>_
        int IComparable<Item>.CompareTo(Item other)
        {
            int ReturnValue = 0;

            if (other != null)
            {
                ReturnValue = this.SuggestedPrice.CompareTo(other.SuggestedPrice);
                if (ReturnValue == 0)
                {
                    ReturnValue = this.Name.CompareTo(other.Name);
                    if (ReturnValue == 0)
                    {
                        Random RNG;

                        RNG = new Random();
                        if (RNG.Next(0, 10) < 5)
                            ReturnValue = 1;
                        else
                            ReturnValue = -1;
                    }
                }
            }

            return ReturnValue;
        }

        #endregion
        #endregion
    }
}