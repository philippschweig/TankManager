using System;
using System.Windows;
using EFSresources.EFSframeworkelements;
using EFSresources.EFSlive;
using EFSresources.EFSlive.SkyDrive;
using EFSresources.EFSlogging;
using Microsoft.Live;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TankManager.Lokalisierung;
using TankManager.Model;
using TankManager.ViewModel;

namespace TankManager.Views
{
	public partial class EinstellungenView : PhoneApplicationPage
	{
		private EinstellungenViewModel ViewModel;
		private LiveConnect Live;
		private SkyDriveClient SkyDrive;

		private IApplicationBar VehiclePivotAppBar;
		private ApplicationBarIconButton AppBarIconButtonAddVehicle;
		private ApplicationBarMenuItem AppBarIconButtonDeleteVehicles;

		private Boolean PivotGeneral_isLoaded = false;
		private Boolean InVehicleDeleting = false;
		private Boolean InLoggingOut = false;
		private DateTime tempBackupTime;

		public EinstellungenView()
		{
			InitializeComponent();

			this.ViewModel = (EinstellungenViewModel)this.Resources["ViewModel"];

			this.ToggleSwitch_ExitMessage.Click += new EventHandler<RoutedEventArgs>(ToggleSwitch_ExitMessage_Click);

			this.Init();

			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "EinstellungenView initialisiert");
		}

		#region Initialisierung
		private void Init()
        {
            this.ListPicker_KmCounter.ItemsSource = this.ViewModel.Zaehler;
			this.ListPicker_KmCounter.SelectedIndex = (Int32)this.ViewModel.Einstellungen.KmZaehler;
			this.Button_Info.Click += new RoutedEventHandler(Button_Info_Click);

			this.ToggleSwitch_ShowVehicleEditButton.IsChecked = this.ViewModel.Einstellungen.ShowVehicleEditButton;
			this.ToggleSwitch_ShowVehicleEditButton.Header = Translation.EinstellungenViewShowVehicleEditButton_Header;
			this.ToggleSwitch_ShowVehicleEditButton.Refresh(Translation.ToggleSwitchYes, Translation.ToggleSwitchNo);
			this.TS_ShowVehicleEditButton();

			this.ToggleSwitch_ExitMessage.IsChecked = this.ViewModel.Einstellungen.ShowExitMessage;
			this.ToggleSwitch_ExitMessage.Header = Translation.EinstellungenViewShowExitMessage;
			this.ToggleSwitch_ExitMessage.Refresh(Translation.ToggleSwitchYes, Translation.ToggleSwitchNo);
        }

		private void TS_ShowVehicleEditButton()
        {
			if ((Boolean)this.ToggleSwitch_ShowVehicleEditButton.IsChecked == true)
			{
				this.TextBlock_ShowVehicleEditButton.Visibility = Visibility.Visible;
			}
			else
			{
				this.TextBlock_ShowVehicleEditButton.Visibility = Visibility.Collapsed;
			}
        }
		#endregion

		#region Loaded-Ereignisse
		private void PivotItem_General_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			this.PivotGeneral_isLoaded = true;
		}

		private void PivotItem_Vehicles_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			this.AppBarIconButtonAddVehicle = new ApplicationBarIconButton() { IconUri = new Uri("/icons/appbar.add.rest.png", UriKind.RelativeOrAbsolute), Text = Translation.FahrzeugAddViewTitle };
			this.AppBarIconButtonAddVehicle.Click += new EventHandler(AppBarIconButtonAddVehicle_Click);

			this.AppBarIconButtonDeleteVehicles = new ApplicationBarMenuItem() { Text = Translation.MessageBoxDeleteAllVehiclesHeader };
			this.AppBarIconButtonDeleteVehicles.Click += new EventHandler(AppBarIconButtonDeleteVehicles_Click);
			
			this.VehiclePivotAppBar = new ApplicationBar();
			this.VehiclePivotAppBar.Buttons.Add(this.AppBarIconButtonAddVehicle);
			this.VehiclePivotAppBar.MenuItems.Add(this.AppBarIconButtonDeleteVehicles);

			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "VehiclePivotAppBar geladen");
		}

		private void PivotItem_Backup_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			this.LastBackup();

            if (this.SkyDrive != null)
            {
                if (this.SkyDrive.IsOperation)
                {
                    this.UpdateSkyDriveProgressBar(true);
                }
            }
		}
		#endregion

		#region SelectionChanged-Ereignisse
		private void PivotControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			this.ApplicationBar = null;

			if (this.PivotControl.SelectedItem == this.PivotItem_Vehicles)
			{
				this.ListBox_Vehicles.SelectedItem = this.ViewModel.FViewModel.AktuellesFahrzeug;
				this.ApplicationBar = this.VehiclePivotAppBar;
			}
		}

		private void ListBox_Vehicles_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (!this.InVehicleDeleting)
			{
				if (this.PivotControl.SelectedItem == this.PivotItem_Vehicles)
				{
					if (this.ViewModel.FViewModel.AktuellesFahrzeug != this.ListBox_Vehicles.SelectedItem && this.ListBox_Vehicles.SelectedItem != null)
					{
						App.ViewModel.AktuelleFahrzeugID = App.ViewModel.Fahrzeuge.IndexOf((FahrzeugModel)ListBox_Vehicles.SelectedItem);
						this.ViewModel.FViewModel.SetzeFahrzeug();
						EFSlogsystem.Logger.WriteInfo(this.GetType(), "Fahrzeug gewechselt", App.ViewModel.AktuelleFahrzeugID);
						MessageBox.Show(String.Format(Translation.MessageBoxVehicleChangedContent, this.ViewModel.FViewModel.AktuellesFahrzeug.Name), Translation.MessageBoxVehicleChangedHeader, MessageBoxButton.OK);
					}
				}
			}
		}

		private void ListPicker_KmCounter_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (this.PivotGeneral_isLoaded)
			{
				if (this.ViewModel.Einstellungen.KmZaehler != (Kilometerzaehler)this.ListPicker_KmCounter.SelectedIndex)
				{
					this.ViewModel.Einstellungen.KmZaehler = (Kilometerzaehler)this.ListPicker_KmCounter.SelectedIndex;
					EFSlogsystem.Logger.WriteInfo(this.GetType(), "Kilometerzähler geändert", this.ViewModel.Einstellungen.KmZaehler);
				}
			}
		}
		#endregion

		#region AppBar
		private void AppBarIconButtonAddVehicle_Click(object sender, EventArgs e)
		{
			NavigationService.Navigate(new Uri("/Views/FahrzeugAddView.xaml", UriKind.Relative));
		}

		private void AppBarIconButtonDeleteVehicles_Click(object sender, EventArgs e)
		{
			this.InVehicleDeleting = true;
			if (MessageBoxResult.OK == MessageBox.Show(Translation.MessageBoxDeleteAllVehiclesContent, Translation.MessageBoxDeleteAllVehiclesHeader, MessageBoxButton.OKCancel))
			{
				App.ViewModel.DeleteAllVehicles();
			}
			this.InVehicleDeleting = false;
		}
		#endregion

		#region SkyDrive

		private void LastBackup()
		{
			if (App.ViewModel.LetztesBackup.Ticks > 0)
			{
				this.TextBlock_LastBackup.Text = App.ViewModel.LetztesBackup.ToString();
				this.StackPanel_LastBackup.Visibility = Visibility.Visible;
			}
			else
			{
				this.StackPanel_LastBackup.Visibility = Visibility.Collapsed;
			}
		}

		private void ActivateSkyDrive(Boolean activated)
		{
			if (activated)
			{
				this.TextBlock_NoInternetConnection.Visibility = Visibility.Collapsed;

				this.Button_Signin.IsEnabled = true;
			}
			else
			{
				this.TextBlock_NoInternetConnection.Visibility = Visibility.Visible;

				this.Button_Signin.IsEnabled = false;

				this.UpdateSkyDriveProgressBar(false);
			}
		}

		private void UpdateSkyDrive(Boolean activated)
		{
			if (activated)
			{
				this.ProgressOverlay.Visibility = Visibility.Visible;
				this.PerformanceProgressBar.IsIndeterminate = true;
			}
			else
			{
				this.ProgressOverlay.Visibility = Visibility.Collapsed;
				this.PerformanceProgressBar.IsIndeterminate = false;
			}
		}

		private void UpdateSkyDriveProgressBar(Boolean activated)
		{
			if (activated)
			{
				this.Button_Backup.IsEnabled = false;
				this.Button_Restore.IsEnabled = false;

				this.ProgressBar_SkyDrive.IsIndeterminate = true;
				this.ProgressBar_SkyDrive.Opacity = 100;
			}
			else
			{
				this.Button_Backup.IsEnabled = true;
				this.Button_Restore.IsEnabled = true;

				this.ProgressBar_SkyDrive.IsIndeterminate = false;
				this.ProgressBar_SkyDrive.Opacity = 0;
			}
		}

		private void Button_Signin_SessionChanged(object sender, Microsoft.Live.Controls.LiveConnectSessionChangedEventArgs e)
		{
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Windows Live Session geändert");

			if (e.Error != null)
			{
				EFSlogsystem.Logger.WriteError(this.GetType(), "Fehler während der Live Verbindung");

				this.InLoggingOut = true;
				this.UpdateSkyDrive(false);

				String messageBox = Translation.MessageBoxLiveConnectFailed1Content;

				//if (SysException.getHResult(e.Error) == -2146233088)
				//{
				//    messageBox = String.Format(Translation.MessageBoxLiveConnectFailed1ContentReason, Translation.MessageBoxLiveConnectFailedReasonUserGranting);
				//    EFSlogsystem.Logger.WriteError(this.GetType(), "Benutzer hat der App nicht die Rechte gestattet", e.Error);
				//}
				//else if (SysException.getHResult(e.Error) == -2146233033)
				//{
				//    messageBox = String.Format(Translation.MessageBoxLiveConnectFailed1ContentReason, Translation.MessageBoxLiveConnectFailedReasonNoConnection);
				//    EFSlogsystem.Logger.WriteError(this.GetType(), "Keine Internetverbindung vorhanden", e.Error);
				//}

				if (this.PivotControl.SelectedItem == this.PivotItem_Backup)
				{
					MessageBox.Show(messageBox);
				}
			}
			else
			{
				if (e.Status == LiveConnectSessionStatus.Connected)
				{
					this.Live = new LiveConnect(e.Session);
					this.Live.Client.GetCompleted += new EventHandler<LiveOperationCompletedEventArgs>(this.OnConnectionComplete);
					this.Live.Client.GetAsync("me", null);

					EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Verbindungsaufbau zu Live gestartet");
				}
				else
				{
					this.InLoggingOut = true;
					this.UpdateSkyDrive(false);
					this.StackPanel_Connected.Visibility = Visibility.Collapsed;
					this.Live = null;
					this.SkyDrive = null;

					EFSlogsystem.Logger.WriteInfo(this.GetType(), "Verbindung zu Live getrennt");
				}
			}
		}

		private void OnConnectionComplete(object sender, LiveOperationCompletedEventArgs e)
		{
			this.UpdateSkyDrive(false);
			if (e.Error == null)
			{
				this.StackPanel_Connected.Visibility = Visibility.Visible;
				this.TextBlock_Welcome.Text = Translation.EinstellungenViewBackupConnectInfo;
				EFSlogsystem.Logger.WriteInfo(this.GetType(), "Verbindung zu Live hergestellt");

				if (this.SkyDrive == null)
				{
					this.SkyDrive = new SkyDriveClient(this.Live);
				}
			}
			else
			{
				MessageBox.Show(Translation.MessageBoxLiveConnectFailed3Content);
				EFSlogsystem.Logger.WriteError(this.GetType(), "Fehler beim Aufrufen der API aufgetreten", e.Error);
			}
		}

		private void Button_Signin_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (!this.InLoggingOut)
			{
				this.UpdateSkyDrive(true);
			}
			else
			{
				this.InLoggingOut = false;
			}
		}

		#region Backup
		private void Button_Backup_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (this.SkyDrive != null)
			{
				EFSlogsystem.Logger.WriteInfo(this.GetType(), "Backupvorgang eingeleitet");

				this.tempBackupTime = App.ViewModel.LetztesBackup;
				App.ViewModel.LetztesBackup = DateTime.Now;

				this.SkyDrive.UploadCompleted += new EventHandler<LiveOperationCompletedEventArgs>(BackupCompleted);
				this.SkyDrive.UploadFailed += new EventHandler<SkyDriveClientArgs>(BackupFailed);

				if (this.ViewModel.BackupToSkyDrive(this.SkyDrive))
				{
					this.UpdateSkyDriveProgressBar(true);
				}
				else
				{
					App.ViewModel.LetztesBackup = this.tempBackupTime;

					this.SkyDrive.UploadCompleted -= BackupCompleted;
					this.SkyDrive.UploadFailed -= BackupFailed;
				}
			}
		}

		private void BackupCompleted(object sender, LiveOperationCompletedEventArgs e)
		{
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Backup vollzogen");

			this.SkyDrive.UploadCompleted -= BackupCompleted;

			this.UpdateSkyDriveProgressBar(false);

			this.LastBackup();

			MessageBox.Show(Translation.MessageBoxSkyDriveBackupSuccess);
		}

		private void BackupFailed(object sender, SkyDriveClientArgs e)
		{
			EFSlogsystem.Logger.WriteWarning(this.GetType(), "Fehler bei der Backuperstellung");

			this.SkyDrive.UploadFailed -= BackupFailed;

			App.ViewModel.LetztesBackup = this.tempBackupTime;

			this.UpdateSkyDriveProgressBar(false);

			MessageBox.Show(Translation.MessageBoxSkyDriveBackupFailed);
		}
		#endregion

		#region Restore
		private void Button_Restore_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (MessageBoxResult.OK == MessageBox.Show(Translation.MessageBoxSkyDriveRestoreLegitimate, String.Empty, MessageBoxButton.OKCancel))
			{
				if (this.SkyDrive != null)
				{
					EFSlogsystem.Logger.WriteInfo(this.GetType(), "Wiederherstellungsvorgang eingeleitet");
					this.SkyDrive.DownloadCompleted += new EventHandler<LiveDownloadCompletedEventArgs>(RestoreCompleted);
					this.SkyDrive.DownloadFailed += new EventHandler<SkyDriveClientArgs>(RestoreFailed);

					if (this.ViewModel.RestoreFromSkyDrive(this.SkyDrive))
					{
						this.UpdateSkyDriveProgressBar(true);
					}
					else
					{
						this.SkyDrive.DownloadCompleted -= RestoreCompleted;
						this.SkyDrive.DownloadFailed -= RestoreFailed;
					}
				}
			}
			else
			{
				EFSlogsystem.Logger.WriteInfo(this.GetType(), "Wiederherstellungsvorgang abgebrochen");
			}
		}

		private void RestoreCompleted(object sender, LiveDownloadCompletedEventArgs e)
		{
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Wiederherstellung vollzogen");

			this.SkyDrive.DownloadCompleted -= RestoreCompleted;

			this.ViewModel.RestoreCompleted();
			this.Init();

			this.UpdateSkyDriveProgressBar(false);

			MessageBox.Show(Translation.MessageBoxSkyDriveRestoreSuccess);
		}

		private void RestoreFailed(object sender, SkyDriveClientArgs e)
		{
			EFSlogsystem.Logger.WriteWarning(this.GetType(), "Fehler bei der Wiederherstellung");

			this.SkyDrive.DownloadFailed -= RestoreFailed;

			this.UpdateSkyDriveProgressBar(false);

			switch (e.operationResult)
			{
				case OperationResult.PathNotExist:
				case OperationResult.FileNotExist:
					MessageBox.Show(Translation.MessageBoxSkyDriveRestoreFailedNotExist);
					break;
				case OperationResult.LiveError:
				case OperationResult.DownloadFailed:
				default:
					MessageBox.Show(Translation.MessageBoxSkyDriveRestoreFailed);
					break;
			}
		}
		#endregion

		#endregion

		#region Click-Ereignisse

		private void Button_Info_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show(Translation.InfoUCText);
		}

		private void MenuItem_Modify_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			FahrzeugModel selectedItem = ((FahrzeugModel)((MenuItem)sender).DataContext);

			App.NavigationObject = selectedItem;
			NavigationService.Navigate(new Uri("/Views/FahrzeugEditView.xaml", UriKind.Relative));
		}

		private void MenuItem_Delete_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			FahrzeugModel selectedItem = ((FahrzeugModel)((MenuItem)sender).DataContext);

			MessageBoxResult mBoxResult = MessageBoxResult.OK;

			if (selectedItem == this.ViewModel.FViewModel.AktuellesFahrzeug)
			{
				mBoxResult = MessageBox.Show(Translation.MessageBoxCurrentVehicleDelContent, Translation.MessageBoxCurrentVehicleDelHeader, MessageBoxButton.OKCancel);
			}

			this.InVehicleDeleting = true;
			if (mBoxResult == MessageBoxResult.OK)
			{
				App.ViewModel.DeleteVehicle(selectedItem);
				MessageBox.Show(String.Format(Translation.MessageBoxVehicleDelContent, selectedItem.Name), Translation.MessageBoxVehicleDelHeader, MessageBoxButton.OK);
			}
			this.InVehicleDeleting = false;
		}

		private void ToggleSwitch_ShowVehicleEditButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.ToggleSwitch_ShowVehicleEditButton.Refresh(Translation.ToggleSwitchYes, Translation.ToggleSwitchNo);
			this.TS_ShowVehicleEditButton();

			this.ViewModel.Einstellungen.ShowVehicleEditButton = this.ToggleSwitch_ShowVehicleEditButton.IsChecked.Value;
		}

		void ToggleSwitch_ExitMessage_Click(object sender, RoutedEventArgs e)
		{
			this.ToggleSwitch_ExitMessage.Refresh(Translation.ToggleSwitchYes, Translation.ToggleSwitchNo);
			this.ViewModel.Einstellungen.ShowExitMessage = this.ToggleSwitch_ExitMessage.IsChecked.Value;
		}
		#endregion


	}
}