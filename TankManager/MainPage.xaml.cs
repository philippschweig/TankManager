using System;
using System.Windows;
using EFSresources;
using EFSresources.EFSlibrary;
using EFSresources.EFSlogging;
using Microsoft.Phone.Controls;
using TankManager.Lokalisierung;
using TankManager.ViewModel;
using TombstoneHelper;
using Microsoft.Phone.Tasks;
using TankManager.Views;
using EFSresources.Views.Information;
using System.Reflection;
using TankManager.Sys;

namespace TankManager
{
	public partial class MainPage : PhoneApplicationPage
	{
		private FahrzeugViewModel ViewModel;

		// Konstruktor
		public MainPage()
		{
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "MainPage initialisiert");

			InitializeComponent();

			this.ViewModel = (FahrzeugViewModel)this.Resources["ViewModel"];

			EFStools.SetAppBarIconButtonsText(this.ApplicationBar, Translation.TankenAddViewTitle, Translation.TankbuchViewTitle);
			EFStools.SetAppBarMenuItemsText(this.ApplicationBar, Translation.HelpViewTitle, Translation.MainPageAppBarMenuItemSettings, Translation.InfoViewTitle);

			// Datenkontext des Listenfeldsteuerelements auf die Beispieldaten festlegen
			this.DataContext = App.ViewModel;
			//this.Loaded += new RoutedEventHandler(MainPage_Loaded);

			if (!EFSlogsystem.Logger.RegularEnded())
			{
				MessageBoxResult mBox = MessageBox.Show(EFSlanguage.EFSlogsystem_MessageBox_Content, EFSlanguage.EFSlogsystem_MessageBox_Header, MessageBoxButton.OKCancel);
				if (mBox == MessageBoxResult.OK)
				{
					String test = EFSlogsystem.Logger.lastLog().getCrash();

					EmailComposeTask task = new EmailComposeTask();
					task.To = "efsdeveloping@gmail.com";
					task.Body = "TankManager Log\nVersion " + EFSappinfos.GetAppVersion() + "\n\n\n" + test;
					task.Subject = "TankManager Log";
					task.Show();
				}
			}
		}

		private void UpdateView()
        {
			if (App.ViewModel.UserEinstellungen.ShowVehicleEditButton)
			{
				this.Button_VehicleEdit.Visibility = Visibility.Visible;
			}
			else
			{
				this.Button_VehicleEdit.Visibility = Visibility.Collapsed;
			}

			if (this.ViewModel.AktuellesFahrzeug != null)
			{
				this.Grid_OverviewNoContent.Visibility = Visibility.Collapsed;
				this.Grid_FuellogNoContent.Visibility = Visibility.Collapsed;
				this.Grid_DataAnalysisNoContent.Visibility = Visibility.Collapsed;

				this.ScrollViewer_Overview.Visibility = Visibility.Visible;
				this.ListBox_Fuellog.Visibility = Visibility.Visible;
				this.ListBox_DataAnalysis.Visibility = Visibility.Visible;

				EFStools.GetAppBarIconButton(this.ApplicationBar, 1).IsEnabled = true;
				EFStools.GetAppBarIconButton(this.ApplicationBar, 2).IsEnabled = true;

				if (this.ViewModel.AktuellesFahrzeug.Tankverlauf.Count == 0)
				{
					this.FuellogNoContent_Header.Text = Translation.TankbuchViewNoContent_Header;
					this.FuellogNoContent_Content.Text = Translation.TankbuchViewNoContent_Content;

					this.Grid_FuellogNoContent.Visibility = Visibility.Visible;
					this.ListBox_Fuellog.Visibility = Visibility.Collapsed;
				}
				else
				{
					this.Refresh();
				}
			}
			else
			{
				this.FuellogNoContent_Header.Text = Translation.MainPageNoContentHeader;
				this.FuellogNoContent_Content.Text = Translation.MainPageNoContentContent;

				this.Grid_OverviewNoContent.Visibility = Visibility.Visible;
				this.Grid_FuellogNoContent.Visibility = Visibility.Visible;
				this.Grid_DataAnalysisNoContent.Visibility = Visibility.Visible;

				this.ScrollViewer_Overview.Visibility = Visibility.Collapsed;
				this.ListBox_Fuellog.Visibility = Visibility.Collapsed;
				this.ListBox_DataAnalysis.Visibility = Visibility.Collapsed;

				EFStools.GetAppBarIconButton(this.ApplicationBar, 1).IsEnabled = false;
				EFStools.GetAppBarIconButton(this.ApplicationBar, 2).IsEnabled = false;
			}
        }

		// Daten für die ViewModel-Elemente laden
		private void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			this.ViewModel.Load();

			this.UpdateView();

			this.ApplicationBar.IsVisible = true;

            if (App.ViewModel.WasUpdated)
            {
                App.ViewModel.WasUpdated = false;
                MessageBoxResult mBox = MessageBox.Show(Translation.UpgradeContent, Translation.UpgradeTitle, MessageBoxButton.OKCancel);
                if (mBox == MessageBoxResult.OK)
                {
                    this.NavigationService.Navigate(new Uri("/Views/InfoView.xaml?Pivot=1", UriKind.Relative));
                }
            }

			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "MainPage geladen");
		}

		private void Refresh()
        {
			this.ViewModel.RefreshTankverlaufLast10();
        }

		private void Button_VehicleEdit_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			App.NavigationObject = this.ViewModel.AktuellesFahrzeug;
            this.NavigationService.Navigate(new Uri("/Views/FahrzeugEditView.xaml", UriKind.Relative));
		}

		private void AppBarIconButton_Tanken_Click(object sender, System.EventArgs e)
		{
            this.NavigationService.Navigate(new Uri("/Views/TankenAddView.xaml", UriKind.Relative));
		}

		private void AppBarIconButton_FuelLog_Click(object sender, System.EventArgs e)
		{
            this.NavigationService.Navigate(new Uri("/Views/TankbuchView.xaml", UriKind.Relative));
		}

		private void AppBarMenuItem_Settings_Click(object sender, System.EventArgs e)
		{
			this.NavigationService.Navigate(new Uri("/Views/EinstellungenView.xaml", UriKind.Relative));
		}

		private void AppBarMenuItem_Infos_Click(object sender, System.EventArgs e)
		{
			this.NavigationService.Navigate(new Uri("/Views/InfoView.xaml", UriKind.Relative));

			//InformationTransporter.Navigate(this.NavigationService, SysConst.InfoTransport);
		}

		private void AppBarMenuItem_Help_Click(object sender, System.EventArgs e)
		{
			this.NavigationService.Navigate(new Uri("/Views/HelpView.xaml", UriKind.Relative));
		}

		private void MenuItem_Tanktabelle_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.NavigationService.Navigate(new Uri("/Views/TanktabelleView.xaml", UriKind.Relative));
		}

		#region navigation
		protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
		{
			base.OnNavigatingFrom(e);

			this.SaveState(e);
		}

		protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			this.RestoreState();

			EFStools.DeactivateAllIconButtons(this.ApplicationBar);

			if (App.DormantState)
			{
				this.UpdateView();
				App.DormantState = false;
			}
		}

		protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
		{
			if (App.ViewModel.UserEinstellungen.ShowExitMessage)
			{
				MessageBoxResult mBox = MessageBox.Show(Translation.MessageBoxExit_Content, Translation.MessageBoxExit_Header, MessageBoxButton.OKCancel);
				if (mBox == MessageBoxResult.Cancel)
				{
					e.Cancel = true;
				}
			}

			base.OnBackKeyPress(e);
		}
		#endregion

		//
	}
}