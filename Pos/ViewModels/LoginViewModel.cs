using System.Threading.Tasks;
using System.Windows.Input;
using Pos.Services;
using Xamarin.Forms;

namespace Pos.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		PosSDK posSDK => DependencyService.Get<PosSDK>();
		public LoginViewModel()
		{
			SignInCommand = new Command(async () => await SignIn());
		}

		string message = string.Empty;
		public string Message
		{
			get { return message; }
			set { message = value; OnPropertyChanged(); }
		}

		string tenant = string.Empty;
		public string Tenant
		{
			get { return tenant; }
			set { tenant = value; OnPropertyChanged(); }
		}

		string username = string.Empty;
		public string Username
		{
			get { return username; }
			set { username = value; OnPropertyChanged(); }
		}

		string password = string.Empty;
		public string Password
		{
			get { return password; }
			set { password = value; OnPropertyChanged(); }
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
			var result = await posSDK.CallAPI<Models.Account>("/account/login", new
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
