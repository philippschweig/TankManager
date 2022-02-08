using System;
using System.Windows;
using System.Windows.Navigation;
using EFSresources.Library;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace EFSresources.Views.Help
{
	public partial class HelpView : PhoneApplicationPage
	{
		HelpViewModel ViewModel;

		IApplicationBar AppBar;
		IApplicationBarMenuItem AppBarMenuItemRefresh;
		ProgressIndicator ProgressIndicator;

		public HelpView()
		{
			InitializeComponent();

			this.InitLogic();
			this.InitUI();
		}

		private void InitLogic()
		{
			this.ViewModel = new HelpViewModel();
		}

		private void InitUI()
		{
			this.SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

			this.ProgressIndicator = new ProgressIndicator() { IsIndeterminate = true, IsVisible = false };
			SystemTray.SetProgressIndicator(this, this.ProgressIndicator);

			this.ApplicationTitle.Text = AppInfo.TitleUpperCase;
			this.PageTitle.Text = EFSlanguage.EFShelpViewTitle;

			this.WebBrowser.Loaded += new RoutedEventHandler(WebBrowser_Loaded);
			this.WebBrowser.Navigated += new EventHandler<NavigationEventArgs>(WebBrowser_Navigated);

			this.AppBarMenuItemRefresh = new ApplicationBarMenuItem(EFSlanguage.DicRefresh);
			//this.AppBarMenuItemRefresh.Click += new EventHandler(AppBarMenuItemRefresh_Click);

			this.AppBar = new ApplicationBar();
			this.AppBar.Mode = ApplicationBarMode.Minimized;
			this.AppBar.MenuItems.Add(this.AppBarMenuItemRefresh);
		}

		private void WebRefreshCompleted(object sender, EventArgs e)
		{
			this.WebBrowser.NavigateToString(this.ViewModel.Help.HtmlString);
		}

		private void WebBrowser_Loaded(object sender, RoutedEventArgs e)
		{
			this.ProgressIndicator.IsVisible = true;

			this.ViewModel.Load();
			this.ViewModel.ResponseCompleted = this.WebRefreshCompleted;

			if (this.ViewModel.Help != null)
			{
				this.WebBrowser.NavigateToString(this.ViewModel.Help.HtmlString);
			}
		}

		private void WebBrowser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
		{
			this.ProgressIndicator.IsVisible = false;
		}
	}
}
