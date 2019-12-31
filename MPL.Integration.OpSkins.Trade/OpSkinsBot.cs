using MPL.Common.Logging;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines a bot for interaction with OpSkins WAX ExpressTrade.
    /// </summary>
    public class OpSkinsBot
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="id">An int indicating the identifier of the bot.</param>
        /// <param name="name">A string containing the name for the bot.</param>
        /// <param name="oatp">A string containing the 2FA code for the bot.</param>
        /// <param name="tradeURL">A string containing the trade URL for the bot.</param>
        /// <param name="apiKey">A string containing the API key for the bot.</param>
        /// <param name="tradeApiUrl">A string containing the trade API URL.</param>
        public OpSkinsBot(int id, string name, string oatp, string tradeURL, string apiKey, string tradeApiUrl)
        {
            ID = id;
            Name = name;
            _2FA = oatp;
            TradeURL = tradeURL;
            _ApiKey = apiKey;

            _OpSkinsAPI = new IntegrationManager(_2FA, _ApiKey, tradeApiUrl);
        }

        #endregion

        #region Declarations
        #region _Members_
        private string _2FA;
        private string _ApiKey;
        private IList<string> _Codes;
        private IList<int> _IgnoredTradeOfferIDs;
        private IntegrationManager _OpSkinsAPI;
        private List<TradeOfferState> _SentTradeOfferStates;
        private Thread _RunBotThread;

        #endregion
        #endregion

        #region Methods
        #region _Private_
        private void BuildSentTradeOfferStates()
        {
            // Build the sent offer state
            _SentTradeOfferStates = new List<TradeOfferState>();
            _SentTradeOfferStates.Add(TradeOfferState.Accepted);
            _SentTradeOfferStates.Add(TradeOfferState.Active);
            _SentTradeOfferStates.Add(TradeOfferState.Expired);
            _SentTradeOfferStates.Add(TradeOfferState.Cancelled);
            _SentTradeOfferStates.Add(TradeOfferState.Declined);
            _SentTradeOfferStates.Add(TradeOfferState.InvalidItems);
        }

        private void ProcessReceivedOffers()
        {
            List<TradeOffer> TradeOffers;

            // Process received offers
            TradeOffers = _OpSkinsAPI.GetTradeOffers(TradeOfferState.Active, TradeOfferTypes.Received);
            if (TradeOffers != null)
            {
                foreach (TradeOffer Item in TradeOffers)
                    // Check to see whether the trade was already processed
                    if (!_IgnoredTradeOfferIDs.Contains(Item.ID))
                    {
                        if (OnProcessReceivedTradeOffer(Item))
                            _IgnoredTradeOfferIDs.Add(Item.ID);
                    }
            }
        }

        private void ProcessSentOffers()
        {
            List<TradeOffer> TradeOffers;

            // Process sent offers
            TradeOffers = _OpSkinsAPI.GetTradeOffers(_SentTradeOfferStates, TradeOfferTypes.Sent);
            if (TradeOffers != null)
            {
                foreach (TradeOffer Item in TradeOffers)
                    // Check to see whether the trade was already processed
                    if (!_IgnoredTradeOfferIDs.Contains(Item.ID))
                    {
                        if (OnProcessSentTradeOffer(Item))
                            _IgnoredTradeOfferIDs.Add(Item.ID);
                    }
            }
        }

        private void RunBot()
        {
            while (IsRunning)
            {
                ProcessReceivedOffers();
                ProcessSentOffers();

                Thread.Sleep(150);
            }
        }

        #endregion
        #region _Protected_
        /// <summary>
        /// Process a received trade offer.
        /// </summary>
        /// <param name="tradeOffer">The TradeOffer to process.</param>
        /// <returns>A bool indicating whether the trade offer has been processed and should be ignored.</returns>
        protected virtual bool OnProcessReceivedTradeOffer(TradeOffer tradeOffer)
        {
            return false;
        }

        /// <summary>
        /// Process a sent trade offer.
        /// </summary>
        /// <param name="tradeOffer">The TradeOffer to process.</param>
        /// <returns>A bool indicating whether the trade offer has been processed and should be ignored.</returns>
        protected virtual bool OnProcessSentTradeOffer(TradeOffer tradeOffer)
        {
            return false;
        }

        #endregion
        #region _Public_
        /// <summary>
        /// Accepts the specified trade offer.
        /// </summary>
        /// <param name="tradeOfferID">An int indicating the identifier of the trade offer to accept.</param>
        /// <returns>A bool indicating success.</returns>
        public bool AcceptTradeOffer(int tradeOfferID)
        {
            return _OpSkinsAPI.AcceptTradeOffer(tradeOfferID);
        }
        /// <summary>
        /// Accepts the specified trade offer.
        /// </summary>
        /// <param name="tradeOffer">A TradeOffer containing the trade offer to accept.</param>
        /// <returns>A bool indicating success.</returns>
        public bool AcceptTradeOffer(TradeOffer tradeOffer)
        {
            return AcceptTradeOffer(tradeOffer.ID);
        }

        /// <summary>
        /// Cancels the specified trade offer.
        /// </summary>
        /// <param name="tradeOfferID">An int indicating the identifier of the OpSkins trade to cancel.</param>
        /// <returns>A bool indicating whether the offer was successfully cancelled.</returns>
        public bool CancelTrade(int tradeOfferID)
        {
            bool ReturnValue = false;

            if (_OpSkinsAPI.CancelTradeOffer(tradeOfferID))
                ReturnValue = true;

            return ReturnValue;
        }
        /// <summary>
        /// Cancels the specified trade offer.
        /// </summary>
        /// <param name="trade">A TradeOffer that is the OpSkins trade offer to cancel.</param>
        /// <returns>A bool indicating whether the offer was successfully cancelled.</returns>
        public bool CancelTrade(TradeOffer tradeOffer)
        {
            return CancelTrade(tradeOffer.ID);
        }

        /// <summary>
        /// Gets the inventory from the bot.
        /// </summary>
        /// <returns>An ItemList of Item containing the inventory.</returns>
        public ItemList GetInventory()
        {
            return _OpSkinsAPI.GetInventory();
        }

        /// <summary>
        /// Gets the specified trade offer.
        /// </summary>
        /// <param name="tradeOfferID">An int indicating the identifier of the OpSkins trade to cancel.</param>
        /// <returns>A TradeOffer that was returned.</returns>
        public TradeOffer GetTradeOffer(int tradeOfferID)
        {
            return _OpSkinsAPI.GetTradeOffer(tradeOfferID);
        }

        /// <summary>
        /// Sends the specified trade offer.
        /// </summary>
        /// <param name="tradeURL">A string containing the trade URL to send the trade to.</param>
        /// <param name="assetsToSend">A List of string containing the identifiers of the assets to send.</param>
        /// <param name="assetsToReceive">A List of string containing the identifiers of the assets to receive.</param>
        /// <param name="message">A string that contains any mesasge to include with the trade.</param>
        /// <param name="tradeID">A string that will be set to the identifier of the trade.</param>
        /// <returns>A bool indicating success.</returns>
        public bool SendTrade(string tradeURL, List<string> assetsToSend, List<string> assetsToReceive, string message, out string tradeID, out string tradeMessage)
        {
            bool ReturnValue = false;

            tradeID = null;
            tradeMessage = null;

            try
            {
                // Attempt to send trade
                if (_OpSkinsAPI.SendTrade(tradeURL, assetsToSend, assetsToReceive, message, out tradeID, out tradeMessage))
                    ReturnValue = true;
            }
            catch (Exception ex)
            {
                LogWriter.LogException(ex);
                ReturnValue = false;
            }

            return ReturnValue;
        }

        /// <summary>
        /// Starts the bot.
        /// </summary>
        public void Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;

                // Objects
                BuildSentTradeOfferStates();
                //_Codes = new SyncList<string>();
                _Codes = new SynchronizedList<string>();
                _IgnoredTradeOfferIDs = new SynchronizedList<int>();
                //_IgnoredTradeOfferIDs = new SyncList<int>();

                // Update the preferences for the bot
                _OpSkinsAPI.UpdateUserPreferences(Name, Troolean.False, Troolean.True, Troolean.NotSet, Troolean.True);

                // Start bot thread
                _RunBotThread = new Thread(new ThreadStart(RunBot));
                _RunBotThread.Name = "OpSkins Bot Run Thread";
                _RunBotThread.Start();
            }
        }

        /// <summary>
        /// Stops the bot.
        /// </summary>
        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
            }
        }

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets the identifier of the bot.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Gets and indication of whether the bot is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Gets the name of the bot.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the trade URL of the bot.
        /// </summary>
        public string TradeURL { get; }

        #endregion
    }
}