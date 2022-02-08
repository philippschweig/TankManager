using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using EFSresources.EFSmvvm;
using TankManager.Model;
using EFSresources.EFSlogging;
using EFSresources.Library;
using EFSresources.App.Update;

namespace TankManager.ViewModel
{
    public class MainViewModel : ViewModelBase, IUpdate
	{
        public bool WasUpdated = false;
        private DateTime _appVersion = DateTime.MinValue;
        public DateTime AppVersion 
        {
            get { return _appVersion; }
            set
            {
                if (_appVersion != value)
                {
                    _appVersion = value;
                }
            }
        }

		private EinstellungenModel _userEinstellungen;
		public EinstellungenModel UserEinstellungen
		{
			get { return _userEinstellungen; }
			set
			{
				if (_userEinstellungen != value)
				{
					_userEinstellungen = value;
				}
			}
		}

		private ObservableCollection<FahrzeugModel> _fahrzeuge;
		public ObservableCollection<FahrzeugModel> Fahrzeuge
		{
			get { return _fahrzeuge; }
			set
			{
				if (_fahrzeuge != value)
				{
					_fahrzeuge = value;
				}
			}
		}

		private DateTime _letztesBackup = new DateTime(0);
		public DateTime LetztesBackup
		{
			get
			{
				return _letztesBackup;
			}
			set
			{
				if (value != _letztesBackup)
				{
					_letztesBackup = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("LetztesBackup");
				}
			}
		}

		// ID das aktuellen Fahrzeuges
		public Int32 AktuelleFahrzeugID = -1;

		public MainViewModel()
		{
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "MainViewModel initialisert");
		}

		public void Load()
        {
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "MainViewModel laden");
			this.LoadSettings();
			this.LoadData();
        }

		public void LoadSettings()
        {
			if (this.UserEinstellungen == null)
			{
				this.UserEinstellungen = new EinstellungenModel();
				this.UserEinstellungen.setLanguageFromSystem();

				EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Standard-Einstellungen geladen", this.UserEinstellungen);
			}
        }

		public void LoadData()
        {
			if (this.Fahrzeuge == null)
			{
				this.Fahrzeuge = new ObservableCollection<FahrzeugModel>();
				EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Leere Fahrzeugliste angelegt");
			}
        }

		public void AddVehicle(KennzeichenModel _kennzeichen, String _name, Int32 _gesamtkm, SpritModel _sprit, Boolean _zähleMitTageskilometer)
        {
			this.Fahrzeuge.Add(new FahrzeugModel() { Kennzeichen = _kennzeichen, Name = _name, Gesamtkilometer = _gesamtkm, Sprit = _sprit, ZähleMitTageskilometer = _zähleMitTageskilometer });
			this.AktuelleFahrzeugID = this.Fahrzeuge.Count - 1;
        }

		public void DeleteVehicle(FahrzeugModel _vehicle)
		{
			if (this.Fahrzeuge.IndexOf(_vehicle) == this.AktuelleFahrzeugID)
			{
				this.AktuelleFahrzeugID = -1;
			}
			else if (this.Fahrzeuge.IndexOf(_vehicle) < this.AktuelleFahrzeugID)
			{
				this.AktuelleFahrzeugID--;
			}

			this.Fahrzeuge.Remove(_vehicle);

			EFSlogsystem.Logger.WriteInfo(this.GetType(), "Fahrzeug gelöscht", _vehicle);
		}

		public void DeleteAllVehicles()
        {
            this.AktuelleFahrzeugID = -1;

			this.Fahrzeuge.Clear();

			EFSlogsystem.Logger.WriteInfo(this.GetType(), "Alle Fahrzeuge gelöscht");
        }

        public void Update(bool silent = false)
        {
            if (this.IsUpdateable())
            {
                foreach (var item in this.Fahrzeuge)
                {
                    item.Upgrade();
                }

                this.WasUpdated = !silent;
                this.AppVersion = AppInfo.BuildDateTime;
            }
            else if (this.AppVersion == DateTime.MinValue)
            {
                this.AppVersion = AppInfo.BuildDateTime;
            }
        }

        public bool IsUpdateable()
        {
            if (this.AppVersion < AppInfo.BuildDateTime && this.AppVersion != DateTime.MinValue)
            {
                return true;
            }

            return false;
        }
	}
}