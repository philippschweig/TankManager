using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using EFSresources.EFSmvvm;
using System.Windows.Data;
using EFSresources.EFSlogging;
using System.ComponentModel;
using System.Collections.ObjectModel;
using TankManager.Model;
using TankManager.Lokalisierung;
using EFSresources.EFSlive.SkyDrive;
using System.IO.IsolatedStorage;
using EFSresources.EFSdatastorage;
using System.IO;

namespace TankManager.ViewModel
{
	public class EinstellungenViewModel : ViewModelBase
	{
		private CollectionViewSource _sortedFahrzeuge;
		public CollectionViewSource SortedFahrzeuge
		{
			get { return _sortedFahrzeuge; }
			private set
			{
				if (_sortedFahrzeuge != value)
				{
					_sortedFahrzeuge = value;
					NotifyPropertyChanged("SortedFahrzeuge");
				}
			}
		}

		private ObservableCollection<String> _zaehler;
		public ObservableCollection<String> Zaehler
		{
			get { return _zaehler; }
			private set
			{
				if (_zaehler != value)
				{
					_zaehler = value;
					NotifyPropertyChanged("Zaehler");
				}
			}
		}

		public FahrzeugViewModel FViewModel;

		public EinstellungenModel Einstellungen
		{
			get { return App.ViewModel.UserEinstellungen; }
		}

		public EinstellungenViewModel()
		{
			this.Load();
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "EinstellungenViewModel initialisert");
		}

		public void Load()
        {
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "EinstellungenViewModel laden");
			this.LoadFahrzeuge();
			this.LoadZaehler();
			this.FViewModel = new FahrzeugViewModel();
        }

		public void ReLoad()
		{
			this.Load();
			NotifyPropertyChanged("Einstellungen");
			NotifyPropertyChanged("FViewModel");
		}

		public void LoadFahrzeuge()
        {
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Fahrzeuge geladen");
			this.SortedFahrzeuge = new CollectionViewSource();
			//this.SortedFahrzeuge.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
			this.SortedFahrzeuge.Source = App.ViewModel.Fahrzeuge;
        }

		public void LoadZaehler()
		{
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Zähler laden");
			this.Zaehler = new ObservableCollection<String>();
			this.Zaehler.Add(Translation.EnumKilometerzaeherAutomatic);
			this.Zaehler.Add(Translation.EnumKilometerzaeherIndividual);
		}

		public Boolean BackupToSkyDrive(SkyDriveClient skydrive)
		{
			((App)App.Current).SaveToIsolatedStorage();

			IsolatedStorageFileStream file = DataStorage.ReadFile(App.dataFile);

			EFSlogsystem.Logger.WriteInfo(this.GetType(), "SkyDrive Backup gestartet");

			return skydrive.UploadFile(file, "UserData.txt", App.skydrivePath);
		}

		public Boolean RestoreFromSkyDrive(SkyDriveClient skydrive)
		{
			EFSlogsystem.Logger.WriteInfo(this.GetType(), "SkyDrive Restore gestartet");

			IsolatedStorageFileStream file = DataStorage.WriteFile(App.dataFile);
			return skydrive.DownloadFile(file, "UserData.txt", App.skydrivePath, false);
		}

		public void RestoreCompleted()
		{
			((App)App.Current).LoadFromIsolatedStorage();
			this.ReLoad();
            App.ViewModel.Update(true);
		}
	}
}
