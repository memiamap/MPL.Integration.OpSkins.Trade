using MPL.Common.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Threading;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines an integration manager for the OpSkins Trade API.
    /// </summary>
    public class IntegrationManager
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="oatp">A string containing the two-factor authentication key to use.</param>
        /// <param name="apiKey">A string containing the API key to use.</param>
        /// <param name="baseUrl">A string containing the base URL of the trade API.</param>
        public IntegrationManager(string oatp, string apiKey, string baseUrl)
        {
            _2FA = oatp;
            _ApiKey = apiKey;
            _BaseUrl = baseUrl;
            
            GenerateAuthToken();
        }

        #endregion

        #region Declarations
        #region _Members_
        private readonly string _2FA;
        private readonly string _ApiKey;
        private string _AuthToken;
        private readonly string _BaseUrl;

        #endregion
        #endregion

        #region Methods
        #region _Private_
        private string BuildItemList(List<string> items)
        {
            string ReturnValue = string.Empty;

            foreach (string Item in items)
                ReturnValue += string.Format("{0},", Item);
            ReturnValue = ReturnValue.TrimEnd(',');

            return ReturnValue;
        }

        private string BuildUrl(string endpoint)
        {
            return $"{_BaseUrl}/{endpoint}".Replace($"{_BaseUrl}//", $"{_BaseUrl}/");
        }

        private string GenerateAuthToken()
        {
            string ReturnValue;

            ReturnValue = HelperFunctions.GenerateAuthenticationToken(_ApiKey);
            _AuthToken = ReturnValue;

            return ReturnValue;
        }

        #endregion
        #region _Public_
        /// <summary>
        /// Accepts the specified trade offer.
        /// </summary>
        /// <param name="tradeOfferID">An int containing the identifier of the trade offer.</param>
        /// <returns>A bool indicating success.</returns>
        public bool AcceptTradeOffer(int tradeOfferID)
        {
            bool ReturnValue = false;

            try
            {
                string Code;
                NameValueCollection PostData;
                ApiResponse<AcceptTradeResponse> Result;

                Code = Authenticator.Compute2FA(_2FA);

                // Build the post data
                PostData = new NameValueCollection
                {
                    { "twofactor_code", Code },
                    { "offer_id", tradeOfferID.ToString() }
                };

                // Execute the request
                Result = OpSkinsWebRequest<AcceptTradeResponse>.ExecutePost(BuildUrl("ITrade/AcceptOffer/v1/"), _AuthToken, PostData);
                if (Result != null && Result.Response.TradeOffer != null)
                {
                    ReturnValue = true;
                }
                else
                    throw new InvalidOperationException("No result data was received");
            }
            catch (Exception ex)
            {
                throw new Exception("Trade offer could not be accepted", ex);
            }

            return ReturnValue;
        }

        /// <summary>
        /// Cancels the specified trade offer.
        /// </summary>
        /// <param name="tradeOfferID">An int indicating the identifier of the trade offer.</param>
        /// <returns>A bool indicating success.</returns>
        public bool CancelTradeOffer(int tradeOfferID)
        {
            bool ReturnValue = false;

            try
            {
                NameValueCollection PostData;
                ApiResponse<CancelOfferResponse> Result;

                // Build the post data
                PostData = new NameValueCollection
                {
                    { "offer_id", tradeOfferID.ToString() }
                };

                // Execute the request
                Result = OpSkinsWebRequest<CancelOfferResponse>.ExecutePost(BuildUrl("ITrade/CancelOffer/v1/"), _AuthToken, PostData);
                if (Result != null && Result.Response != null && Result.Response.TradeOffer != null)
                {
                    TradeOffer TheOffer;

                    TheOffer = Result.Response.TradeOffer;
                    if (TheOffer.ID == tradeOfferID)
                    {
                        if (TheOffer.State == TradeOfferState.Cancelled || TheOffer.State == TradeOfferState.Declined)
                            ReturnValue = true;
                    }
                    else
                        throw new InvalidDataException(string.Format("The API returned an invalid trade offer identifier ({0}) during cancellation", TheOffer.ID));
                }
                else
                {
                    if (Result.Message.Contains("cancel an inactive"))
                    {

                    }
                    else
                    {
                        throw new InvalidOperationException("No result data was received");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Trade offer could not be cancelled", ex);
            }

            return ReturnValue;
        }

        /// <summary>
        /// Gets the specified trade offer.
        /// </summary>
        /// <param name="tradeID">An int indicating the identifier of the trade offer.</param>
        public TradeOffer GetTradeOffer(int tradeOfferID)
        {
            int Retries = 0;
            TradeOffer ReturnValue = null;

            while (Retries++ < 3)
            {
                try
                {
                    NameValueCollection RequestData;
                    ApiResponse<GetOfferResponse> Result;

                    // Build the post data
                    RequestData = new NameValueCollection
                {
                    { "offer_id", tradeOfferID.ToString() }
                };

                    // Execute the request
                    Result = OpSkinsWebRequest<GetOfferResponse>.ExecuteGet(BuildUrl("/ITrade/GetOffer/v1/"), _AuthToken, RequestData);
                    if (Result != null && Result.Response != null)
                    {
                        ReturnValue = Result.Response.TradeOffer;
                        Retries = 5;
                    }
                    else
                        throw new InvalidOperationException("No result data was received");
                }
                catch (Exception ex)
                {
                    LogWriter.LogException(ex);
                }

                if (Retries < 5)
                    Thread.Sleep(5);
            }

            return ReturnValue;
        }

        /// <summary>
        /// Gets a list of trade offers from the specified data.
        /// </summary>
        /// <param name="data">A string containing the data to get trade offers from.</param>
        public List<TradeOffer> GetTradeOffers(string data)
        {
            List<TradeOffer> ReturnValue = null;
            ApiResponse<GetOffersResponse> Result;

            Result = OpSkinsWebRequest<GetOffersResponse>.ProcessResponse(data);
            if (Result != null && Result.Response != null)
                ReturnValue = Result.Response.TradeOffers;

            return ReturnValue;
        }
        /// <summary>
        /// Gets a list of trade offers for the specified trade offer state.
        /// </summary>
        /// <param name="states">A TradeOfferState containing the state to get trade offers for.</param>
        /// <param name="state">A TradeOfferTypes indicating the types of trade offer to get.</param>
        public List<TradeOffer> GetTradeOffers(TradeOfferState state, TradeOfferTypes tradeOfferTypes)
        {
            List<TradeOfferState> States;

            States = new List<TradeOfferState>
            {
                state
            };

            return GetTradeOffers(States, tradeOfferTypes);
        }
        /// <summary>
        /// Gets a list of trade offers for the specified trade offer states.
        /// </summary>
        /// <param name="states">A List of TradeOfferState containing the states to get trade offers for.</param>
        /// <param name="state">A TradeOfferTypes indicating the types of trade offer to get.</param>
        public List<TradeOffer> GetTradeOffers(List<TradeOfferState> states, TradeOfferTypes tradeOfferTypes)
        {
            List<TradeOffer> ReturnValue = null;
            int Retries = 0;

            while (Retries++ < 3)
            {
                try
                {
                    NameValueCollection RequestData;
                    ApiResponse<GetOffersResponse> Result;

                    // Build the post data
                    RequestData = new NameValueCollection
                {
                    { "twofactor_code", Authenticator.Compute2FA(_2FA) }
                };
                    if (states != null && states.Count > 0)
                    {
                        string StateList = string.Empty;

                        foreach (TradeOfferState State in states)
                            StateList += string.Format("{0},", (int)State);
                        StateList = StateList.TrimEnd(new char[] { ',' });
                        RequestData.Add("state", StateList);
                    }

                    // Add the trade offer type
                    switch (tradeOfferTypes)
                    {
                        case TradeOfferTypes.Received:
                            RequestData.Add("type", "received");
                            break;

                        case TradeOfferTypes.Sent:
                            RequestData.Add("type", "sent");
                            break;
                    }

                    // Execute the request
                    Result = OpSkinsWebRequest<GetOffersResponse>.ExecuteGet(BuildUrl("ITrade/GetOffers/v1/"), _AuthToken, RequestData);
                    if (Result != null && Result.Response != null)
                        ReturnValue = Result.Response.TradeOffers;
                    else
                        throw new InvalidOperationException("No result data was received");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not get trade offers: {0}", ex);
                }
            }

            return ReturnValue;
        }

        /// <summary>
        /// Gets the inventory of the bot.
        /// </summary>
        /// <returns>An ItemList containing the inventory.</returns>
        public ItemList GetInventory()
        {
            ItemList ReturnValue = null;

            try
            {
                NameValueCollection RequestData;
                ApiResponse<GetInventoryResponse> Result;

                // Build the post data
                RequestData = new NameValueCollection
                {
                    { "app_id", "1" }
                };

                // Execute the request
                Result = OpSkinsWebRequest<GetInventoryResponse>.ExecuteGet(BuildUrl("IUser/GetInventory/v1/"), _AuthToken, RequestData);
                if (Result != null && Result.Response != null)
                    ReturnValue = Result.Response.Items;
                else
                    throw new InvalidOperationException("No result data was received");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Could not get trade offers", ex);
            }

            return ReturnValue;
        }
        /// <summary>
        /// Gets the inventory of the specifeid user.
        /// </summary>
        /// <param name="userID">An int containing the user identifier.</param>
        /// <returns>An ItemList containing the inventory.</returns>
        public ItemList GetInventory(int userID)
        {
            ItemList ReturnValue = null;

            try
            {
                NameValueCollection RequestData;
                ApiResponse<GetInventoryResponse> Result;

                // Build the post data
                RequestData = new NameValueCollection
                {
                    { "uid", userID.ToString() }
                };

                // Execute the request
                Result = OpSkinsWebRequest<GetInventoryResponse>.ExecuteGet(BuildUrl("/ITrade/GetUserInventory/v1/"), _AuthToken, RequestData);
                if (Result != null && Result.Response != null)
                    ReturnValue = Result.Response.Items;
                else
                    throw new InvalidOperationException("No result data was received");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Could not get trade offers", ex);
            }

            return ReturnValue;
        }

        /// <summary>
        /// Sends a trade with the specified assets to the specified URL.
        /// </summary>
        /// <param name="tradeURL">A string containing the trade URL to send the trade to.</param>
        /// <param name="itemsToSend">A List of string containing the identifiers of the assets to send.</param>
        /// <param name="itemsToReceive">A List of string containing the identifiers of the assets to receive.</param>
        /// <param name="tradeID">A string that will be set to the identifier of the trade.</param>
        /// <param name="tradeMessage">A string that will be set to any message associated with the trade.</param>
        /// <returns>A bool indicating whether the request was successful.</returns>
        public bool SendTrade(string tradeURL, List<string> itemsToSend, List<string> itemsToReceive, out string tradeID, out string tradeMessage)
        {
            return SendTrade(tradeURL, itemsToSend, itemsToReceive, null, out tradeID, out tradeMessage);
        }
        /// <summary>
        /// Sends a trade with the specified assets to the specified URL, displaying the specified message.
        /// </summary>
        /// <param name="tradeURL">A string containing the trade URL to send the trade to.</param>
        /// <param name="itemsToSend">A List of string containing the identifiers of the assets to send.</param>
        /// <param name="itemsToReceive">A List of string containing the identifiers of the assets to receive.</param>
        /// <param name="message">A string containing the message to include with the trade.</param>
        /// <param name="tradeID">A string that will be set to the identifier of the trade.</param>
        /// <param name="tradeMessage">A string that will be set to any message associated with the trade.</param>
        /// <returns>A bool indicating whether the request was successful.</returns>
        public bool SendTrade(string tradeURL, List<string> itemsToSend, List<string> itemsToReceive, string message, out string tradeID, out string tradeMessage)
        {
            bool ReturnValue = false;

            // Defaults
            tradeID = null;
            tradeMessage = null;

            try
            {
                // Check the trade URL is valid
                if (HelperFunctions.TryParseTradeURL(tradeURL, out string UID, out string Token))
                {
                    // Check that there are items
                    if ((itemsToSend != null && itemsToSend.Count > 0) ||
                        (itemsToReceive != null && itemsToReceive.Count > 0))
                    {
                        int Retries = 0;

                        while (Retries++ < 5)
                        {
                            string Code;
                            NameValueCollection PostData;
                            ApiResponse<SendOfferResponse> Result;

                            Code = Authenticator.Compute2FA(_2FA);

                            // Build the post data
                            PostData = new NameValueCollection
                            {
                                { "twofactor_code", Code },
                                { "uid", UID },
                                { "token", Token }
                            };
                            //PostData.Add("items", BuildItemList(items));
                            if (itemsToReceive != null && itemsToReceive.Count > 0)
                                PostData.Add("items_to_receive", BuildItemList(itemsToReceive));
                            if (itemsToSend != null && itemsToSend.Count > 0)
                                PostData.Add("items_to_send", BuildItemList(itemsToSend));
                            if (message != null)
                                PostData.Add("message", message);

                            // Execute the request
                            Result = OpSkinsWebRequest<SendOfferResponse>.ExecutePost(BuildUrl("/ITrade/SendOffer/v1/"), _AuthToken, PostData);
                            if (Result != null)
                            {
                                if (Result.Response != null && Result.Response.TradeOffer != null)
                                {
                                    TradeOffer TheOffer;

                                    TheOffer = Result.Response.TradeOffer;
                                    switch (TheOffer.State)
                                    {
                                        case TradeOfferState.Active:
                                            tradeID = TheOffer.ID.ToString();
                                            ReturnValue = true;
                                            break;

                                        default:
                                            tradeMessage = TheOffer.StateName;
                                            break;
                                    }

                                    Retries = 10;
                                }
                                else if (Result.Message != null)
                                {
                                    if (Result.Message == "Two-factor code already used.")
                                    {
                                        tradeMessage = "The bot reported an error whilst sending the trade";
                                        throw new InvalidOperationException("The specified 2FA code has already been used");
                                    }
                                    else if (Result.Message.Contains("that do not belong to you"))
                                    {
                                        tradeMessage = "The chosen items are not valid for this trade";
                                        throw new InvalidOperationException("The specified items do not belong to the user");
                                    }
                                    else
                                    {
                                        tradeMessage = Result.Message;
                                        throw new InvalidOperationException(string.Format("The Send Trade call returned: {0}", Result.Message));
                                    }
                                }
                            }
                            else
                                throw new InvalidOperationException("No result data was received");
                        }
                    }
                    else
                        throw new ArgumentException("No items were specified for the trade", "itemsToSend");
                }
                else
                {
                    tradeMessage = "The user's trade URL appears to be invalid";
                    throw new ArgumentException("The specified trade URL is invalid", "tradeURL");
                }
            }
            catch (Exception ex)
            {
                LogWriter.LogException(ex);
                throw new InvalidOperationException("Could not send trade offer", ex);
            }

            return ReturnValue;
        }

        /// <summary>
        /// Updates user preferences to set preferences of a user.
        /// </summary>
        /// <param name="displayName">A string that will set the display name of the user.</param>
        /// <param name="inventoryIsPrivate">A Troolean that will indicate whether the inventory should be marked private.</param>
        /// <param name="Allow2faReuse">A Troolean that will indicate whether 2FA codes can be resued by the user.</param>
        /// <param name="autoAcceptGifts">A Troolean that will indicate whether to automatically accept gift trades.</param>
        /// <param name="anonymousTransactions">A Troolean that will indicate whether to hide the account name from the public transaction log.</param>
        /// <returns>A User that is the updated user.</returns>
        public User UpdateUserPreferences(string displayName = null, Troolean inventoryIsPrivate = Troolean.NotSet, Troolean Allow2faReuse = Troolean.NotSet, Troolean autoAcceptGifts = Troolean.NotSet, Troolean anonymousTransactions = Troolean.NotSet)
        {
            User ReturnValue = null;

            try
            {

                // Check for at least one value to change
                if ((displayName != null && displayName.Length > 0) || inventoryIsPrivate != Troolean.NotSet || Allow2faReuse != Troolean.NotSet)
                {
                    NameValueCollection PostData;
                    ApiResponse<UserResponse> Result;

                    // Build the post data
                    PostData = new NameValueCollection();
                    if (displayName != null && displayName.Length > 0)
                        PostData.Add("display_name", displayName);
                    if (inventoryIsPrivate == Troolean.False)
                        PostData.Add("inventory_is_private", "false");
                    else if (inventoryIsPrivate == Troolean.True)
                        PostData.Add("inventory_is_private", "true");
                    if (Allow2faReuse == Troolean.False)
                        PostData.Add("allow_twofactor_code_reuse", "false");
                    else if (Allow2faReuse == Troolean.True)
                        PostData.Add("allow_twofactor_code_reuse", "true");
                    if (autoAcceptGifts == Troolean.False)
                        PostData.Add("auto_accept_gift_trades", "false");
                    else if (autoAcceptGifts == Troolean.True)
                        PostData.Add("auto_accept_gift_trades", "false");
                    if (anonymousTransactions == Troolean.False)
                        PostData.Add("anonymous_transactions", "false");
                    else if (anonymousTransactions == Troolean.True)
                        PostData.Add("anonymous_transactions", "true");

                    // Check at least one value was set
                    if (PostData.Count > 0)
                    {
                        // Execute the request
                        Result = OpSkinsWebRequest<UserResponse>.ExecutePost(BuildUrl("/IUser/UpdateProfile/v1/"), _AuthToken, PostData);
                        if (Result != null && Result.Response != null && Result.Response.User != null)
                        {
                            ReturnValue = Result.Response.User;
                        }
                        else
                            throw new InvalidOperationException("No result data was received");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Could not update user preferences", ex);
            }

            return ReturnValue;
        }

        #endregion
        #endregion
    }
}
