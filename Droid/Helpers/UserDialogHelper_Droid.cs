using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Pos.Services;
using Acr.UserDialogs;

namespace Pos.Droid.Helpers
{
    public class UserDialogHelper_Droid : IUserDialogService
    {
        /// <summary>
        /// show loading
        /// userdialog
        /// https://github.com/aritchie/userdialogs/blob/master/src/Samples/Samples/MainPage.cs
        /// </summary>
        public void ShowLoading(string msg)
        {
            try
            {
                UserDialogs.Instance.ShowLoading(msg);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// hide loading
        /// userdialog
        /// https://github.com/aritchie/userdialogs/blob/master/src/Samples/Samples/MainPage.cs
        /// </summary>
        public void HideLoading()
        {
            try
            {
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// alert
        /// https://github.com/aritchie/userdialogs/blob/master/src/Samples/Samples/MainPage.cs
        /// </summary>
        public async void Alert(string msg, string title)
        {
            try
            {
                await UserDialogs.Instance.AlertAsync(msg, null, "关闭");
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// alert
        /// https://github.com/aritchie/userdialogs/blob/master/src/Samples/Samples/MainPage.cs
        /// </summary>
        /// <param name="content"></param>
        public void AlertLongText(string msg)
        {
            try
            {
                UserDialogs.Instance.Alert(msg, null, "关闭");
            }
            catch (Exception)
            {
            }
        }

        public void ShowToast(string msg)
        {
            try
            {
                UserDialogs.Instance.Toast(msg, new TimeSpan(2000));
            }
            catch (Exception)
            {
            }
        }
        public async Task<string> ShowPromt(string title)
        {
            PromptConfig config = new PromptConfig()
            {
                CancelText = "取消",
                InputType = InputType.Default,
                IsCancellable = true,
                OkText = "确定",
                Title = title,
                Text = ""
            };
            var result = await UserDialogs.Instance.PromptAsync(config);
            if (result.Ok)
            {
                return result.Text;
            }
            return "Cancel";
        }
        public async Task<bool> Confirm(string msg)
        {
            var result = await UserDialogs.Instance.ConfirmAsync(msg, "温馨提示", "确定", "取消");

            return result;
        }
    }
}