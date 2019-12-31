using MPL.Common.Logging;
using System;
using System.Collections.Specialized;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that provides functionality for executing a web request against the OpSkins API.
    /// </summary>
    /// <typeparam name="T">A type that derives from ResponseBase that will be returned from the request.</typeparam>
    internal static class OpSkinsWebRequest<T>
        where T : ResponseBase
    {
        #region Methods
        #region _Internal_
        /// <summary>
        /// Executes a GET against the OpSkins API.
        /// </summary>
        /// <param name="url">A string containing the URL for the web request.</param>
        /// <param name="authToken">A string containing the OpSkins authentication token to use for the request.</param>
        /// <returns>An ApiResponse of T containing the result returned by the web request.</returns>
        internal static ApiResponse<T> ExecuteGet(string url, string authToken)
        {
            string Response;
            ApiResponse<T> ReturnValue;

            Response = OpSkinsWebRequest.ExecuteGet(url, authToken);
            ReturnValue = ProcessResponse(Response);

            return ReturnValue;
        }
        /// <summary>
        /// Executes a GET against the OpSkins API.
        /// </summary>
        /// <param name="url">A string containing the URL for the web request.</param>
        /// <param name="authToken">A string containing the OpSkins authentication token to use for the request.</param>
        /// <returns>An ApiResponse of T containing the result returned by the web request.</returns>
        internal static ApiResponse<T> ExecuteGet(string url, string authToken, NameValueCollection requestData)
        {
            string Response;
            ApiResponse<T> ReturnValue;

            Response = OpSkinsWebRequest.ExecuteGet(url, authToken, requestData);
            ReturnValue = ProcessResponse(Response);

            return ReturnValue;
        }

        /// <summary>
        /// Executes a POST against the OpSkins API.
        /// </summary>
        /// <param name="url">A string containing the URL for the web request.</param>
        /// <param name="authToken">A string containing the OpSkins authentication token to use for the request.</param>
        /// <param name="postData">A NameValueCollection containing any data to be POST'd as part of the web request.</param>
        /// <returns>An ApiResponse of T containing the result returned by the web request.</returns>
        internal static ApiResponse<T> ExecutePost(string url, string authToken, NameValueCollection postData = null)
        {
            string Response;
            ApiResponse<T> ReturnValue;

            Response = OpSkinsWebRequest.ExecutePost(url, authToken, postData);
            ReturnValue = ProcessResponse(Response);

            return ReturnValue;
        }
        
        /// <summary>
        /// Executes a web request against the OpSkins API.
        /// </summary>
        /// <param name="url">A string containing the URL for the web request.</param>
        /// <param name="method">A string indicating the method of the web request.</param>
        /// <param name="authToken">A string containing the OpSkins authentication token to use for the request.</param>
        /// <param name="data">A string containing data relating to the web request.</param>
        /// <returns>An ApiResponse of T containing the result returned by the web request.</returns>
        internal static ApiResponse<T> ExecuteRequest(string url, string method, string authToken, string data = null)
        {
            string Response;
            ApiResponse<T> ReturnValue;

            Response = OpSkinsWebRequest.ExecuteRequest(url, method, authToken, data);
            ReturnValue = ProcessResponse(Response);

            return ReturnValue;
        }

        /// <summary>
        /// Processes response data into a response object.
        /// </summary>
        /// <param name="response">A string containing the response data to process.</param>
        /// <returns>An ApiResponse<T> containing the processed data.</returns>
        internal static ApiResponse<T> ProcessResponse(string response)
        {
            ApiResponse<T> ReturnValue = null;

            try
            {
                //TODO: Fix this dirty hack!
                if (typeof(T).FullName.Contains("GetOffersResponse"))
                {
                    if (response.Contains("},false,{"))
                        response = response.Replace("},false,{", "},{");
                    if (response.Contains("[false,{"))
                        response = response.Replace("[false,{", "[{");
                    if (response.Contains("},false]"))
                        response = response.Replace("}.false]", "}]");
                }
                ReturnValue = Serialiser<ApiResponse<T>>.Deserialise(response);
            }
            catch (Exception ex)
            {
                LogWriter.LogException(ex);
                LogWriter.LogMessage("T is: {0}", typeof(T).FullName);
                LogWriter.LogMessage("Raw Data: {0}", response);
                throw new ArgumentException("Unable to deserialise the response data", "response", ex);
            }

            return ReturnValue;
        }

        #endregion
        #endregion
    }
}