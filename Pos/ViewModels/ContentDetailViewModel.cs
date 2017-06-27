using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Pos.Models;
using Xamarin.Forms;

namespace Pos.ViewModels
{
    public class ContentDetailViewModel : BaseViewModel
    {
        public Content Content { get; set; }
        public ObservableRangeCollection<Sku> Skus { get; set; }
        public ContentDetailViewModel(Content content = null)
        {
            Title = content.Code;
            Skus = new ObservableRangeCollection<Sku>();
            Content = content;
            Task.Run(async () => await Load(content.Id));
        }

		async Task Load(long contentId)
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{				
				var result = await PosSDK.CallAPI<Content>("/catalog/get-content", new { id = contentId });
                //Skus = new ObservableRangeCollection<Sku>(result.Result.Skus);
				Skus.ReplaceRange(result.Result.Skus);
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