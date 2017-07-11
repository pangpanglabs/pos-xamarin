using GalaSoft.MvvmLight.Messaging;
using Pos.Models;
using Pos.ViewModels.Sale;
using Pos.ViewModels.Sale.Payment;
using Pos.Views.Sales.Payment;
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
    public partial class PayPreparePage : ContentPage
    {
        CashPaymentPage _cashPaymentPage;
        HandCradPaymentPage _handCradPaymentPage;

        public PayPreparePage()
        {
            InitializeComponent();
            CreatePaymentButton();
            BindingContext = App.ViewModelLocator.PayPrepareViewModel;
        }

        public PayPreparePage(Cart cart) :this()
        {
            App.ViewModelLocator.PayPrepareViewModel.CurrentCart = cart;
        }

        private void CreatePaymentButton()
        {
            var controlGrid = this.FindByName<Grid>("paymentGrid");
            controlGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });
            int screenWidth = App.ScreenWidth / 4;
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(screenWidth + 10) });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(screenWidth + 10) });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(screenWidth + 10) });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(screenWidth + 10) });

            var cashPayButton = new Button { Text = "Cash", TextColor = Color.White };
            cashPayButton.Clicked += CashPayButton_Clicked;
            cashPayButton.BackgroundColor = Color.FromHex("#42947D");

            var creditCardPayButton = new Button { Text = "HandCard", TextColor = Color.White };
            creditCardPayButton.BackgroundColor = Color.FromHex("#42947D");
            creditCardPayButton.Clicked += CreditCardPayButton_Clicked;

            controlGrid.Children.Add(cashPayButton, 0, 0);
            controlGrid.Children.Add(creditCardPayButton, 1, 0);
        }

        private void CreditCardPayButton_Clicked(object sender, EventArgs e)
        {
            if (!Navigation.NavigationStack.Contains(_handCradPaymentPage))
            {
                HandCardPaymentViewModel viewModel = new HandCardPaymentViewModel();
                viewModel.TotalAmt = App.ViewModelLocator.PayPrepareViewModel.RemainAmt;
                viewModel.ReceivedAmt = App.ViewModelLocator.PayPrepareViewModel.RemainAmt;
                viewModel.CartId = App.ViewModelLocator.PayPrepareViewModel.CartId;
                Navigation.PushAsync(_handCradPaymentPage = new HandCradPaymentPage(viewModel));
            }
        }

        private void CashPayButton_Clicked(object sender, EventArgs e)
        {
            if (!Navigation.NavigationStack.Contains(_cashPaymentPage))
            {
                CashPaymentViewModel viewModel = new CashPaymentViewModel();
                viewModel.TotalAmt = App.ViewModelLocator.PayPrepareViewModel.RemainAmt;
                viewModel.ReceivedAmt = App.ViewModelLocator.PayPrepareViewModel.RemainAmt;
                viewModel.CartId = App.ViewModelLocator.PayPrepareViewModel.CartId;
                Navigation.PushAsync(_cashPaymentPage = new CashPaymentPage(viewModel));
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (!Navigation.NavigationStack.ToList().Where(s => s.GetType().FullName.Contains("PaymentPage")).Any())
            {
                Messenger.Default.Unregister<string>(this, "OrderPayComplete");
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Messenger.Default.Unregister<string>(this, "OrderPayComplete");
            Messenger.Default.Register<string>(this, "OrderPayComplete", m =>
            {
                for (int i = 0; i < Navigation.NavigationStack.Count; i++)
                {
                    if (i > 1)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[i]);
                    }
                }
                App.ViewModelLocator.PayPrepareViewModel.CurrentCart = null;
            });
        }
    }
}