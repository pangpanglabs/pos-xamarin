using Pos.Controls;
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
    public partial class IndexPageDetail : ContentPage
    {
        public IndexPageDetail()
        {
            InitializeComponent();
            InitGrid();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.MasterDetailPage.IsGestureEnabled = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            App.MasterDetailPage.IsGestureEnabled = false;
        }

        private void InitGrid()
        {
            //ToBe:menuCount=接口返回的值
            int menuCount = 3;

            int count = menuCount % 3 == 0 ? menuCount / 3 : menuCount / 3 + 1;

            for (int i = 0; i < count; i++)
            {
                menuGrid.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new GridLength(135)
                });
            }
            menuGrid.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = new GridLength(1, GridUnitType.Star)
            });
            menuGrid.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = new GridLength(1, GridUnitType.Star)
            });
            menuGrid.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = new GridLength(1, GridUnitType.Star)
            });
            List<Action> actionList = new List<Action>();
            string[] images = new string[menuCount]; 
                                                               
            string[] menuName = new string[menuCount];
            Color[] color = new Color[menuCount];
            //ToBe:menuList=接口返回的值
            var menuList = new List<string>() { "sale","return","saleReport"};
            for (int i = 0; i < menuList.Count; i++)
            {
                if (menuList[i] == "sale")
                {
                    menuName[i] = "销售";
                    images[i] = "sale.png";
                    color[i] = Color.FromRgb(82, 170, 193);
                    actionList.Add(GoSalePage);
                }
                else if (menuList[i] == "return")
                {
                    menuName[i] = "退货";
                    images[i] = "return.png";
                    color[i] = Color.FromRgb(47, 191, 191);
                    actionList.Add(GoReturn);
                }
                else if (menuList[i] == "saleReport")
                {
                    menuName[i] = "销售记录";
                    images[i] = "litterSettlement.png";
                    color[i] = Color.FromRgb(187, 226, 232);
                    actionList.Add(GoSaleReport);
                }
            }
            for (int i = 0; i < menuCount; i++)
            {
                var stack = new ImageButton()
                {
                    ImageHeightRequest = 40,
                    ImageWidthRequest = 40,
                    Source = ImageSource.FromFile(images[i]),
                    Text = menuName[i],
                    Orientation = ImageOrientation.ImageCentered,
                    Command = new Command(actionList[i]),
                    BackgroundColor = color[i],
                    TextColor = Color.White,
                    BorderRadius = 0,
                    Margin = 2
                };
                ChangeImageButtonColor(i, stack);
                menuGrid.Children.Add(stack, i % 3, i / 3);
            }
        }
        private static void ChangeImageButtonColor(int i, ImageButton stack)
        {
            if (i < 3)
            {
                if (i % 3 == 0)
                {
                    stack.BackgroundColor = Color.FromRgb(78, 140, 168);
                }
                else if (i % 3 == 1)
                {
                    stack.BackgroundColor = Color.FromRgb(222, 206, 189);
                }
                else if (i % 3 == 2)
                {
                    stack.BackgroundColor = Color.FromRgb(189, 148, 142);
                }
            }
            else if (i > 3 && i <= 5)
            {
                if (i % 3 == 0)
                {
                    stack.BackgroundColor = Color.FromRgb(201, 199, 157);
                }
                else if (i % 3 == 1)
                {
                    stack.BackgroundColor = Color.FromRgb(78, 140, 168);
                }
                else if (i % 3 == 2)
                {
                    stack.BackgroundColor = Color.FromRgb(118, 175, 175);
                }
            }
            else if (i > 5 && i <= 8)
            {
                if (i % 3 == 0)
                {
                    stack.BackgroundColor = Color.FromRgb(162, 115, 73);
                }
                else if (i % 3 == 1)
                {
                    stack.BackgroundColor = Color.FromRgb(236, 199, 110);
                }
                else if (i % 3 == 2)
                {
                    stack.BackgroundColor = Color.FromRgb(185, 121, 177);
                }
            }
            else if (i > 8 && i <= 11)
            {
                if (i % 3 == 0)
                {
                    stack.BackgroundColor = Color.FromRgb(206, 186, 196);
                }
                else if (i % 3 == 1)
                {
                    stack.BackgroundColor = Color.FromRgb(78, 140, 168);
                }
                else if (i % 3 == 2)
                {
                    stack.BackgroundColor = Color.FromRgb(239, 236, 205);
                }
            }
        }
        private void GoSaleReport()
        {
        }

        private void GoReturn()
        {
        }

        private void GoSalePage()
        {
            Navigation.PushAsync(new CatalogPage());
        }
    }
}