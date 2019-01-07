using System;
using System.Runtime.Serialization;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines a trade offer.
    /// </summary>
    [DataContract()]
    public class TradeOffer
    {
        #region Declarations
        #region _Members_
        private int _ID;
        private bool _IsGift;
        private string _Message;
        private TradePerson _Recipient;
        private TradePerson _Sender;
        private bool _SentByYou;
        private TradeOfferState _State;
        private string _StateName;
        private int _TimeCreated;
        private int _TimeExpires;
        private int _TimeUpdated;

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets the identifier of the trade.
        /// </summary>
        [DataMember(Name = "id")]
        public int ID
        {
            get { return _ID; }
            private set { _ID = value; }
        }

        /// <summary>
        /// Gets an indication of whether this trade is a gift.
        /// </summary>
        [DataMember(Name = "is_gift")]
        public bool IsGift
        {
            get { return _IsGift; }
            private set { _IsGift = value; }
        }

        /// <summary>
        /// Gets the trade message.
        /// </summary>
        [DataMember(Name = "message")]
        public string Message
        {
            get { return _Message; }
            private set { _Message = value; }
        }

        /// <summary>
        /// Gets the trade recipient.
        /// </summary>
        [DataMember(Name = "recipient")]
        public TradePerson Recipient
        {
            get { return _Recipient; }
            private set { _Recipient = value; }
        }

        /// <summary>
        /// Gets the trade sender.
        /// </summary>
        [DataMember(Name = "sender")]
        public TradePerson Sender
        {
            get { return _Sender; }
            private set { _Sender = value; }
        }

        /// <summary>
        /// Gets an indication of whether this trade was sent by you.
        /// </summary>
        [DataMember(Name = "sent_by_you")]
        public bool SentByYou
        {
            get { return _SentByYou; }
            private set { _SentByYou = value; }
        }

        /// <summary>
        /// Gets the status code of the trade.
        /// </summary>
        [DataMember(Name = "state")]
        public TradeOfferState State
        {
            get { return _State; }
            private set { _State = value; }
        }

        /// <summary>
        /// Gets the name of the trade state.
        /// </summary>
        [DataMember(Name = "state_name")]
        public string StateName
        {
            get { return _StateName; }
            private set { _StateName = value; }
        }

        /// <summary>
        /// Gets the timestamp of the trade creation.
        /// </summary>
        [DataMember(Name = "time_created")]
        public int TimeCreated
        {
            get { return _TimeCreated; }
            private set { _TimeCreated = value; }
        }

        /// <summary>
        /// Gets the timestamp of the trade expiry.
        /// </summary>
        [DataMember(Name = "time_expires")]
        public int TimeExpires
        {
            get { return _TimeExpires; }
            private set { _TimeExpires = value; }
        }

        /// <summary>
        /// Gets the timestamp of the trade's last update.
        /// </summary>
        [DataMember(Name = "time_updated")]
        public int TimeUpdated
        {
            get { return _TimeUpdated; }
            private set { _TimeUpdated = value; }
        }

        #endregion
    }
}