using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines a person involved in a trade.
    /// </summary>
    [DataContract()]
    public class TradePerson
    {
        #region Declarations
        #region _Members_
        private string _Avatar;
        private string _DisplayName;
        private List<Item> _Items;
        private string _SteamID;
        private int _UID;

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets the person's avatar.
        /// </summary>
        [DataMember(Name = "avatar")]
        public string Avatar
        {
            get { return _Avatar; }
            private set { _Avatar = value; }
        }

        /// <summary>
        /// Gets the person's display name.
        /// </summary>
        [DataMember(Name = "display_name")]
        public string DisplayName
        {
            get { return _DisplayName; }
            private set { _DisplayName = value; }
        }

        /// <summary>
        /// Gets a list of items relating to the person in the trade.
        /// </summary>
        [DataMember(Name = "items")]
        public List<Item> Items
        {
            get { return _Items; }
            private set { _Items = value; }
        }

        /// <summary>
        /// Gets the person's Steam identifier.
        /// </summary>
        [DataMember(Name = "steam_id")]
        public string SteamID
        {
            get { return _SteamID; }
            private set { _SteamID = value; }
        }

        /// <summary>
        /// Gets the person's user identifier.
        /// </summary>
        [DataMember(Name = "uid")]
        public int UID
        {
            get { return _UID; }
            private set { _UID = value; }
        }

        #endregion
    }
}