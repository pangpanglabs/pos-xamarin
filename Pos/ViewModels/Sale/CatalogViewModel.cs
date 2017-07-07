using Pos.Models;
using Pos.Views.Sales;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Pos.ViewModels.Sale
{
    public class CatalogViewModel : BaseViewModel
    {
        public ICommand searchProductCommand { get; }
        public Command LoginCmd { get; }
        public Command GoToCartPage { get; }
        public ObservableRangeCollection<Sku> Contents { get; set; }
        public Cart currentCart { get; set; }
        Sku currentContent { get; set; }

        public string SubTotalAndQty = "1";


        public CatalogViewModel()
        {
            Contents = new ObservableRangeCollection<Sku>();
            searchProductCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoginCmd = new Command(async () => await SignIn());
        }

        public Sku CurrentContent
        {
            get
            {
                return currentContent;
            }
            set
            {
                if (currentCart != null)
                {
                    AddContentToCart((Sku)value);
                }
                else
                {
                    CreateNewCart((Sku)value);
                }
            }
        }

        async Task ExecuteLoadItemsCommand()
        {

            try
            {
                Contents.Clear();
                var result = await PosSDK.CallAPI<ListResult<Sku>>("/catalog/search-skus");
                Contents.ReplaceRange(result.Result.Items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }

        }

        async Task CreateNewCart(Sku item)
        {
            var result = await PosSDK.CallAPI<Cart>("/cart/create-cart");
            currentCart = result.Result;

            AddContentToCart(item);
            currentCart = result.Result;
            MessagingCenter.Send<Cart>(currentCart, "NewCart");
        }

        async Task AddContentToCart(Sku item)
        {
            var result = await PosSDK.CallAPI<Cart>("/cart/add-item", new
            {
                cartId = currentCart.Id,
                skuId = item.Id,
                quantity = 1
            });
            currentCart = result.Result;
            MessagingCenter.Send<Cart>(currentCart, "NewCart");
        }

        async Task RemoveContentFromCart(Sku item)
        {
            var result = await PosSDK.CallAPI<Cart>("/cart/remove-item", new
            {
                cartId = currentCart.Id,
                skuId = item.Id,
                quantity = 1
            });
            currentCart = result.Result;
            MessagingCenter.Send<Cart>(currentCart, "NewCart");
        }

        #region login Area
        async Task SignIn()
        {
            try
            {
                // Log the user in
                await TryLoginAsync();
            }
            finally
            {

            }
        }
        public async Task<bool> TryLoginAsync()
        {
            var result = await PosSDK.CallAPI<Models.Account>("/account/login", new
            {
                tenant = "ELAND",
                username = "salesman",
                password = "1234"
            });
            if (result.Success == true)
            {
                Settings.UserId = result.Result.UserId.ToString();
                Settings.AuthToken = result.Result.Token;
                return true;
            }
            return false;
        }

        #endregion
    }
}