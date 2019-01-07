using System;
using System.Runtime.Serialization;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines a standard user object.
    /// </summary>
    [DataContract()]
    public class User
    {
        #region Declarations
        #region _Members_
        private bool _Allow2faCodeReuse;
        private bool _ApiKeyExists;
        private string _Avatar;
        private string _DisplayName;
        private string _Email;
        private int _ID;
        private bool _Is2faEnabled;
        private bool _IsInventoryPrivate;
        private bool _IsVcaseRestricted;
        private string _Phone;
        private string _SteamID;

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets an indication of whether 2FA codes can be reused.
        /// </summary>
        [DataMember(Name = "allow_twofactor_code_reuse")]
        public bool Allow2faCodeReuse
        {
            get { return _Allow2faCodeReuse; }
            private set { _Allow2faCodeReuse = value; }
        }

        /// <summary>
        /// Gets an indication of whether an API key exists..
        /// </summary>
        [DataMember(Name = "api_key_exists")]
        public bool ApiKeyExists
        {
            get { return _ApiKeyExists; }
            private set { _ApiKeyExists = value; }
        }

        /// <summary>
        /// Gets the avatar of the user.
        /// </summary>
        [DataMember(Name = "avatar")]
        public string Avatar
        {
            get { return _Avatar; }
            private set { _Avatar = value; }
        }

        /// <summary>
        /// Gets the display name of the user.
        /// </summary>
        [DataMember(Name = "display_name")]
        public string DisplayName
        {
            get { return _DisplayName; }
            private set { _DisplayName = value; }
        }

        /// <summary>
        /// Gets the email of the user.
        /// </summary>
        [DataMember(Name = "contact_email")]
        public string Email
        {
            get { return _Email; }
            private set { _Email = value; }
        }

        /// <summary>
        /// Gets the identifier of the user.
        /// </summary>
        [DataMember(Name = "id")]
        public int ID
        {
            get { return _ID; }
            private set { _ID = value; }
        }

        /// <summary>
        /// Gets an indication of whether 2FA is enabled.
        /// </summary>
        [DataMember(Name = "twofactor_enabled")]
        public bool Is2faEnabled
        {
            get { return _Is2faEnabled; }
            private set { _Is2faEnabled = value; }
        }

        /// <summary>
        /// Gets an indication of whether the inventory is private.
        /// </summary>
        [DataMember(Name = "inventory_is_private")]
        public bool IsInventoryPrivate
        {
            get { return _IsInventoryPrivate; }
            private set { _IsInventoryPrivate = value; }
        }

        /// <summary>
        /// Gets an indication of whether the user is VCase restricted.
        /// </summary>
        [DataMember(Name = "vcase_restricted")]
        public bool IsVcaseRestricted
        {
            get { return _IsVcaseRestricted; }
            private set { _IsVcaseRestricted = value; }
        }

        /// <summary>
        /// Gets the phone number of the user.
        /// </summary>
        [DataMember(Name = "sms_phone")]
        public string Phone
        {
            get { return _Phone; }
            private set { _Phone = value; }
        }

        /// <summary>
        /// Gets the Steam identifier of the user.
        /// </summary>
        [DataMember(Name = "steam_id")]
        public string SteamID
        {
            get { return _SteamID; }
            private set { _SteamID = value; }
        }

        #endregion
    }
}