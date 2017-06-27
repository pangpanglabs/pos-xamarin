using System;
using System.Collections.Generic;
using Pos.Models;
using Pos.ViewModels;
using Xamarin.Forms;

namespace Pos.Views
{
    public partial class ContentDetailPage : ContentPage
    {
		ContentDetailViewModel viewModel;

		public ContentDetailPage(ContentDetailViewModel viewModel)
		{
            InitializeComponent();

			BindingContext = this.viewModel = viewModel;
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var item = args.SelectedItem as Sku;
			if (item == null)
				return;
		}
    }
}
