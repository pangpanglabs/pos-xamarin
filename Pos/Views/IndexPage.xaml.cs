using Pos.Models;
using Pos.Views.Return;
using Pos.Views.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IndexPage : MasterDetailPage
    {
        public IndexPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Content;
            if (item == null)
                return;
            //ToBe:根据点击的item，创建相应的页面
            Page page = null;
            switch (item.Code)
            {
                case "home":
                    page = new IndexPageDetail();
                    break;
                case "sale":
                    page = new CatalogPage();
                    break;
                case "return":
                    page = new ReturnOrderSearchPage();
                    break;
                default:
                    break;
            }

            if(!Detail.Navigation.NavigationStack.Any(p => p.GetType() == page.GetType()))
            {
                Detail.Navigation.PushAsync(page);
            }
            IsPresented = false;
            IsGestureEnabled = false;
            MasterPage.ListView.SelectedItem = null;
        }
    }
}