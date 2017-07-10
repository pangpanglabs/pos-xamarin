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
                CartItems =new ObservableRangeCollection<CartItem>( c.Items);
            });
            MessagingCenter.Subscribe<CartItem>(this, "DeletecartItem", async (i) => {
                ApiResult<Cart> cartResult = await PosSDK.CallAPI<Cart>("/cart/remove-item", new
                {
                    cartId = i.Id,
                    skuId = i.Sku.Id,
                    quantity = i.Quantity
                });
                if (cartResult.Success == true)
                {
                    CurrentCart = cartResult.Result;
                    MessagingCenter.Send<Cart>(CurrentCart, "ChangeCart");
                }
            });
        }
        Cart currentCart;
        public Cart CurrentCart
        {
            get { return currentCart; }
            set { currentCart = value; OnPropertyChanged(); }
        }
        ObservableRangeCollection<CartItem> cartItems;
        public ObservableRangeCollection<CartItem> CartItems
        {
            get { return cartItems; }
            set { cartItems = value; OnPropertyChanged(); }
        }
        CartItem selectedItem;
        public CartItem SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; OnPropertyChanged(); }
        }
    }
}