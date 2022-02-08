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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EFSresources.EFSlogging;
using EFSresources.EFSmvvm;

namespace TankManager.Model
{
	public class FahrzeugModel : ModelBase
	{
		// Eigenschaften
		private KennzeichenModel _kennzeichen;
		public KennzeichenModel Kennzeichen
		{
			get
			{
				return _kennzeichen;
			}
			set
			{
				if (value != _kennzeichen)
				{
					_kennzeichen = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Kennzeichen");
				}
			}
		}

		private String _name;
		public String Name
		{
			get
			{
				return _name;
			}
			set
			{
				if (value != _name)
				{
					_name = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Name");
				}
			}
		}

		private Int32 _gesamtkilometer;
		public Int32 Gesamtkilometer
		{
			get { return /*(Int32)Math.Round((Decimal)Einstellungen.Einheitensystem().Length2.Format(_gesamtkilometer))*/ _gesamtkilometer; }
			set
			{
				//Int32 temp = (Int32)Math.Round((Decimal)Einstellungen.Einheitensystem().Length2.FormatBack(value));
				if (_gesamtkilometer != value)
				{
					_gesamtkilometer = value;
					NotifyPropertyChanged("Gesamtkilometer");
				}
			}
		}

		private StatistikModel _statistik;
		public StatistikModel Statistik
		{
			get
			{
				if (_statistik == null)
				{
					_statistik = new StatistikModel();
				}
				return _statistik;
			}
			set
			{
				if (value != _statistik)
				{
					_statistik = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Statistik");
				}
			}
		}

		private SpritModel _sprit;
		public SpritModel Sprit
		{
			get
			{
				return _sprit;
			}
			set
			{
				if (value != _sprit)
				{
					_sprit = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Sprit");
				}
			}
		}

		public Boolean ZähleMitTageskilometer { get; set; }

		private FahrzeugExtrasModel _extras;
		public FahrzeugExtrasModel Extras
		{
			get
			{
				return _extras;
			}
			set
			{
				if (value != _extras)
				{
					_extras = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Extras");
				}
			}
		}

		private ObservableCollection<TankvorgangModel> _tankverlauf;
		public ObservableCollection<TankvorgangModel> Tankverlauf
		{
			get
			{
				if (_tankverlauf == null)
				{
					_tankverlauf = new ObservableCollection<TankvorgangModel>();
				}

				return _tankverlauf;
			}
			set
			{
				if (_tankverlauf != value)
				{
					_tankverlauf = value;
					NotifyPropertyChanged("Tankverlauf");
				}
			}
		}

		// Konstruktoren
		public FahrzeugModel() { EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Fahrzeug initialisiert"); }

		public FahrzeugModel(KennzeichenModel _kennzeichen, String _name, Int32 _gesamtkm, SpritModel _sprit, Boolean _zähleMitTageskilometer)
		{
			this.Kennzeichen = _kennzeichen;
			this.Name = _name;
			this.Gesamtkilometer = _gesamtkm;
			this.Sprit = _sprit;
			this.ZähleMitTageskilometer = _zähleMitTageskilometer;

			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Fahrzeug initialisiert", this);
		}

		public FahrzeugModel(KennzeichenModel _kennzeichen, String _name, Int32 _gesamtkm, SpritModel _sprit) : this(_kennzeichen, _name, _gesamtkm, _sprit, false) { }

		// Methoden
		public void Tanken(Double _km, Double _liter, Double _preis, DateTime _datum, SpritQuelleModel _spritQuelle, Boolean _vollgetankt, Boolean _tageskilometerZaehler)
		{
			List<TankvorgangModel> liste;
			Double tempLiter = 0;
			Boolean init = false;

			// Normales Tanken
			if (this.Tankverlauf.Count > 0)
			{
				// Bei Volltankung
				if (_vollgetankt)
				{
					// Wenn beim letzten Mal nicht vollgetankt wurde
					if (!this.LetzterTankvorgang().Vollgetankt)
					{
						liste = this.LetztenTankvorgängeNachgetankt();

						foreach (var item in liste)
						{
							tempLiter += item.Liter;
						}

						this.Gesamtkilometer = this.LetzterTankvorgang(true).GesamtKilometer;
					}

					if (_tageskilometerZaehler)
					{
						// Neuer Gesamtkilometerstand
						this.Gesamtkilometer += (Int32)_km;
					}
					else
					{
						// Alter Gesamtkilometerstand
						Int32 temp = this.Gesamtkilometer;
						// Neuer Gesamtkilometerstand
						this.Gesamtkilometer = (Int32)_km;
						// Errechnete Tageskilometer
						_km = this.Gesamtkilometer - temp;
					}
				}
				// Bei Nachtankung
				else
				{
					this.Gesamtkilometer = (Int32)_km;
					_km = 0;
				}
			}
			// Initialisierungs Tanken
			else
			{
				// Initalisiere Gesamtkilometerstand
				this.Gesamtkilometer = (Int32)_km;
				// Setzte gefahrene Kilometer auf 0
				_km = 0;
				init = true;
			}

			this.Tankverlauf.Add(new TankvorgangModel(_datum, this.Gesamtkilometer, _km, this.Sprit, _liter, _preis, _spritQuelle, _vollgetankt, tempLiter, init));

			this.AktualisiereStatistik();

			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Tankvorgang hinzugefügt");
		}

		public void Tanken(Double _km, Double _liter, Double _preis, DateTime _datum, SpritQuelleModel _spritQuelle, Boolean _vollgetankt)
		{
			this.Tanken(_km, _liter, _preis, _datum, _spritQuelle, _vollgetankt, this.ZähleMitTageskilometer);
		}

		public void Tanken(Double _km, Double _liter, Double _preis, SpritQuelleModel _spritQuelle, Boolean _vollgetankt, Boolean _tageskilometerZaehler)
		{
			this.Tanken(_km, _liter, _preis, DateTime.Now, _spritQuelle, _vollgetankt, _tageskilometerZaehler);
		}

		public void Tanken(Double _km, Double _liter, Double _preis, SpritQuelleModel _spritQuelle, Boolean _vollgetankt)
		{
			this.Tanken(_km, _liter, _preis, DateTime.Now, _spritQuelle, _vollgetankt, this.ZähleMitTageskilometer);
		}

		private void AktualisiereStatistik(Boolean reset = false)
		{
			if (reset)
			{
				this.Statistik.Reset();
			}

			this.Statistik.Aktualisiere(this.Tankverlauf);
		}

		public TankvorgangModel TankvorgangVor(TankvorgangModel tv)
		{
			if (this.Tankverlauf.Contains(tv) && this.Tankverlauf.IndexOf(tv) > 0)
			{
				return this.Tankverlauf[this.Tankverlauf.IndexOf(tv) - 1];
			}

			return null;
		}

		public TankvorgangModel TankvorgangNach(TankvorgangModel tv)
		{
			if (this.Tankverlauf.Contains(tv) && this.Tankverlauf.IndexOf(tv) < this.Tankverlauf.Count - 1)
			{
				return this.Tankverlauf[this.Tankverlauf.IndexOf(tv) + 1];
			}

			return null;
		}

		public TankvorgangModel LetzterTankvorgang(Boolean IsVollgetankt = false)
		{
			if (this.Tankverlauf.Count > 0)
			{
				if (IsVollgetankt)
				{
					for (int i = this.Tankverlauf.Count - 1; i >= 0; i--)
					{
						TankvorgangModel tv = this.Tankverlauf[i];

						if (tv.Vollgetankt)
						{
							return tv;
						}
					}
				}

				return this.Tankverlauf[this.Tankverlauf.Count - 1];
			}

			return null;
		}

		private List<TankvorgangModel> LetztenTankvorgängeNachgetankt(Int32 from = -1)
		{
			if (this.Tankverlauf.Count > 0)
			{
				List<TankvorgangModel> liste = new List<TankvorgangModel>();

				if (from < 0)
				{
					from = this.Tankverlauf.Count - 1;
				}

				for (int i = from; i >= 0; i--)
				{
					TankvorgangModel tv = this.Tankverlauf[i];

					if (!tv.Vollgetankt)
					{
						liste.Add(tv);
					}
					else
					{
						break;
					}
				}

				return liste;
			}

			return null;
		}

		public bool IstLetzterTankvorgangVollgetankt()
		{
			if (this.LetzterTankvorgang().Vollgetankt)
			{
				return true;
			}
			return false;
		}

		public TankenOperation TankvorgangÄndern(TankvorgangModel tv, DateTime dt, Double liter, Double kosten)
		{
			Boolean validDateTimeArea = true;
			TankvorgangModel
				tvVor = this.TankvorgangVor(tv),
				tvNach = this.TankvorgangNach(tv);

			if (tvVor != null)
			{
				if (tv.Datum.Ticks < tvVor.Datum.Ticks)
				{
					validDateTimeArea = false;
				}
			}

			if (tvNach != null)
			{
				if (tv.Datum.Ticks > tvNach.Datum.Ticks)
				{
					validDateTimeArea = false;
				}
			}

			if (validDateTimeArea)
			{
				Double tempLiter = 0;

				// Wenn beim letzten Mal nicht vollgetankt wurde
				if (tvVor != null)
				{
					if (!tvVor.Vollgetankt)
					{
						List<TankvorgangModel> liste = this.LetztenTankvorgängeNachgetankt(this.Tankverlauf.IndexOf(tvVor));

						foreach (var item in liste)
						{
							tempLiter += item.Liter;
						}
					}
				}

				tv.Datum = dt;
				tv.Liter = liter;
				tv.Preis = kosten;

				tv.Aktualisiere(tempLiter);

				this.AktualisiereStatistik(true);

				EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Tankvorgang geändert");

				return TankenOperation.Success;
			}

			EFSlogsystem.Logger.WriteWarning(this.GetType(), "Tankvorgang nicht geändert");
			
			return TankenOperation.NotInDateTimeArea;
		}

		public bool TankvorgangLöschen(TankvorgangModel tv)
        {
			if (this.Tankverlauf.Count > 0)
			{
				if (this.Tankverlauf[this.Tankverlauf.Count - 1] == tv)
				{
					this.Tankverlauf.Remove(tv);

					TankvorgangModel last_tv = this.LetzterTankvorgang();

					if (last_tv != null)
					{
						this.Gesamtkilometer = last_tv.GesamtKilometer;
					}

					this.AktualisiereStatistik(true);

					EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Tankvorgang gelöscht");

					return true;
				}
			}

			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Tankvorgang nicht gelöscht");

			return false;
        }

		public bool LetztenTankvorgangLöschen()
		{
			return this.TankvorgangLöschen(this.LetzterTankvorgang());
		}

		public void TankverlaufLöschen()
		{
			this.Tankverlauf = null;

			this.AktualisiereStatistik(true);


			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Tankverlauf gelöscht");
		}

		public void Upgrade()
		{
            this.AktualisiereStatistik(true);
		}
	}

	public enum TankenOperation { Success, Failed, NotInDateTimeArea }
}
