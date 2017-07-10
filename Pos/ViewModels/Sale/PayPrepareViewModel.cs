using Pos.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using GalaSoft.MvvmLight.Messaging;

namespace Pos.ViewModels.Sale
{
    public class PayPrepareViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        public PayPrepareViewModel()
        {
            //CurrentCart = cart;
            MessagingCenter.Subscribe<Cart>(this, "PaymentStart", async (s) =>
            {
                CurrentCart = s;
                if (RemainAmt == 0)
                {
                    var result = await PosSDK.CallAPI<Cart>("/order/place-order", new
                    {
                        cartId = this.CartId
                    });
                    if (result.Success == true)
                    {
                        Messenger.Default.Send<string>(string.Empty,"OrderPayComplete");
                    }
                }
            });
        }

        Cart currentCart;
        public Cart CurrentCart
        {
            get { return currentCart; }
            set
            {
                Set(() => CurrentCart, ref currentCart, value);
                if (value != null)
                {
                    RemainAmt = currentCart.RemainAmount;
                    PaymentInfos = new ObservableCollection<PaymentInfo>(currentCart.Payments ?? new List<PaymentInfo>());
                    PayAmt = currentCart.Payments == null ? 0 : currentCart.Payments.Sum(s => s.Amount);
                }
                else
                {
                    RemainAmt = 0;
                    PaymentInfos = new ObservableCollection<PaymentInfo>(new List<PaymentInfo>());
                    PayAmt = 0;
                }
            }
        }
        decimal payAmt;
        public decimal PayAmt
        {
            get { return payAmt; }
            set
            {
                Set(() => PayAmt, ref payAmt, value);
            }
        }
        decimal remainAmt;
        public decimal RemainAmt
        {
            get { return remainAmt; }
            set
            {
                Set(() => RemainAmt, ref remainAmt, value);
            }
        }
        ObservableCollection<PaymentInfo> paymentInfos;
        public ObservableCollection<PaymentInfo> PaymentInfos
        {
            get { return paymentInfos; }
            set
            {
                Set(() => PaymentInfos, ref paymentInfos, value);
            }
        }

        public string CartId
        {
            get
            {
                return CurrentCart.Id;
            }
        }
    }
}
