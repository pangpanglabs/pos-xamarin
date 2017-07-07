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
        public CatalogPage ()
		{
			InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<Cart>(this, "NewCart", (sender) =>
            {
                string i = "刷新购物车";
            });
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<Cart>(this, "NewCart");
            base.OnDisappearing();
        }



    }
}