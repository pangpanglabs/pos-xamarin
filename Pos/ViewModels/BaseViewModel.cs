using GalaSoft.MvvmLight;
using Pos.Services;
using Xamarin.Forms;

namespace Pos
{
    public class BaseViewModel : ViewModelBase
    {
        /// <summary>
        /// Get the azure service instance
        /// </summary>
        protected PosSDK PosSDK => DependencyService.Get<PosSDK>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { Set(ref isBusy, value); }
        }
        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get { return title; }
            set { Set(ref title, value); }
        }
    }
}
