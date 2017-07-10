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
        public INavigation Navigation { get; set; }
        public PayPrepareViewModel()
        {
            //CurrentCart = cart;
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
        decimal payAmt;
        public decimal PayAmt
        {
            get { return payAmt; }
            set { payAmt = value; OnPropertyChanged(); }
        }
        decimal remainAmt;
        public decimal RemainAmt
        {
            get { return remainAmt; }
            set { remainAmt = value; OnPropertyChanged(); }
        }
        ObservableCollection<PaymentInfo> paymentInfos;
        public ObservableCollection<PaymentInfo> PaymentInfos
        {
            get { return paymentInfos; }
            set { paymentInfos = value; OnPropertyChanged(); }
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
