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
using EFSresources.EFSlogging;
using EFSresources.EFSmvvm;

namespace TankManager.Model
{
	public class TankvorgangModel : ModelBase
	{
		public DateTime ErstelltAm { get; set; }
		public DateTime Datum { get; set; }
		public Int32 GesamtKilometer { get; set; }
		public Double TagesKilometer { get; set; }
		public Double Liter { get; set; }
		public Double Preis { get; set; }
		public Double PreisProLiter { get; set; }
		public Double Verbrauch { get; set; }
		public SpritModel Sprit { get; set; }
		public Boolean Init { get; set; }
		public Boolean Vollgetankt { get; set; }
		public SpritQuelleModel SpritQuelle { get; set; }
		// GPS-Koordinaten

		public TankvorgangModel() { }

		// _liter2: Literzahl für vorherige Tankungen, bei denen nicht vollgetankt wurde
		public TankvorgangModel(DateTime _datum, Int32 _gesamtkm, Double _tageskm, SpritModel _sprit, Double _liter, Double _preis, SpritQuelleModel _spritQuelle, Boolean _vollgetankt, Double _liter2 = 0, Boolean _init = false)
		{
			this.ErstelltAm = DateTime.Now;

			this.Datum = _datum;
			this.GesamtKilometer = _gesamtkm;
			this.TagesKilometer = _tageskm;
			this.Liter = _liter;
			this.Sprit = _sprit;
			this.Vollgetankt = _vollgetankt;
			this.Init = _init;
			this.SpritQuelle = _spritQuelle;

			if (this.SpritQuelle == SpritQuelleModel.Zapfsaeule)
			{
				this.Preis = _preis;
				this.PreisProLiter = this.Preis / this.Liter;
			}
			else
			{
				this.Preis = 0;
				this.PreisProLiter = 0;
			}


			if (this.Vollgetankt && TagesKilometer != 0)
			{
				if (_liter2 != 0)
				{
					_liter2 += this.Liter;
				}
				else
				{
					_liter2 = this.Liter;
				}
				this.Verbrauch = _liter2 * 100 / this.TagesKilometer;
			}

			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Tankvorgang initialisiert");
		}

		public void Aktualisiere(Double tempLiter = 0)
		{
			if (this.SpritQuelle.Id == SpritQuelleModel.Zapfsaeule.Id)
			{
				this.PreisProLiter = this.Preis / this.Liter;
			}

			if (this.Vollgetankt && TagesKilometer != 0)
			{
				if (tempLiter != 0)
				{
					this.Verbrauch = (this.Liter + tempLiter) * 100 / this.TagesKilometer;
				}
				else
				{
					this.Verbrauch = this.Liter * 100 / this.TagesKilometer;
				}
			}

			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Tankvorgang aktualisiert");
		}
	}
}
