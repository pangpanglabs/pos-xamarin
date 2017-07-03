using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IndexPageMaster : ContentPage
    {
        public ListView ListView;

        public IndexPageMaster()
        {
            InitializeComponent();

            BindingContext = new IndexPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class IndexPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<IndexPageMenuItem> MenuItems { get; set; }
            
            public IndexPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<IndexPageMenuItem>(new[]
                {
                    new IndexPageMenuItem { Id = 0, Title = "Page 1" },
                    new IndexPageMenuItem { Id = 1, Title = "Page 2" },
                    new IndexPageMenuItem { Id = 2, Title = "Page 3" },
                    new IndexPageMenuItem { Id = 3, Title = "Page 4" },
                    new IndexPageMenuItem { Id = 4, Title = "Page 5" },
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}