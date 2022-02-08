using System;
using System.Windows.Controls;
using EFSresources.EFSframeworkelements;
using EFSresources.EFSlibrary;
using EFSresources.EFSlogging;
using Microsoft.Phone.Controls;
using TankManager.Lokalisierung;
using TankManager.Model;
using EFSresources.Controllers;
using TombstoneHelper;
using EFSresources.Frameworkelements.Controllers;

namespace TankManager.Views
{
	public partial class FahrzeugAddView : PhoneApplicationPage
	{
		private RotateController TabController;
        private PointToCommaController CommaController;

		public FahrzeugAddView()
		{
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "FahrzeugAddView initialisiert");

			InitializeComponent();

			this.TabController = new RotateController(this);
			this.TabController.Add(this.TextBox_Name);
			this.TabController.Add(this.TextBox_PKZ_T1);
			this.TabController.Add(this.TextBox_PKZ_T2);
			this.TabController.Add(this.TextBox_Km);

            this.CommaController = new PointToCommaController();
            this.CommaController.Add(this.TextBox_Km);

			this.UpdateToggleSwitch(ToggleSwitch_Counter, Translation.ToggleSwitchOn, Translation.ToggleSwitchOff);

			EFStools.SetAppBarIconButtonsText(this.ApplicationBar, Translation.DicNext, Translation.DicSave);
		}

		private void CheckData()
		{
			Boolean check = true;

			if (this.TextBox_Name.Text == "")
			{
				check = false;
			}

			if (this.TextBox_PKZ_T1.Text == "")
			{
				check = false;
			}

			if (this.TextBox_PKZ_T2.Text == "")
			{
				check = false;
			}

			if (this.TextBox_Km.Text == "")
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

		private void AppBarIconButtonNext_Click(object sender, System.EventArgs e)
		{
			this.TabController.Rotate();
		}

		private void AppBarIconButtonSave_Click(object sender, System.EventArgs e)
		{
			String
				region = this.TextBox_PKZ_T1.Text,
				kennung = this.TextBox_PKZ_T2.Text,
				name = this.TextBox_Name.Text;
			Int32 km = (Int32)Double.Parse(this.TextBox_Km.Text);
			SpritModel sp = this.ListPicker_Kraftstoff.SelectedItem as SpritModel;
			Boolean tKm = this.ToggleSwitch_Counter.IsChecked.Value;

			App.ViewModel.AddVehicle(new KennzeichenModel() { Region = region, Kennung = kennung }, name, km, sp, tKm);
			this.NavigationService.GoBack();
		}

		private void TextBox_Name_LostFocus(object sender, System.Windows.RoutedEventArgs e)
		{
			this.CheckData();
		}

		private void TextBox_PKZ_T1_LostFocus(object sender, System.Windows.RoutedEventArgs e)
		{
			this.CheckData();
		}

		private void TextBox_PKZ_T2_LostFocus(object sender, System.Windows.RoutedEventArgs e)
		{
			this.CheckData();
		}

		private void TextBox_Km_LostFocus(object sender, System.Windows.RoutedEventArgs e)
		{
			this.CheckData();
		}

		private void ToggleSwitch_Counter_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.UpdateToggleSwitch((ToggleSwitch)sender, Translation.ToggleSwitchOn, Translation.ToggleSwitchOff);
		}

		private void TextBox_PKZ_T1_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (this.TextBox_PKZ_T1.Text.Length >= 3)
			{
				e.Handled = true;
			}
		}

		private void TextBox_PKZ_T2_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (this.TextBox_PKZ_T2.Text.Length >= 7)
			{
				e.Handled = true;
			}
		}

		private void TextBox_PKZ_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			String eingabe = ((TextBox)sender).Text;
			Int32 selectionStart;

			if (eingabe.Length > 0)
			{
				selectionStart = ((TextBox)sender).SelectionStart;
				((TextBox)sender).Text = eingabe.ToUpper();
				((TextBox)sender).Select(selectionStart, 0);
			}
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