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
        PayPrepareViewModel _viewModel;

        public PayPreparePage()
        {
            InitializeComponent();
            CreatePaymentButton();
        }

        public PayPreparePage(PayPrepareViewModel viewModel):this()
        {
            BindingContext = _viewModel = viewModel;
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
                viewModel.TotalAmt = _viewModel.RemainAmt;
                viewModel.ReceivedAmt = _viewModel.RemainAmt;
                viewModel.CartId = _viewModel.CartId;
                Navigation.PushAsync(_handCradPaymentPage = new HandCradPaymentPage(viewModel));
            }
        }

        private void CashPayButton_Clicked(object sender, EventArgs e)
        {
            if (!Navigation.NavigationStack.Contains(_cashPaymentPage))
            {
                CashPaymentViewModel viewModel = new CashPaymentViewModel();
                viewModel.TotalAmt = _viewModel.RemainAmt;
                viewModel.ReceivedAmt = _viewModel.RemainAmt;
                viewModel.CartId = _viewModel.CartId;
                Navigation.PushAsync(_cashPaymentPage = new CashPaymentPage(viewModel));
            }
        }
    }
}