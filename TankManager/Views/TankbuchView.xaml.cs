using System;
using System.Windows;
using EFSresources.EFSlogging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TankManager.Helper;
using TankManager.Lokalisierung;
using TankManager.ViewModel;
using TankManager.Model;

namespace TankManager.Views
{
	public partial class TankbuchView : PhoneApplicationPage
	{
		private FahrzeugViewModel ViewModel;

		private IApplicationBar AppBar;
		private IApplicationBarMenuItem AppBarMenuItemLogDelete;
		private IApplicationBarMenuItem AppBarMenuItemDeleteLast;

		private Boolean OrderAsc;

		public TankbuchView()
		{
			InitializeComponent();

			this.InitLogic();

			this.Loaded += new RoutedEventHandler(TankbuchView_Loaded);

			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "TankbuchView initialisiert");
		}

		private void InitLogic()
		{
			this.ViewModel = (FahrzeugViewModel)this.Resources["ViewModel"];

			if (this.ViewModel.AktuellesFahrzeug.Tankverlauf.Count > 0)
			{
				int index;

				if (this.ViewModel.AktuellesFahrzeug.Tankverlauf.Count > 10)
				{
					index = this.ViewModel.AktuellesFahrzeug.Tankverlauf.Count - 10;
				}
				else
				{
					index = 0;
				}

				this.DatePicker_From.Value = this.ViewModel.AktuellesFahrzeug.Tankverlauf[index].Datum;
				this.DatePicker_To.Value = this.ViewModel.AktuellesFahrzeug.Tankverlauf[this.ViewModel.AktuellesFahrzeug.Tankverlauf.Count - 1].Datum;
			}
			else
			{
				this.DatePicker_From.Value = DateTime.Now;
				this.DatePicker_To.Value = DateTime.Now;
			}
			
			this.DatePicker_From.ValueChanged += new EventHandler<DateTimeValueChangedEventArgs>(DatePicker_From_ValueChanged);
			this.DatePicker_To.ValueChanged += new EventHandler<DateTimeValueChangedEventArgs>(DatePicker_To_ValueChanged);

			this.Button_Order.Click += new RoutedEventHandler(Button_Order_Click);
			this.OrderAscUpdate(true);

			this.AppBarMenuItemLogDelete = new ApplicationBarMenuItem(Translation.TankbuchViewDeleteAll);
			this.AppBarMenuItemLogDelete.Click += new EventHandler(AppBarMenuItemLogDelete_Click);

			this.AppBarMenuItemDeleteLast = new ApplicationBarMenuItem(Translation.MessageBoxDeleteRefuelingLastOne_Header);
			this.AppBarMenuItemDeleteLast.Click += new EventHandler(AppBarMenuItemDeleteLast_Click);

			this.AppBar = new ApplicationBar();
			this.AppBar.Mode = ApplicationBarMode.Minimized;
			this.AppBar.MenuItems.Add(this.AppBarMenuItemDeleteLast);
			this.AppBar.MenuItems.Add(this.AppBarMenuItemLogDelete);

			this.ApplicationBar = this.AppBar;

			this.Refresh();
		}

		private void UpdateUI()
        {
			if (this.ViewModel.AktuellesFahrzeug.Tankverlauf.Count > 0)
			{
				this.ApplicationBar = this.AppBar;

				this.Grid_Content.Visibility = Visibility.Visible;
				this.Grid_FuellogNoContent.Visibility = Visibility.Collapsed;
			}
			else
			{
				this.ApplicationBar = null;

				this.FuellogNoContent_Header.Text = Translation.TankbuchViewNoContent_Header;
				this.FuellogNoContent_Content.Text = Translation.TankbuchViewNoContent_Content;

				this.Grid_Content.Visibility = Visibility.Collapsed;
				this.Grid_FuellogNoContent.Visibility = Visibility.Visible;
			}

			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "UI aktualisiert");
        }

		private void Refresh()
		{
			this.ViewModel.RefreshTankverlaufZeitraum(this.DatePicker_From.Value.Value, this.DatePicker_To.Value.Value, this.OrderAsc);
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Zeitraum aktualisiert");
		}

		private void OrderAscUpdate(Boolean asc = true)
		{
			if (asc)
			{
				this.OrderAsc = false;
				this.Button_Order.Content = "↓";
			}
			else
			{
				this.OrderAsc = true;
				this.Button_Order.Content = "↑";
			}
		}

		private void TankbuchView_Loaded(object sender, RoutedEventArgs e)
		{
			this.Refresh();
		}

		private void DatePicker_From_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
		{
			if (e.NewDateTime.Value.Ticks > this.DatePicker_To.Value.Value.Ticks)
			{
				this.DatePicker_From.Value = this.DatePicker_To.Value;
				MessageBox.Show(String.Format(Translation.MessageBoxTankbuchViewDTPickerFrom, this.DatePicker_To.Value.Value.ToShortDateString()));
			}

			this.Refresh();
		}

		private void DatePicker_To_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
		{
			if (e.NewDateTime.Value.Ticks < this.DatePicker_From.Value.Value.Ticks)
			{
				this.DatePicker_To.Value = this.DatePicker_From.Value;
				MessageBox.Show(String.Format(Translation.MessageBoxTankbuchViewDTPickerTo, this.DatePicker_From.Value.Value.ToShortDateString()));
			}

			this.Refresh();
		}

		private void Button_Order_Click(object sender, RoutedEventArgs e)
		{
			this.OrderAscUpdate(this.OrderAsc);

			this.Refresh();
		}

		private void MenuItem_Modify_Click(object sender, RoutedEventArgs e)
		{
			TankvorgangModel selectedItem = ((MenuItem)sender).DataContext as TankvorgangModel;

			App.NavigationObject = selectedItem;
			this.NavigationService.Navigate(new Uri("/Views/TankenEditView.xaml", UriKind.Relative));
		}

		private void AppBarMenuItemDeleteLast_Click(object sender, EventArgs e)
		{
			MessageBoxResult mBox = MessageBox.Show(
				String.Format(
					Translation.MessageBoxDeleteRefuelingLastOne_Content,
					this.ViewModel.AktuellesFahrzeug.LetzterTankvorgang().Datum.ToLongDateString(),
					String.Format(
						UnitFormatter.ToFormatString(UnitFormatter.Units.Length2),
						this.ViewModel.AktuellesFahrzeug.LetzterTankvorgang().GesamtKilometer)),
				Translation.MessageBoxDeleteRefuelingLastOne_Header,
				MessageBoxButton.OKCancel);

			if (mBox == MessageBoxResult.OK)
			{
				this.ViewModel.AktuellesFahrzeug.LetztenTankvorgangLöschen();
				this.UpdateUI();
			}
		}

		private void AppBarMenuItemLogDelete_Click(object sender, EventArgs e)
		{
			MessageBoxResult mBox = MessageBox.Show(
				String.Format(
					Translation.MessageBoxDeleteCompleteFuellog_Content,
					this.ViewModel.AktuellesFahrzeug.Kennzeichen,
					this.ViewModel.AktuellesFahrzeug.Name),
				Translation.MessageBoxDeleteCompleteFuellog_Header,
				MessageBoxButton.OKCancel);
			
			if (mBox == MessageBoxResult.OK)
			{
				this.ViewModel.AktuellesFahrzeug.TankverlaufLöschen();
				this.UpdateUI();
			}
		}

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.UpdateUI();
        }
	}
}