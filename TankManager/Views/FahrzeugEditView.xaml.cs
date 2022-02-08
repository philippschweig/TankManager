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
using Microsoft.Phone.Controls;
using EFSresources.EFSframeworkelements;
using TankManager.Model;
using TankManager.Lokalisierung;
using System.Diagnostics;
using EFSresources.EFSlibrary;
using EFSresources.Controllers;
using TombstoneHelper;

namespace TankManager
{
    public partial class FahrzeugEditView : PhoneApplicationPage
    {
		private RotateController TabController;

		public FahrzeugModel Fahrzeug;

        public FahrzeugEditView()
        {
            InitializeComponent();

			#region ChangedEvents
			this.TextBox_Name.TextChanged += new TextChangedEventHandler(TextBox_TextChanged);

			this.TextBox_PKZ_T1.KeyDown += new KeyEventHandler(TextBox_PKZ_T1_KeyDown);
			this.TextBox_PKZ_T1.TextChanged += new TextChangedEventHandler(TextBox_PKZ_TextChanged);
			this.TextBox_PKZ_T1.TextChanged += new TextChangedEventHandler(TextBox_TextChanged);

			this.TextBox_PKZ_T2.KeyDown += new KeyEventHandler(TextBox_PKZ_T2_KeyDown);
			this.TextBox_PKZ_T2.TextChanged += new TextChangedEventHandler(TextBox_PKZ_TextChanged);
			this.TextBox_PKZ_T2.TextChanged += new TextChangedEventHandler(TextBox_TextChanged);

			this.ToggleSwitch_Counter.Click += new EventHandler<RoutedEventArgs>(ToggleSwitch_Counter_Click);

			#region BETA
			// BETA START
			this.ListPicker_Kraftstoff.SelectionChanged += new SelectionChangedEventHandler(ListPicker_Kraftstoff_SelectionChanged);
			// BETA END
			#endregion

			#endregion

			this.TabController = new RotateController(this);
			this.TabController.Add(this.TextBox_Name);
			this.TabController.Add(this.TextBox_PKZ_T1);
			this.TabController.Add(this.TextBox_PKZ_T2);

			EFStools.GetAppBarIconButton(this.ApplicationBar, 2).IsEnabled = false;
			EFStools.SetAppBarIconButtonsText(this.ApplicationBar, Translation.DicNext, Translation.DicSave);
        }

		private void Init()
		{
			if (App.NavigationObject != null)
			{
				this.Fahrzeug = (FahrzeugModel)App.NavigationObject;
				App.NavigationObject = null;

				this.DataContext = this.Fahrzeug;

				this.ToggleSwitch_Counter.IsChecked = this.Fahrzeug.ZähleMitTageskilometer;
				this.ToggleSwitch_Counter.Refresh(Translation.ToggleSwitchOn, Translation.ToggleSwitchOff);

				foreach (var item in SpritModel.KraftstoffListe)
				{
					if (item.id == this.Fahrzeug.Sprit.id)
					{
						this.ListPicker_Kraftstoff.SelectedItem = item;
					}
				}
			}
		}

		private Boolean CheckChanging()
		{
			Boolean changed = false;

			if (this.TextBox_Name.Text != this.Fahrzeug.Name)
			{
				changed = true;
			}

			if (this.TextBox_PKZ_T1.Text != this.Fahrzeug.Kennzeichen.Region)
			{
				changed = true;
			}

			if (this.TextBox_PKZ_T2.Text != this.Fahrzeug.Kennzeichen.Kennung)
			{
				changed = true;
			}

			if (this.ToggleSwitch_Counter.IsChecked.Value != this.Fahrzeug.ZähleMitTageskilometer)
			{
				changed = true;
			}

			if ((this.ListPicker_Kraftstoff.SelectedItem as SpritModel).id != this.Fahrzeug.Sprit.id)
			{
				changed = true;
			}

			return changed;
		}

		private void UpdateSaveButton()
		{
			if (this.CheckChanging())
			{
				EFStools.GetAppBarIconButton(this.ApplicationBar, 2).IsEnabled = true;
			}
			else
			{
				EFStools.GetAppBarIconButton(this.ApplicationBar, 2).IsEnabled = false;
			}
		}

		#region Kennzeichen Textbox
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
		#endregion

		private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			this.UpdateSaveButton();
		}

		private void ToggleSwitch_Counter_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.ToggleSwitch_Counter.Refresh(Translation.ToggleSwitchOn, Translation.ToggleSwitchOff);
			this.UpdateSaveButton();
		}

		private void ListPicker_Kraftstoff_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.UpdateSaveButton();
		}

        private void AppBarIconButton_Next_Click(object sender, System.EventArgs e)
        {
			this.TabController.Rotate();
        }

		private void AppBarIconButton_Save_Click(object sender, System.EventArgs e)
		{
			this.Fahrzeug.Name = this.TextBox_Name.Text;
			this.Fahrzeug.Kennzeichen = new KennzeichenModel() { Region = this.TextBox_PKZ_T1.Text, Kennung = this.TextBox_PKZ_T2.Text };
			this.Fahrzeug.ZähleMitTageskilometer = this.ToggleSwitch_Counter.IsChecked.Value;
			this.Fahrzeug.Sprit = this.ListPicker_Kraftstoff.SelectedItem as SpritModel;

			this.UpdateSaveButton();
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

            this.Init();
        }
        #endregion
    }
}
