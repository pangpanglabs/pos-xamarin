using Pos.Models;
using Pos.ViewModels.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pos.Views.Sales
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CartPage : ContentPage
	{
        PayPreparePage payPreparePage = null;
        Cart cart;
        public CartPage ()
		{
			InitializeComponent ();
		}
        public CartPage(Cart cart) : this()
        {
            this.cart = cart;
            MessagingCenter.Send<Cart>(cart, "SendCart");
            MessagingCenter.Subscribe<Cart>(this,"ChangeCart",(c)=> {
                cart=c;
            });
        }
        public async void OnPayPrepareButtonClick(object sender, EventArgs args)
        {
            if (!Navigation.NavigationStack.Contains(payPreparePage))
            {
               await Navigation.PushAsync(payPreparePage = new PayPreparePage(cart));
            }
        }
        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var cartItem = mi.CommandParameter as CartItem;
            MessagingCenter.Send<CartItem>(cartItem,"DeletecartItem");
        }
    }
}