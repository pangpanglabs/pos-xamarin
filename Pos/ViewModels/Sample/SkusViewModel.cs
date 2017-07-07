using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Pos.Models;
using Xamarin.Forms;

namespace Pos.ViewModels
{
	public class SkusViewModel : BaseViewModel
	{
		public ObservableRangeCollection<Sku> Skus { get; set; }
		public Command LoadItemsCommand { get; set; }

		public SkusViewModel()
		{
			Title = "Skus";
			Skus = new ObservableRangeCollection<Sku>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
		}

		async Task ExecuteLoadItemsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				Skus.Clear();
				var result = await PosSDK.CallAPI<ListResult<Sku>>("/catalog/search-skus");
				Skus.ReplaceRange(result.Result.Items);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
				{
					Title = "Error",
					Message = "Unable to load items.",
					Cancel = "OK"
				}, "message");
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
