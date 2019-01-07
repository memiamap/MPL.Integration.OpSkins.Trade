using System;
using System.Runtime.Serialization;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines the response to an API operation.
    /// </summary>
    /// <typeparam name="T">A ResponseBase that is the response object.</typeparam>
    [DataContract()]
    public class ApiResponse<T>
        where T : ResponseBase
    {
        #region Declarations
        #region _Members_
        private string _Message;
        private T _Response;
        private int _Status;
        private int _Time;

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets the response.
        /// </summary>
        [DataMember(Name = "message")]
        public string Message
        {
            get { return _Message; }
            private set { _Message = value; }
        }

        /// <summary>
        /// Gets the response.
        /// </summary>
        [DataMember(Name = "response")]
        public T Response
        {
            get { return _Response; }
            private set { _Response = value; }
        }

        /// <summary>
        /// Gets the request status.
        /// </summary>
        [DataMember(Name = "status")]
        public int Status
        {
            get { return _Status; }
            private set { _Status = value; }
        }

        /// <summary>
        /// GEts the server time.
        /// </summary>
        [DataMember(Name = "time")]
        public int Time
        {
            get { return _Time; }
            private set { _Time = value; }
        }

        #endregion
    }
}