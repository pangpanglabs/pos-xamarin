using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Services
{
    public interface IUserDialogService
    {

        /// <summary>
        /// show loading
        /// userdialog
        /// https://github.com/aritchie/userdialogs/blob/master/src/Samples/Samples/MainPage.cs
        /// </summary>
        void ShowLoading(string msg);

        /// <summary>
        /// hide loading
        /// userdialog
        /// https://github.com/aritchie/userdialogs/blob/master/src/Samples/Samples/MainPage.cs
        /// </summary>
        void HideLoading();

        /// <summary>
        /// alert
        /// https://github.com/aritchie/userdialogs/blob/master/src/Samples/Samples/MainPage.cs
        /// </summary>
        void Alert(string msg, string title);

        /// <summary>
        /// alert
        /// https://github.com/aritchie/userdialogs/blob/master/src/Samples/Samples/MainPage.cs
        /// </summary>
        /// <param name="msg"></param>
        void AlertLongText(string msg);

        /// <summary>
        /// alert
        /// https://github.com/aritchie/userdialogs/blob/master/src/Samples/Samples/MainPage.cs
        /// </summary>
        /// <param name="msg"></param>
        void ShowToast(string msg);

        /// <summary>
        /// 输入文本框
        /// </summary>
        /// <returns></returns>
        Task<string> ShowPromt(string title);
        /// <summary>
        /// 确认框
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        Task<bool> Confirm(string msg);
    }
}
