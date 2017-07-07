using Pos.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pos.ViewModels.Sale.Payment
{
    public class CashPaymentViewModel : BaseViewModel
    {
        public Command CashPayCommand { get; set; }

        public CashPaymentViewModel()
        {
            Title = "Cash";
            CashPayCommand = new Command(async () => await CreateCashPay());
        }

        async Task CreateCashPay()
        {
            var result = await PosSDK.CallAPI<Cart>("/cart/set-payment", new
            {
                cartId = CartId,
                method = "Cash",
                amount = ReceivedAmt,
            });
            if (result.Success == true)
            {
                MessagingCenter.Send<Cart>(result.Result, "PaymentStart");
            }
        }
        private string _cartId;
        public string CartId
        {
            get
            {
                return _cartId;
            }
            set
            {
                _cartId = value;
            }
        }
        private decimal _totalAmt;
        public decimal TotalAmt
        {
            get
            {
                return _totalAmt;
            }

            set
            {
                _totalAmt = value;
                OnPropertyChanged();
            }
        }

        private decimal _changeAmt;
        public decimal ChangeAmt
        {
            get
            {
                return _changeAmt;
            }
            set
            {
                _changeAmt = value;
                OnPropertyChanged();
            }
        }

        private decimal _receivedAmt;
        public decimal ReceivedAmt
        {
            get
            {
                return _receivedAmt;
            }
            set
            {
                _receivedAmt = value;
                OnPropertyChanged();
                ChangeAmt = _receivedAmt - _totalAmt < 0 ? 0 : _receivedAmt - _totalAmt;
            }
        }
    }
}
