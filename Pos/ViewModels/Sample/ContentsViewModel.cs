using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Pos.Models;
using Xamarin.Forms;

namespace Pos.ViewModels
{
    public class ContentsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Content> Contents { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ContentsViewModel()
        {
            Title = "Contents";
            Contents = new ObservableRangeCollection<Content>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Contents.Clear();
                var result = await PosSDK.CallAPI<ListResult<Content>>("/catalog/search-contents");
                Contents.ReplaceRange(result.Result.Items);
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