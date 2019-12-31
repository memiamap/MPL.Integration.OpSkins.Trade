using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Serialization;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that provides helper functions.
    /// </summary>
    internal static class HelperFunctions
    {
        #region Methods
        #region _Internal_
        /// <summary>
        /// Converts extension data from a deserialised JSON object into usable data.
        /// </summary>
        /// <param name="data">An ExtensionDataObject containing the extension data.</param>
        /// <returns>A JsonExtensionDataItemCollection containing the results.</returns>
        internal static JsonExtensionDataItemCollection ConvertExtensionData(ExtensionDataObject data)
        {
            JsonExtensionDataItemCollection ReturnValue;

            // Defaults
            ReturnValue = new JsonExtensionDataItemCollection();
            ReturnValue = UnpackExtensionDataMembers(data);

            return ReturnValue;
        }

        /// <summary>
        /// Generate an authentication token for the specified API key.
        /// </summary>
        /// <param name="key">A string containing the API key.</param>
        /// <returns>A string containing the authentication token.</returns>
        internal static string GenerateAuthenticationToken(string key)
        {
            byte[] Data;
            string ReturnValue;

            Data = System.Text.Encoding.ASCII.GetBytes(key + ":");
            ReturnValue = Convert.ToBase64String(Data);

            return ReturnValue;
        }

        /// <summary>
        /// Try to parse the specified trade URL into a UID and trade token.
        /// </summary>
        /// <param name="tradeURL">A string containing the trade URL to try to parse.</param>
        /// <param name="uid">A string that will be set to the UID of the user.</param>
        /// <param name="token">A string that will be set to the trade token of the user.</param>
        /// <returns>A bool indicating success.</returns>
        internal static bool TryParseTradeURL(string tradeURL, out string uid, out string token)
        {
            bool ReturnValue = false;

            // Defaults
            uid = null;
            token = null;

            if (tradeURL != null)
            {
                bool IsValid = false;
                string[] Parts;
                string Token = null;
                string UID = null;

                if (tradeURL.ToLower().StartsWith("https://trade.opskins.com/t/") ||
                    tradeURL.ToLower().StartsWith("https://trade.wax.io/t/"))
                {
                    //https://trade.opskins.com/t/2417322/HcLbROAG
                    UID = tradeURL.Replace("https://trade.opskins.com/t/", "").Replace("https://trade.wax.io/t/", "");
                    Parts = UID.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    if (Parts.Length == 2)
                    {
                        UID = Parts[0];
                        Token = Parts[1];
                        IsValid = true;
                    }
                }
                else if (tradeURL.ToLower().StartsWith("https://trade.opskins.com/trade/userid/") ||
                         tradeURL.ToLower().StartsWith("https://trade.wax.io/trade/userid/"))
                {
                    //https://trade.opskins.com/trade/userid/2417322/token/HcLbROAG
                    UID = tradeURL.Replace("https://trade.opskins.com/trade/userid/", "").Replace("https://trade.wax.io/trade/userid/", "");
                    Parts = UID.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    if (Parts.Length == 3)
                    {
                        UID = Parts[0];
                        Token = Parts[2];
                        IsValid = true;
                    }
                }

                if (IsValid)
                {
                    ReturnValue = true;
                    token = Token;
                    uid = UID;
                }
            }

            return ReturnValue;
        }

        #endregion
        #region _Private_
        private static PropertyInfo FindProperty(object obj, string name)
        {
            return FindProperty(obj.GetType(), name);
        }
        private static PropertyInfo FindProperty(Type type, string name)
        {
            PropertyInfo ReturnValue;

            ReturnValue = type.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            return ReturnValue;
        }

        private static JsonExtensionDataItem UnpackExtensionDataMember(object Item)
        {
            PropertyInfo NameProperty;
            JsonExtensionDataItem ReturnValue = null;
            PropertyInfo ValueProperty;

            NameProperty = FindProperty(Item, "Name");
            ValueProperty = FindProperty(Item, "Value");
            if (NameProperty != null && ValueProperty != null)
            {
                string Name;
                object Value;

                Name = NameProperty.GetValue(Item).ToString();
                Value = ValueProperty.GetValue(Item);
                if (Value != null)
                {
                    switch (Value.GetType().Name)
                    {
                        case "DataNode`1":
                            PropertyInfo ItemValueProperty;
                            PropertyInfo TypeProperty;

                            TypeProperty = FindProperty(Value, "DataType");
                            ItemValueProperty = FindProperty(Value, "Value");
                            if (TypeProperty != null && ItemValueProperty != null)
                            {
                                Type DataType;
                                object ItemValue;

                                DataType = (Type)TypeProperty.GetValue(Value);
                                ItemValue = ItemValueProperty.GetValue(Value);

                                ReturnValue = new JsonExtensionDataItem(Name, DataType, ItemValue);
                            }
                            break;

                        case "ClassDataNode":
                            JsonExtensionDataItemCollection NewItems;

                            NewItems = UnpackExtensionDataMembers(Value);
                            ReturnValue = new JsonExtensionDataItem(Name, NewItems);
                            break;

                        case "CollectionDataNode":
                            break;

                        default:
                            Console.WriteLine("WTF");
                            break;
                    }
                }
            }

            return ReturnValue;
        }

        private static JsonExtensionDataItemCollection UnpackExtensionDataMembers(object item)
        {
            PropertyInfo MemberProperty;
            JsonExtensionDataItemCollection ReturnValue;

            // Defaults
            ReturnValue = new JsonExtensionDataItemCollection();

            // Get member data
            MemberProperty = FindProperty(item, "Members");
            if (MemberProperty != null)
            {
                object MembersValue;

                MembersValue = MemberProperty.GetValue(item);
                if (MembersValue != null && MembersValue is IList)
                {
                    IList MemberList;

                    // Go through each item
                    MemberList = (IList)MembersValue;
                    foreach (object Item in MemberList)
                    {
                        JsonExtensionDataItem UnpackedItem;

                        UnpackedItem = UnpackExtensionDataMember(Item);
                        if (UnpackedItem != null)
                            ReturnValue.Add(UnpackedItem);
                    }
                }
            }

            return ReturnValue;
        }

        #endregion
        #endregion
    }
}