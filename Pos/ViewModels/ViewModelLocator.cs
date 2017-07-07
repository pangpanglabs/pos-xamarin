/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Pos.Droid"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Pos.ViewModels.Sale;
using Pos.ViewModels.Sale.Payment;

namespace Pos.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<CashPaymentViewModel>();
            SimpleIoc.Default.Register<HandCardPaymentViewModel>();
            SimpleIoc.Default.Register<ContentDetailViewModel>();
            SimpleIoc.Default.Register<CartViewModel>();
            SimpleIoc.Default.Register<CatalogViewModel>();
            SimpleIoc.Default.Register<ContentsViewModel>();
            SimpleIoc.Default.Register<ContentDetailViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<SkusViewModel>();
            SimpleIoc.Default.Register<PayPrepareViewModel>();
        }

        public IndexViewModel IndexViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IndexViewModel>();
            }
        }

        public CatalogViewModel CatalogViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CatalogViewModel>();
            }
        }

        public CartViewModel CartViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CartViewModel>();
            }
        }
        public CashPaymentViewModel CashPaymentViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CashPaymentViewModel>();
            }
        }
        public HandCardPaymentViewModel HandCardPaymentViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HandCardPaymentViewModel>();
            }
        }
        public ContentDetailViewModel ContentDetailViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ContentDetailViewModel>();
            }
        }

        public LoginViewModel LoginViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }
        public SkusViewModel SkusViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SkusViewModel>();
            }
        }

        public PayPrepareViewModel PayPrepareViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PayPrepareViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}