using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that provides functionality for executing a web request against the OpSkins API.
    /// </summary>
    internal static class OpSkinsWebRequest
    {
        #region Methods
        #region _Internal_
        /// <summary>
        /// Executes a GET against the OpSkins API.
        /// </summary>
        /// <param name="url">A string containing the URL for the web request.</param>
        /// <param name="authToken">A string containing the OpSkins authentication token to use for the request.</param>
        /// <returns>A string containing the result returned by the web request.</returns>
        internal static string ExecuteGet(string url, string authToken)
        {
            return ExecuteRequest(url, "GET", authToken);
        }
        /// <summary>
        /// Executes a GET against the OpSkins API.
        /// </summary>
        /// <param name="url">A string containing the URL for the web request.</param>
        /// <param name="authToken">A string containing the OpSkins authentication token to use for the request.</param>
        /// <returns>A string containing the result returned by the web request.</returns>
        internal static string ExecuteGet(string url, string authToken, NameValueCollection requestData)
        {
            string RequestParams = string.Empty;
            string ReturnValue = null;

            // Build request data if there is any
            if (requestData != null && requestData.Count > 0)
            {
                if (url.Contains("?"))
                    RequestParams = "&";
                else
                    RequestParams = "?";
                foreach (string Key in requestData.AllKeys)
                    RequestParams += string.Format("{0}={1}&", Uri.EscapeDataString(Key), Uri.EscapeDataString(requestData[Key]));
                RequestParams = RequestParams.TrimEnd(new char[] { '&' });
            }
            ReturnValue = ExecuteRequest(url + RequestParams, "GET", authToken);

            return ReturnValue;
        }

        /// <summary>
        /// Executes a POST against the OpSkins API.
        /// </summary>
        /// <param name="url">A string containing the URL for the web request.</param>
        /// <param name="authToken">A string containing the OpSkins authentication token to use for the request.</param>
        /// <param name="postData">A NameValueCollection containing any data to be POST'd as part of the web request.</param>
        /// <returns>A string containing the result returned by the web request.</returns>
        internal static string ExecutePost(string url, string authToken, NameValueCollection postData = null)
        {
            string Data = null;
            string ReturnValue = null;

            if (postData != null && postData.Count > 0)
            {
                Data = string.Empty;

                foreach (string Key in postData.AllKeys)
                    Data += string.Format("{0}={1}&",  Uri.EscapeDataString(Key), Uri.EscapeDataString(postData[Key]));
                Data = Data.TrimEnd(new char[] { '&' });
            }

            ReturnValue = ExecuteRequest(url, "POST", authToken, Data);

            return ReturnValue;
        }

        /// <summary>
        /// Executes a web request against the OpSkins API.
        /// </summary>
        /// <param name="url">A string containing the URL for the web request.</param>
        /// <param name="method">A string indicating the method of the web request.</param>
        /// <param name="authToken">A string containing the OpSkins authentication token to use for the request.</param>
        /// <param name="data">A string containing data relating to the web request.</param>
        /// <returns>A string containing the result returned by the web request.</returns>
        internal static string ExecuteRequest(string url, string method, string authToken, string data = null)
        {
            HttpWebRequest Request;
            StreamReader Reader = null;
            HttpWebResponse Response;
            string ReturnValue = null;

            // Configure the request
            Request = (HttpWebRequest)HttpWebRequest.Create(url);
            Request.Headers.Add("authorization", "Basic " + authToken);
            Request.Method = method;

            // Add post data
            if (method == "POST" && data != null && data.Length > 0)
            {
                byte[] Data;
                Stream RequestStream;

                Data = System.Text.Encoding.ASCII.GetBytes(data);
                Request.ContentType = "application/x-www-form-urlencoded";
                Request.ContentLength = Data.Length;

                RequestStream = Request.GetRequestStream();
                RequestStream.Write(Data, 0, Data.Length);
                RequestStream.Flush();
                RequestStream.Close();
            }

            try
            {
                Response = (HttpWebResponse)Request.GetResponse();
                try
                {
                    Reader = new StreamReader(Response.GetResponseStream());
                    ReturnValue = Reader.ReadToEnd();
                }
                finally
                {
                    if (Reader != null)
                        Reader.Close();
                }
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    try
                    {
                        string WexData;
                        StreamReader WexReader;

                        WexReader = new StreamReader(wex.Response.GetResponseStream());
                        WexData = WexReader.ReadToEnd();
                        ReturnValue = WexData;
                        // _LogWriter.LogMessage(LogFileEntryPriority.Error, "ExecuteRequest", "WebException error '{0}' generated with response: {1}", wex.Message, ReturnValue);
                    }
                    catch (Exception)
                    {
                        //  _LogWriter.LogMessage(LogFileEntryPriority.Error, "ExecuteRequest", "WebException error '{0}' generated. Unable to process Response object with exception '{1}'", wex.Message, ex.Message);
                    }
                }
                else
                {
                    //_LogWriter.LogMessage(LogFileEntryPriority.Error, "ExecuteRequest", "WebException error '{0}' generated without Response object", wex.Message);
                }
            }
            catch (Exception)
            {
                //_LogWriter.LogMessage(LogFileEntryPriority.Error, "ExecuteRequest", "Exception error '{0}' generated", ex.Message);
            }

            return ReturnValue;
        }

        #endregion
        #endregion
    }
}