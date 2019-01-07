using System;
using System.Runtime.Serialization;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines a generic response to a User-based request.
    /// </summary>
    [DataContract()]
    public class UserResponse : ResponseBase
    {
        #region Declarations
        #region _Members_
        private User _User;

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets the user object associated with the response.
        /// </summary>
        [DataMember(Name = "user")]
        public User User
        {
            get { return _User; }
            private set { _User = value; }
        }

        #endregion
    }
}