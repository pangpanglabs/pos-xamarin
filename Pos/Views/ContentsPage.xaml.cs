using System;
using System.Collections.Generic;
using Pos.Models;
using Pos.ViewModels;
using Xamarin.Forms;

namespace Pos.Views
{
    public partial class ContentsPage : ContentPage
    {
        ContentsViewModel viewModel;

        public ContentsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ContentsViewModel();
        }

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var content = args.SelectedItem as Content;
			if (content == null)
				return;

			await Navigation.PushAsync(new ContentDetailPage(new ContentDetailViewModel(content)));

			// Manually deselect item
			ItemsListView.SelectedItem = null;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (viewModel.Contents.Count == 0)
				viewModel.LoadItemsCommand.Execute(null);
		}
    }
}
