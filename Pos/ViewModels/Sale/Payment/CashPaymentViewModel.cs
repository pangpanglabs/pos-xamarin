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
                MessagingCenter.Send<object>(this, "CahsPayComplete");
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
                Set(() => TotalAmt, ref _totalAmt, value);
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
                Set(() => ChangeAmt, ref _changeAmt, value);

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
                Set(() => ReceivedAmt, ref _receivedAmt, value);
                ChangeAmt = _receivedAmt - _totalAmt < 0 ? 0 : _receivedAmt - _totalAmt;
            }
        }
    }
}
