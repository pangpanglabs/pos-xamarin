using System;
using System.Collections.Generic;
using Pos.ViewModels;
using Pos.Models;
using Xamarin.Forms;

namespace Pos.Views
{
	public partial class SkusPage : ContentPage
	{
		SkusViewModel viewModel;

		public SkusPage()
		{
			InitializeComponent();
			BindingContext = viewModel = new SkusViewModel();
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var item = args.SelectedItem as Sku;
			if (item == null)
				return;
		}
	}
}
