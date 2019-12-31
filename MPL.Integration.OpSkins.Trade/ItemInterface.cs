using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines the OpSkins Trade API IItem interface.
    /// </summary>
    public class ItemInterface : OpSkinsInterfaceBase
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public ItemInterface()
            : base("IItem")
        {

        }

        #endregion

        #region Declarations
        #region _Members_
        string _AuthToken;

        #endregion
        #endregion

        #region Methods
        #region _Private_
        private string GenerateAuthToken()
        {
            string ReturnValue;
            //5BXB 2XI7 AKS5 EXCA
            //b82b6e6dcddf0da3bf111e6682d43a

            ReturnValue = HelperFunctions.GenerateAuthenticationToken("b82b6e6dcddf0da3bf111e6682d43a");
            _AuthToken = ReturnValue;

            return ReturnValue;
        }

        #endregion
        #region _Public_
        /// <summary>
        /// Gets all items for the specified application identifier.
        /// </summary>
        /// <param name="appID">An int indicating the application to get all items for.</param>
        /// <param name="pageNumber">An int indicating the current page to get items from.</param>
        /// <param name="perPage">An int indicating the size of each page that should be returned.</param>
        /// <param name="skuFilter">A List of int indicating the SKUs of items to return.</param>
        /// <param name="nameFilter">A string containing the name to filter items by.</param>
        /// <param name="showAllItems">A bool that indicates whether to return all items (some items are excluded by default).</param>
        /// <param name="sortOrder">An ItemSorts specifying the sort order of the items in the result set.</param>
        public void GetAllItems(int appID, int pageNumber = 1, int perPage = 100, List<int> skuFilter = null, string nameFilter = null, bool showAllItems = false, ItemSorts sortOrder = ItemSorts.Undefined)
        {
            ItemList ReturnValue = null;

            // Validate parameters
            if (appID < 1)
                throw new ArgumentException("The specified application identifier is invalid", "appID");
            if (pageNumber < 1)
                throw new ArgumentException("The specified page number is invalid", "pageNumber");
            if (perPage < 1 || perPage > 100)
                throw new ArgumentException("The specified results per page is invalid", "perPage");

            try
            {
                NameValueCollection RequestData;
                ApiResponse<GetInventoryResponse> Result;

                // Build the post data
                RequestData = new NameValueCollection
                {
                    { "app_id", appID.ToString() },
                    { "page", pageNumber.ToString() },
                    { "per_page", perPage.ToString() }
                };
                if (skuFilter != null && skuFilter.Count > 0)
                {
                    string SkuList = string.Empty;

                    foreach (int Item in skuFilter)
                        SkuList += string.Format("{0},", Item);
                    SkuList = SkuList.Trim(',');
                    RequestData.Add("sku", SkuList);
                }
                if (sortOrder != ItemSorts.Undefined)
                    RequestData.Add("sort", ((int)sortOrder).ToString());
                if (showAllItems)
                    RequestData.Add("no_exclusions", "1");
                if (nameFilter != null && nameFilter.Length > 0)
                    RequestData.Add("name", nameFilter);

                GenerateAuthToken();
                // Execute the request
                Result = OpSkinsWebRequest<GetInventoryResponse>.ExecuteGet("https://api-trade.opskins.com/IItem/GetAllItems/v1/", _AuthToken, RequestData);
                if (Result != null && Result.Response != null)
                    ReturnValue = Result.Response.Items;
                else
                    throw new InvalidOperationException("No result data was received");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Could not get trade offers", ex);
            }

            // return ReturnValue;
        }

        #endregion
        #endregion
    }
}