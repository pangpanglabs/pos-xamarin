using Pos.ViewModels.Sale.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pos.Views.Sales.Payment
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HandCradPaymentPage : ContentPage
	{
        public HandCradPaymentPage()
        {
            InitializeComponent();
        }
		public HandCradPaymentPage (HandCardPaymentViewModel viewModel):this()
		{
            BindingContext = viewModel;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<object>(this, "HandCardPayComplete", (s) => {
                Navigation.RemovePage(this);
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object>(this, "HandCardPayComplete");
        }
    }
}