using System;
using System.Runtime.Serialization;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines a sort parameter.
    /// </summary>
    [DataContract()]
    public class SortParameter
    {
        #region Declarations
        #region _Members_
        private string _Name;
        private int _Value;

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets the display name of the sort parameter.
        /// </summary>
        [DataMember(Name = "display_name")]
        public string Name
        {
            get { return _Name; }
            private set { _Name = value; }
        }

        /// <summary>
        /// Gets the value of the sort parameter.
        /// </summary>
        [DataMember(Name = "value")]
        public int Value
        {
            get { return _Value; }
            private set { _Value = value; }
        }

        #endregion
    }
}