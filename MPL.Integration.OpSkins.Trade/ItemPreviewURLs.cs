using System;
using System.Runtime.Serialization;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines item preview URLs for an item.
    /// </summary>
    [DataContract()]
    public class ItemPreviewURLs
    {
        #region Declarations
        #region _Members_
        private string _Back;
        private string _Front;
        private string _Thumbnail;
        private string _Video;

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets a URL to the item's back image.
        /// </summary>
        [DataMember(Name = "thumb_image")]
        public string Back
        {
            get { return _Back; }
            private set { _Back = value; }
        }

        /// <summary>
        /// Gets a URL to the item's front image.
        /// </summary>
        [DataMember(Name = "front_image")]
        public string Front
        {
            get { return _Front; }
            private set { _Front = value; }
        }

        /// <summary>
        /// Gets a URL to the item's thumbnail.
        /// </summary>
        [DataMember(Name = "back_image")]
        public string Thumbnail
        {
            get { return _Thumbnail; }
            private set { _Thumbnail = value; }
        }

        /// <summary>video image.
        /// </summary>
        [DataMember(Name = "video")]
        public string Video
        {
            get { return _Video; }
            private set { _Video = value; }
        }

        #endregion
    }
}