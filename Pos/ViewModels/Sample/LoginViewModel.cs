using System.Threading.Tasks;
using System.Windows.Input;
using Pos.Services;
using Xamarin.Forms;

namespace Pos.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		public LoginViewModel()
		{
			SignInCommand = new Command(async () => await SignIn());
		}

		string message = string.Empty;
		public string Message
		{
			get { return message; }
			set
            {
                Set(() => Message, ref message, value);
            }
        }

		string tenant = string.Empty;
		public string Tenant
		{
			get { return tenant; }
			set
            {
                Set(() => Tenant, ref tenant, value);
            }
        }

		string username = string.Empty;
		public string Username
		{
			get { return username; }
			set
            {
                Set(() => Username, ref username, value);
            }
        }

		string password = string.Empty;
		public string Password
		{
			get { return password; }
			set
            {
                Set(() => Password, ref password, value);
            }
        }

		public ICommand SignInCommand { get; }

		async Task SignIn()
		{
			try
			{
				IsBusy = true;
				Message = "Signing In...";

				// Log the user in
				await TryLoginAsync();
			}
			finally
			{
				Message = string.Empty;
				IsBusy = false;

				if (Settings.IsLoggedIn)
					App.GoToMainPage();
			}
		}

		public async Task<bool> TryLoginAsync()
		{
			var result = await PosSDK.CallAPI<Models.Account>("/account/login", new
			{
				tenant = this.Tenant,
				username = this.Username,
				password = this.Password,
			});
			if (result.Success == true)
			{
				Settings.UserId = result.Result.UserId.ToString();
				Settings.AuthToken = result.Result.Token;
				App.GoToMainPage();
				return true;
			}
			return false;
		}
	}
}
