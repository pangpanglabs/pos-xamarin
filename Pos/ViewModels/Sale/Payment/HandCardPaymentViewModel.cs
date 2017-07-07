using Pos.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pos.ViewModels.Sale.Payment
{
    public class HandCardPaymentViewModel: BaseViewModel
    {
        public Command HandCardPayCommand { get; set; }

        public HandCardPaymentViewModel()
        {
            Title = "Hand Card";
            HandCardPayCommand = new Command(async () => await CreateHandCardPay());
        }

        async Task CreateHandCardPay()
        {
            var result = await PosSDK.CallAPI<Cart>("/cart/set-payment", new
            {
                cartId = CartId,
                method = "HandCard",
                amount = ReceivedAmt,
            });
            if (result.Success == true)
            {
                MessagingCenter.Send<Cart>(result.Result, "PaymentStart");
                MessagingCenter.Send<object>(null, "HandCardPayComplete");
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
            }
        }
    }
}
