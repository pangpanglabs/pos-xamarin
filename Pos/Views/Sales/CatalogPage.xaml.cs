using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Pos.Models;
using Pos.ViewModels.Sale;

namespace Pos.Views.Sales
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CatalogPage : ContentPage
	{
        public Cart newCart { get; set; }
        public CatalogPage ()
		{
			InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<Cart>(this, "NewCart", (sender) =>
            {
                newCart = sender;
            });
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<Cart>(this, "NewCart");
            base.OnDisappearing();
        }

        public void GoToCartPage(object sender,EventArgs e) {
            if (newCart == null) {
                return;
            }
            Navigation.PushAsync(new CartPage(newCart));
        }
    }
}