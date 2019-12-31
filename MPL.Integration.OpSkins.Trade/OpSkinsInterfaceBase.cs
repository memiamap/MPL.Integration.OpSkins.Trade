using System;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines base functionality of an OpSkins Trade API interface.
    /// </summary>
    public class OpSkinsInterfaceBase
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="endpointName">A string containing the name of the OpSkins Trade API endpoint.</param>
        protected OpSkinsInterfaceBase(string endpointName)
        {
            EndpointName = endpointName;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the OpSkins Trade API endpoint.
        /// </summary>
        internal string EndpointName { get; }

        #endregion
    }
}
