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
using EFSresources.EFSlogging;
using System.Globalization;
using System.Threading;
using System.IO.IsolatedStorage;
using EFSresources.EFSconverter;
using Microsoft.Live;

namespace TankManager.Model
{
	public class EinstellungenModel : ModelBase
	{
		// GPS-Ortung
		public Boolean GPSortung = false;
		// Wahrheitswert für den Erststart der Anwendung
		public Boolean ErsterStart = true;
		// Aktuelle eingestelle Sprache der Anwendung
		public String Sprache;
		//public String Sprache = "en-US";
		public Kilometerzaehler KmZaehler = Kilometerzaehler.Automatic;
		public Einheitensysteme Einheiten;

		// Ansicht-Einstellungen
		public Boolean ShowVehicleEditButton = false;
		public Boolean ShowExitMessage = true;

		public void setLanguageFromSystem()
		{
			this.Sprache = CultureInfo.CurrentCulture.Name;
			//this.Sprache = "en-US";
			this.selectUnits();
		}

		public void selectUnits()
        {
			if (this.Sprache == "en-US")
			{
				this.Einheiten = Einheitensysteme.ImperialUS;
			}
			// Standard: de-DE
			else
			{
				this.Einheiten = Einheitensysteme.Metric;
			}

			EFSlogsystem.Logger.WriteInfo(this.GetType(), "Einheitensystem festgelegt");
        }

		public static UnitSystem Einheitensystem()
        {
			switch (App.ViewModel.UserEinstellungen.Einheiten)
			{
				case Einheitensysteme.Metric:
					return UnitSystem.Metrical;
				case Einheitensysteme.ImperialUS:
					return UnitSystem.ImperialUS;
				case Einheitensysteme.ImperialUK:
					return UnitSystem.ImperialUK;
				default:
					return UnitSystem.Metrical;
			}
        }
	}

	public enum Einheitensysteme { Metric, ImperialUS, ImperialUK }
	public enum Kilometerzaehler { Automatic, Individual }
}
