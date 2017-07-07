using Pos.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Pos.ViewModels.Sale
{
    public class CartViewModel : BaseViewModel
    {
        public CartViewModel()
        {
            MessagingCenter.Subscribe<Cart>(this, "SendCart", (c) => {
                CurrentCart = c;
            });
            MessagingCenter.Subscribe<CartItem>(this, "DeletecartItem", async(i)=> {
                ApiResult<Cart> cartResult = await PosSDK.CallAPI<Cart>("/cart/remove-item", new
                {
                    cartId = CartId,
                    skuId = i.Sku.Id,
                    quantity = i.Quantity
                });
                if (cartResult.Success==true)
                {
                    CurrentCart = cartResult.Result;
                }
            });
        }
        Cart currentCart;
        public Cart CurrentCart
        {
            get { return currentCart; }
            set { currentCart = value; OnPropertyChanged(); }
        }
        public ObservableRangeCollection<CartItem> CartItems
        {
            get
            {
                return new ObservableRangeCollection<CartItem>(CurrentCart.Items);
            }
        }
        CartItem selectedItem;
        public CartItem SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; OnPropertyChanged(); }
        }
        public string CartId
        {
            get
            {
                return CurrentCart.Id;
            }
        }
    }
}
