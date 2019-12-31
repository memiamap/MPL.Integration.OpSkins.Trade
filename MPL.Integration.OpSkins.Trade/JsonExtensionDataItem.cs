using System;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines a JSON extension data item.
    /// </summary>
    public class JsonExtensionDataItem
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="name">A string containing the name of the item.</param>
        /// <param name="items">A JsonExtensionDataItemCollection containing the items belonging to this item.</param>
        internal JsonExtensionDataItem(string name, JsonExtensionDataItemCollection items)
            : this(name, null, null)
        {
            Items = items;
        }

        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="name">A string containing the name of the item.</param>
        /// <param name="dataType">A Type indicating the data type of the item.</param>
        /// <param name="value">An object containing the value of the item.</param>
        internal JsonExtensionDataItem(string name, Type dataType, object value)
        {
            Name = name;
            DataType = dataType;
            Value = value;

            if (DataType == null)
                IsClassType = true;
            else
                IsClassType = false;
        }

        #endregion

        #region Methods
        #region _Public_
        public override string ToString()
        {
            string ReturnValue;

            if (IsClassType)
                ReturnValue = string.Format("{0} class with {1} item(s)", Name, Items.Count);
            else
                ReturnValue = string.Format("{0} ({1}) = {2}", Name, DataType, Value);

            return ReturnValue;
        }
        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets the data type fo the item.
        /// </summary>
        public Type DataType { get; }

        /// <summary>
        /// Gets an indication of whether this is a class type.
        /// </summary>
        public bool IsClassType { get; }

        /// <summary>
        /// Gets any items that belong to this item.
        /// </summary>
        public JsonExtensionDataItemCollection Items { get; }

        /// <summary>
        /// Gets the name of the item.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the value of the item.
        /// </summary>
        public object Value { get; }

        #endregion
    }
}