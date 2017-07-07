using System.Collections.Generic;
using Pos.Services;
using Pos.Views;
using Xamarin.Forms;
using Pos.ViewModels;

namespace Pos
{
    public partial class App : Application
    {
        public static int ScreenWidth { get; set; }
        public static int ScreenHeight { get; set; }

        public static ViewModelLocator ViewModelLocator;

        public App()
        {
            InitializeComponent();

            ViewModelLocator = new ViewModelLocator();

            DependencyService.Register<PosSDK>();
            Current.Resources = new ResourceDictionary();
            Current.Resources.Add("Locator", new ViewModelLocator());

            SetMainPage();
        }

        public static void SetMainPage()
        {
            //if (!Settings.IsLoggedIn)
            //{
            //    Current.MainPage = new NavigationPage(new LoginPage())
            //    {
            //        BarBackgroundColor = (Color)Current.Resources["Primary"],
            //        BarTextColor = Color.White
            //    };
            //}
            //else
            //{
            //    GoToMainPage();
            //}
            Current.MainPage = new IndexPage();
        }

        public static void GoToMainPage()
        {
            Current.MainPage = new TabbedPage
            {
                Children = {
                    new NavigationPage(new ContentsPage())
                    {
                        Title = "Catalog",
                        Icon = Device.OnPlatform("tab_feed.png", null, null)
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "About",
                        Icon = Device.OnPlatform("tab_about.png", null, null)
                    },
                }
            };
        }
    }
}
