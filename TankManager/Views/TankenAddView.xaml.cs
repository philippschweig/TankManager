using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using EFSresources.EFSframeworkelements;
using EFSresources.EFSlibrary;
using EFSresources.EFSlogging;
using Microsoft.Phone.Controls;
using TankManager.Helper;
using TankManager.Lokalisierung;
using TankManager.Model;
using TankManager.ViewModel;
using System.Diagnostics;
using EFSresources.Controllers;
using TombstoneHelper;
using EFSresources.Frameworkelements.Controllers;

namespace TankManager.Views
{
	public partial class TankenAddView : PhoneApplicationPage
	{
		private FahrzeugViewModel ViewModel;

		private Boolean erstesTanken = false;
		private Boolean erstesTankenShowed = false;
		private Boolean benötigeGesamtkilometerstand = false;
		private Boolean benötigeGesamtkilometerstandShowed = false;
		private SpritQuelleModel spritQuelle;

		private RotateController TabController;
        private PointToCommaController CommaController;

		public TankenAddView()
		{
			InitializeComponent();

			this.TabController = new RotateController(this);
			this.TabController.Add(this.TextBox_Km);
			this.TabController.Add(this.TextBox_Liter);
			this.TabController.Add(this.TextBox_Kosten);

            this.CommaController = new PointToCommaController();
            this.CommaController.Add(this.TextBox_Km);
            this.CommaController.Add(this.TextBox_Liter);
            this.CommaController.Add(this.TextBox_Kosten);

			this.ListPicker_SpritQuelle.ItemsSource = SpritQuelleModel.SpritQuellen;
			this.spritQuelle = (SpritQuelleModel)this.ListPicker_SpritQuelle.SelectedItem;

			this.ViewModel = (FahrzeugViewModel)this.Resources["ViewModel"];

			if (!(this.ViewModel.AktuellesFahrzeug.Tankverlauf.Count > 0))
			{
				this.erstesTanken = true;
			}
			else if (!this.ViewModel.AktuellesFahrzeug.IstLetzterTankvorgangVollgetankt())
			{
				this.benötigeGesamtkilometerstand = true;
			}

			this.Layouting();

			EFStools.SetAppBarIconButtonsText(this.ApplicationBar, Translation.DicNext, Translation.DicSave);

			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "TankenAddView initialisiert");
		}

		private void Layouting()
		{
			if (this.erstesTanken)
			{
				this.UpdateTextBlock_Km(false);
				this.ListPicker_SpritQuelle.SelectedIndex = this.ListPicker_SpritQuelle.Items.IndexOf(SpritQuelleModel.Zapfsaeule);
				this.ListPicker_SpritQuelle.IsEnabled = false;
			}
			else if (this.benötigeGesamtkilometerstand)
			{
				this.UpdateTextBlock_Km(false);
			}
			else
			{
				this.LayoutingKm();
			}

			this.UpdateToggleSwitch(this.ToggleSwitch_Vollgetankt, Translation.ToggleSwitchYes, Translation.ToggleSwitchNo);
		}

		public void LayoutingKm(Boolean status = true)
        {
			if (status && !this.erstesTanken && !this.benötigeGesamtkilometerstand)
			{
				if (App.ViewModel.UserEinstellungen.KmZaehler == Kilometerzaehler.Individual)
				{
					this.ToggleSwitch_KmZaehler.IsChecked = this.ViewModel.AktuellesFahrzeug.ZähleMitTageskilometer;
					this.UpdateToggleSwitch(this.ToggleSwitch_KmZaehler, Translation.ToggleSwitchYes, Translation.ToggleSwitchNo);
					this.ToggleSwitch_KmZaehler.Visibility = Visibility.Visible;
				}

				this.UpdateTextBlock_Km(this.ViewModel.AktuellesFahrzeug.ZähleMitTageskilometer);
			}
			else
			{
				this.ToggleSwitch_KmZaehler.IsChecked = false;
				this.UpdateToggleSwitch(this.ToggleSwitch_KmZaehler, Translation.ToggleSwitchYes, Translation.ToggleSwitchNo);
				this.ToggleSwitch_KmZaehler.Visibility = Visibility.Collapsed;
				this.UpdateTextBlock_Km(false);
			}
        }

		private void CheckData()
        {
			Boolean check = true;

			if (this.TextBox_Km.Text == "")
			{
				check = false;
			}

			if (this.TextBox_Liter.Text == "")
			{
				check = false;
			}

			if (this.TextBox_Kosten.Text == "" && this.spritQuelle == SpritQuelleModel.Zapfsaeule)
			{
				check = false;
			}

			if (check)
			{
				EFStools.GetAppBarIconButton(this.ApplicationBar, 2).IsEnabled = true;
			}
			else
			{
				EFStools.GetAppBarIconButton(this.ApplicationBar, 2).IsEnabled = false;
			}
        }

		private void PhoneApplicationPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			if (this.erstesTanken)
			{
				this.ToggleSwitch_Vollgetankt.IsEnabled = false;

				if (!this.erstesTankenShowed)
				{
					MessageBox.Show(Translation.MessageBoxFirstFuellingContent, Translation.MessageBoxFirstFuellingHeader, MessageBoxButton.OK);
					this.erstesTankenShowed = true;
				}
			}
			else if (this.benötigeGesamtkilometerstand)
			{
				if (!this.benötigeGesamtkilometerstandShowed)
				{
					MessageBox.Show(Translation.MessageBoxNeedTotalKmContent, Translation.MessageBoxNeedTotalKmHeader, MessageBoxButton.OK);
					this.benötigeGesamtkilometerstandShowed = true;
				}
			}
		}

		private void ListPicker_SpritQuelle_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			this.spritQuelle = (SpritQuelleModel)this.ListPicker_SpritQuelle.SelectedItem;

			if (this.spritQuelle == SpritQuelleModel.Kanister)
			{
				this.StackPanel_Kosten.Visibility = Visibility.Collapsed;
				this.ToggleSwitch_Vollgetankt.IsChecked = false;
				this.ToggleSwitch_Vollgetankt_Click(sender, e);

				this.TabController.DeactivateElement(this.TextBox_Kosten);
			}
			else
			{
				this.StackPanel_Kosten.Visibility = Visibility.Visible;

				this.TabController.ActivateElement(this.TextBox_Kosten);
			}
		}

		private void ToggleSwitch_Vollgetankt_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.UpdateToggleSwitch(this.ToggleSwitch_Vollgetankt, Translation.ToggleSwitchYes, Translation.ToggleSwitchNo);

			if ((Boolean)this.ToggleSwitch_Vollgetankt.IsChecked)
			{
				this.LayoutingKm();
			}
			else
			{
				this.LayoutingKm(false);
			}
		}

		private void Button_Datum_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.DatePicker_Datum.IsEnabled = true;
			this.Button_Datum.IsEnabled = false;
			//Button_Datum.Visibility = Visibility.Collapsed;
		}

		private void DatePicker_Datum_ValueChanged(object sender, Microsoft.Phone.Controls.DateTimeValueChangedEventArgs e)
		{
			if (this.ViewModel.AktuellesFahrzeug.Tankverlauf.Count > 0)
			{
				if (((DateTime)this.DatePicker_Datum.Value).Ticks < this.ViewModel.AktuellesFahrzeug.Tankverlauf[this.ViewModel.AktuellesFahrzeug.Tankverlauf.Count - 1].Datum.Ticks)
				{
					MessageBox.Show(Translation.MessageBoxDateChangedFailedContent, Translation.MessageBoxDateChangedFailedHeader, MessageBoxButton.OK);
					this.DatePicker_Datum.Value = DateTime.Now;
				}
			}
		}

		private void AppBarIconButtonNext_Click(object sender, System.EventArgs e)
		{
			this.TabController.Rotate();
		}

		private void AppBarIconButtonSave_Click(object sender, System.EventArgs e)
		{
			Double km, liter, kosten;
			Boolean vollgetankt = (Boolean)this.ToggleSwitch_Vollgetankt.IsChecked;
			SpritQuelleModel spritQuelle = (SpritQuelleModel)this.ListPicker_SpritQuelle.SelectedItem;

			Double.TryParse(this.TextBox_Km.Text, out km);
			Double.TryParse(this.TextBox_Liter.Text, out liter);
			Double.TryParse(this.TextBox_Kosten.Text, out kosten);

			// Bei jeden Tanken auswählen wie gezählt werden soll
			if (App.ViewModel.UserEinstellungen.KmZaehler == Kilometerzaehler.Individual || this.erstesTanken || !vollgetankt)
			{
				Boolean tagesZaehler;

				if (this.erstesTanken || !vollgetankt)
				{
					tagesZaehler = false;
				}
				else
				{
					tagesZaehler = (Boolean)this.ToggleSwitch_KmZaehler.IsChecked;
				}

				if (this.DatePicker_Datum.IsEnabled)
				{
					this.ViewModel.AktuellesFahrzeug.Tanken(km, liter, kosten, (DateTime)this.DatePicker_Datum.Value, spritQuelle, vollgetankt, tagesZaehler);
				}
				else
				{
					this.ViewModel.AktuellesFahrzeug.Tanken(km, liter, kosten, spritQuelle, vollgetankt, tagesZaehler);
				}
			}
			else
			{
				if (this.DatePicker_Datum.IsEnabled)
				{
					this.ViewModel.AktuellesFahrzeug.Tanken(km, liter, kosten, (DateTime)this.DatePicker_Datum.Value, spritQuelle, vollgetankt);
				}
				else
				{
					this.ViewModel.AktuellesFahrzeug.Tanken(km, liter, kosten, spritQuelle, vollgetankt);
				}
			}

			NavigationService.GoBack();
		}

		private void UpdateTextBlock_Km(Boolean _tageskilometerZaehler)
		{
			if (_tageskilometerZaehler)
			{
				this.TextBlock_Km.Text = Translation.UnitSystemDriveLength;
			}
			else
			{
				this.TextBlock_Km.Text = Translation.UnitSystemDriveTotalLength;
			}
		}

		private void TextBox_Km_LostFocus(object sender, System.Windows.RoutedEventArgs e)
		{
			this.CheckData();
		}

		private void TextBox_Liter_LostFocus(object sender, System.Windows.RoutedEventArgs e)
		{
			this.CheckData();
		}

		private void TextBox_Kosten_LostFocus(object sender, System.Windows.RoutedEventArgs e)
		{
			this.CheckData();
		}

		private void ToggleSwitch_KmZaehler_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.UpdateToggleSwitch((ToggleSwitch)sender, Translation.ToggleSwitchYes, Translation.ToggleSwitchNo);
			this.UpdateTextBlock_Km((Boolean)((ToggleSwitch)sender).IsChecked);
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
        }
        #endregion
	}
}