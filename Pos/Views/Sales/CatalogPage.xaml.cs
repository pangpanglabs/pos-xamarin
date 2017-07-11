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
        CartPage CartPage = null;
        public CatalogPage()
        {
            InitializeComponent();
            BindingContext = App.ViewModelLocator.CatalogViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        public void GoToCartPage(object sender, EventArgs e)
        {

            if (App.ViewModelLocator.CatalogViewModel.CurrentCart == null || App.ViewModelLocator.CatalogViewModel.CurrentCart.Quantity.Equals(0))
            {
                return;
            }
            if (!Navigation.NavigationStack.Contains(CartPage))
            {
                Navigation.PushAsync(new CartPage(App.ViewModelLocator.CatalogViewModel.CurrentCart));
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            ((ListView)sender).SelectedItem = null;
        }
    }
}