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
using System.Collections.ObjectModel;
using EFSresources.EFSlogging;

namespace TankManager.Model
{
	public class StatistikModel : ModelBase
	{
		public bool firstInit = true;

		private Double _durchschnittsVerbrauch;
		public Double DurchschnittsVerbrauch
		{
			get { return _durchschnittsVerbrauch; }
			set
			{
				if (_durchschnittsVerbrauch != value)
				{
					_durchschnittsVerbrauch = value;
					NotifyPropertyChanged("DurchschnittsVerbrauch");
				}
			}
		}

		private Double _hoechsterVerbrauch;
		public Double HoechsterVerbrauch
		{
			get { return _hoechsterVerbrauch; }
			set
			{
				if (_hoechsterVerbrauch != value)
				{
					_hoechsterVerbrauch = value;
					NotifyPropertyChanged("HoechsterVerbrauch");
				}
			}
		}

		private Double _niedrigsterVerbrauch;
		public Double NiedrigsterVerbrauch
		{
			get { return _niedrigsterVerbrauch; }
			set
			{
				if (_niedrigsterVerbrauch != value)
				{
					_niedrigsterVerbrauch = value;
					NotifyPropertyChanged("NiedrigsterVerbrauch");
				}
			}
		}

		private Double _gesamtKosten;
		public Double GesamtKosten
		{
			get { return _gesamtKosten; }
			set
			{
				if (value != _gesamtKosten)
				{
					_gesamtKosten = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("GesamtKosten");
				}
			}
		}

		private Double _gesamtliter;
		public Double GesamtLiter
		{
			get { return _gesamtliter; }
			set
			{
				if (_gesamtliter != value)
				{
					_gesamtliter = value;
					NotifyPropertyChanged("GesamtLiter");
				}
			}
		}

		public StatistikModel() { }

		public void Aktualisiere(ObservableCollection<TankvorgangModel> tankverlauf)
        {
			this.firstInit = false;
			this.GesamtKosten = 0;
			this.GesamtLiter = 0;

            Double durchschnitt = 0;
			Int32 x = 0;

			//this.Reset();

			foreach (TankvorgangModel item in tankverlauf)
			{
				this.GesamtLiter += item.Liter;

				if (item.Vollgetankt && item.TagesKilometer > 0)
				{
					x++;
					durchschnitt += item.Verbrauch;

					if (item.Verbrauch > this.HoechsterVerbrauch)
					{
						this.HoechsterVerbrauch = item.Verbrauch;
					}

					if (this.NiedrigsterVerbrauch == 0 || item.Verbrauch < this.NiedrigsterVerbrauch)
					{
						this.NiedrigsterVerbrauch = item.Verbrauch;
					}
				}

				if (item.SpritQuelle.Id == SpritQuelleModel.Zapfsaeule.Id)
				{
					this.GesamtKosten += item.Preis;
				}
			}

			if (x > 0)
			{
				this.DurchschnittsVerbrauch = durchschnitt / x;
			}
			else
			{
				this.DurchschnittsVerbrauch = 0;
				this.HoechsterVerbrauch = 0;
				this.NiedrigsterVerbrauch = 0;
			}

			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Statistik aktualisiert");
        }

		public void Reset()
		{
			this.DurchschnittsVerbrauch = 0;
			this.NiedrigsterVerbrauch = 0;
			this.HoechsterVerbrauch = 0;
			this.GesamtKosten = 0;
			this.GesamtLiter = 0;

			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Statistik zurückgesetzt");
		}
	}
}
