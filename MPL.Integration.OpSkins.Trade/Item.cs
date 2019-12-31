using System;
using System.Runtime.Serialization;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines an item.
    /// </summary>
    [DataContract()]
    public class Item : IComparable<Item>, IComparable, IExtensibleDataObject
    {
        public Item()
        { }

        #region Declarations
        #region _Members_
        private ExtensionDataObject _ExtensionData;
        private float _SuggestedPriceFloat;
        private float _SuggestedPriceFloorFloat;

        #endregion
        #endregion

        #region Methods
        #region _Private_
        private void LoadFromExtensionData()
        {
            JsonExtensionDataItemCollection Data;
            JsonExtensionDataItem ImageItem;

            Data = HelperFunctions.ConvertExtensionData(_ExtensionData);
            ImageItem = Data.GetItemByName("image");
            if (ImageItem != null)
            {
                if (ImageItem.IsClassType)
                {
                    if (ImageItem.Items.Count == 2)
                    {
                        Images = new ItemImages
                        {
                            SmallImage = ImageItem.Items[0].Value.ToString(),
                            LargeImage = ImageItem.Items[1].Value.ToString()
                        };
                    }
                    else
                    {
                        string SmallImage = null;
                        string LargeImage = null;

                        foreach (JsonExtensionDataItem Item in ImageItem.Items)
                        {
                            string ItemValue;

                            ItemValue = Item.Value.ToString();
                            if (ItemValue.Contains("-300."))
                            {
                                SmallImage = ItemValue;
                            }
                            else if (ItemValue.Contains("-600."))
                            {
                                LargeImage = ItemValue;
                            }
                        }

                        if (SmallImage == null)
                            SmallImage = ImageItem.Items[0].Value.ToString();
                        if (LargeImage == null)
                            LargeImage = ImageItem.Items[0].Value.ToString();

                        Images = new ItemImages
                        {
                            SmallImage = SmallImage,
                            LargeImage = LargeImage
                        };
                    }
                }
                else if (ImageItem.DataType.Name == "String")
                {
                    Images = new ItemImages
                    {
                        SmallImage = ImageItem.Value.ToString(),
                        LargeImage = ImageItem.Value.ToString()
                    };
                }
                else
                {
                    Console.WriteLine("WTF");
                }
            }
        }

        #endregion
        #region _Public_
        public override string ToString()
        {
            return string.Format("{0} - {1}", Name, SuggestedPrice);
        }

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets the item's category.
        /// </summary>
        [DataMember(Name = "category")]
        public string Category { get; private set; }

        /// <summary>
        /// Gets the item's colour.
        /// </summary>
        [DataMember(Name = "color")]
        public string Colour { get; private set; }

        /// <summary>
        /// Gets a link to inspect the Ethereum blockchain used to generate the item.
        /// </summary>
        [DataMember(Name = "eth_inspect")]
        public string EthereumInspectLink { get; private set; }

        /// <summary>
        /// Gets or sets the extension data for this class.
        /// </summary>
        public ExtensionDataObject ExtensionData
        {
            get
            {
                return _ExtensionData;
            }
            set
            {
                _ExtensionData = value;
                LoadFromExtensionData();
            }
        }

        /// <summary>
        /// Gets the item's identifier.
        /// </summary>
        [DataMember(Name = "id")]
        public int ID { get; private set; }

        /// <summary>
        /// Gets the item's images.
        /// </summary>
        public ItemImages Images { get; private set; }

        /// <summary>
        /// Gets the an inspection link for the item.
        /// </summary>
        [DataMember(Name = "inspect")]
        public string Inspect { get; private set; }

        /// <summary>
        /// Gets the item's internal application identifier.
        /// </summary>
        [DataMember(Name = "internal_app_id")]
        public int InternalAppID { get; private set; }

        /// <summary>
        /// Gets an indication of whether the item is missing.
        /// </summary>
        [DataMember(Name = "missing")]
        public bool IsMissing { get; private set; }

        /// <summary>
        /// Gets the item's name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; private set; }

        /// <summary>
        /// Gets the item's paint index.
        /// </summary>
        [DataMember(Name = "paint_index")]
        public string PaintIndex { get; private set; }

        /// <summary>
        /// Gets the item's pattern index.
        /// </summary>
        [DataMember(Name = "pattern_index")]
        public string PatternIndex { get; private set; }

        /// <summary>
        /// Gets the item's preview URLs.
        /// </summary>
        [DataMember(Name = "preview_urls")]
        public ItemPreviewURLs PreviewURLs { get; private set; }

        /// <summary>
        /// Gets the item's rarity.
        /// </summary>
        [DataMember(Name = "rarity")]
        public string Rarity { get; private set; }

        /// <summary>
        /// Gets the item's SKU.
        /// </summary>
        [DataMember(Name = "sku")]
        public int Sku { get; private set; }

        /// <summary>
        /// Gets the suggested price for the item.
        /// </summary>
        [DataMember(Name = "suggested_price")]
        public int? SuggestedPrice { get; private set; }

        /// <summary>
        /// Gets the suggested price for the item.
        /// </summary>
        public float SuggestedPriceFloat
        {
            get 
            {
                if (_SuggestedPriceFloat == 0)
                    _SuggestedPriceFloat = (float)SuggestedPrice / 100;

                return _SuggestedPriceFloat; 
            }
        }
        
        /// <summary>
        /// Gets the suggested price for the item (floor).
        /// </summary>
        [DataMember(Name = "suggested_price_floor")]
        public int? SuggestedPriceFloor { get; private set; }

        /// <summary>
        /// Gets the suggested price for the item.
        /// </summary>
        public float SuggestedPriceFloorFloat
        {
            get
            {
                if (_SuggestedPriceFloorFloat == 0)
                    _SuggestedPriceFloorFloat = (float)SuggestedPriceFloor / 100;

                return _SuggestedPriceFloorFloat;
            }
        }

        /// <summary>
        /// Gets the timestamp of the trade hold expiry.
        /// </summary>
        [DataMember(Name = "trade_hold_expires")]
        public string TradeHoldExpiry { get; private set; }

        /// <summary>
        /// Gets the item type.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; private set; }

        /// <summary>
        /// Gets the item wear float.
        /// </summary>
        [DataMember(Name = "wear")]
        public string Wear { get; private set; }

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
                int MyPrice = 0;
                int OtherPrice = 0;

                if (this.SuggestedPrice.HasValue)
                    MyPrice = this.SuggestedPrice.Value;
                if (other.SuggestedPrice.HasValue)
                    OtherPrice = other.SuggestedPrice.Value;
                ReturnValue = MyPrice.CompareTo(OtherPrice);
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