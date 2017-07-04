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
    public partial class IndexDetail : ContentPage
    {
        public IndexDetail()
        {
            InitializeComponent();
            InitGrid();
        }

        private void InitGrid()
        {
            //ToBe:通过接口获取应该显示几个菜单
            int menuCount = 3;
            //根据菜单数计算显示几行
            int count = menuCount % 3 == 0 ? menuCount / 3 : menuCount / 3 + 1;
            for (int i = 0; i < count; i++)
            {
                menuGrd.RowDefinitions.Add(new RowDefinition() { Height=new GridLength(135)});
            }
        }
    }
}