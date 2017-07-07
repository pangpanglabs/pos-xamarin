using Pos.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Pos.ViewModels.Sale
{
    public class PayPrepareViewModel : BaseViewModel
    {
        public PayPrepareViewModel(Cart cart)
        {
            CurrentCart = cart;
            MessagingCenter.Subscribe<Cart>(this, "PaymentStart", async (s) =>
            {
                    CurrentCart = s;
                    if (RemainAmt == 0)
                    {
                        await PosSDK.CallAPI<Cart>("/order/place-order", new
                        {
                            cartId = this.CartId
                        });
                    }
            });
        }

        Cart currentCart;
        public Cart CurrentCart
        {
            get { return currentCart; }
            set { currentCart = value; OnPropertyChanged(); }
        }

        public decimal PayAmt
        {
            get
            {
                return CurrentCart.Payments.Sum(o => o.Amount);
            }
        }

        public decimal RemainAmt
        {
            get
            {
                return CurrentCart.RemainAmount;
            }
        }

        public ObservableCollection<PaymentInfo> PaymentInfos
        {
            get
            {
                return new ObservableCollection<PaymentInfo>(CurrentCart.Payments);
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
