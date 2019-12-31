using System;
using System.Runtime.Serialization;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines images for an item.
    /// </summary>
    [DataContract()]
    public class ItemImages
    {
        #region Declarations
        #region _Members_
        private string _LargeImage;
        private string _SmallImage;

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets the item's large image.
        /// </summary>
        [DataMember(Name = "600px")]
        public string LargeImage
        {
            get { return _LargeImage; }
            set { _LargeImage = value; }
        }

        /// <summary>
        /// Gets the item's small image.
        /// </summary>
        [DataMember(Name = "300px")]
        public string SmallImage
        {
            get { return _SmallImage; }
            set { _SmallImage = value; }
        }

        #endregion
    }
}