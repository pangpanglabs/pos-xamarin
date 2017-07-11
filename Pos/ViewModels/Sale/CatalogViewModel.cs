using GalaSoft.MvvmLight.Messaging;
using Pos.Models;
using Pos.Views.Sales;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using GalaSoft.MvvmLight;

namespace Pos.ViewModels.Sale
{
    public class CatalogViewModel : BaseViewModel
    {
        public ICommand searchProductCommand { get; }
        public Command LoginCmd { get; }
        Cart currentCart;
        public Cart CurrentCart {
            get {
                return currentCart;
            }
            set {
                Set(() => CurrentCart, ref currentCart, value);
                RaisePropertyChanged(() => SubTotalAndQty);
            }
        }
        Sku currentContent;

        ObservableRangeCollection<Sku> contents;
        public ObservableRangeCollection<Sku> Contents
        {
            get { return contents; }
            set
            {
                Set(() => Contents, ref contents, value);
            }
        }

        string searchText;
        public string SearchText {
            get { return searchText; }
            set {
                Set(() => SearchText, ref searchText, value);
            }
        }

        string subTotalAndQty ;
        public string SubTotalAndQty {
            get
            {
                if (currentCart == null)
                {
                    return "  总金额： " + Convert.ToDecimal(0).ToString() + "  数量： " + "0";
                }
                return currentCart.SalePrice.ToString() + "  数量： " + currentCart.Quantity.ToString();
            }
        }

        public CatalogViewModel()
        {
            Contents = new ObservableRangeCollection<Sku>();
            searchProductCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoginCmd = new Command(async () => await SignIn());

            Messenger.Default.Register<string>(this, "OrderPayComplete", m =>
            {
                Contents = new ObservableRangeCollection<Sku>(); 
                currentCart = null;
                SearchText = "";
                RaisePropertyChanged(() => Contents);
                RaisePropertyChanged(() => SubTotalAndQty);
            });
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
                //RaisePropertyChanged(() => CurrentCart);
                //RaisePropertyChanged(() => SubTotalAndQty);
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
            MessagingCenter.Send<Cart>(currentCart, "NewCart");

            await AddContentToCart(item);
        }

        async Task AddContentToCart(Sku item)
        {
            try
            {
                var result = await PosSDK.CallAPI<Cart>("/cart/add-item", new
                {
                    cartId = currentCart.Id,
                    skuId = item.Id,
                    quantity = 1
                });
                currentCart = result.Result;
                RaisePropertyChanged(() => CurrentCart);
                RaisePropertyChanged(() => SubTotalAndQty);
                MessagingCenter.Send<Cart>(currentCart, "NewCart");
            }
            catch (Exception e) {
                throw (e);
            }
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