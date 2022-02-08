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
using System.Collections.ObjectModel;
using TankManager.Model;

namespace TankManager.ViewModel
{
	public class HistoryViewModel : ListItemViewModel
	{
		public HistoryViewModel() { }

		public void Load()
		{
			if (!this._isLoaded)
			{
				EFSlogsystem.Logger.WriteInfo(this.GetType(), "History laden");

				this.Items = new ObservableCollection<ListItemModel>();
                #region Public Release
				this.Items.Add(new ListItemModel()
				{
					Title = "1.5",
					Contents = new HistoryList<string>() {
						"Änderung: Man muss nun nach einer Teilbetankung immer den Gesamtkilometerstand angeben. Dadurch wird der Verbrauch wieder richtig berechnet."
				    }
				});
                this.Items.Add(new ListItemModel()
                {
                    Title = "1.4",
                    Contents = new HistoryList<string>() {
						"Änderung: Punkt wird ab sofort wie ein Komma behandelt",
						"Behoben: Anwendungsabsturz, nachdem man den aller letzten Eintrag über die Änderungsansicht gelöscht hat",
						"Behoben: Neuberechnung des Literpreises, wenn der Rechnungsbetrag geändert wurde"
				    }
                });
                this.Items.Add(new ListItemModel()
                {
                    Title = "1.3",
                    Contents = new HistoryList<string>() {
						"Änderung: Einige Designverbesserungen"
				    }
                });
				this.Items.Add(new ListItemModel()
				{
					Title = "1.2",
                    Contents = new HistoryList<string>() {
						"Änderung: Hilfe überarbeitet",
						"Änderung: Einige Designverbesserungen",
                        "Änderung: Einige Veränderungen in der Sprache (Deutsch)",
				        "Behoben: Falsche Verbrauchsberechnung bei Teilbetankungen"
				    }
				});

				this.Items.Add(new ListItemModel()
				{
					Title = "1.1",
					Contents = new HistoryList<string>() {
				        "Änderung: Ladeverbesserungen im Hilfesystem",
						"Änderung: Einige Designverbesserungen"
				    }
				});

				this.Items.Add(new ListItemModel()
				{
					Title = "1.0",
					Contents = new HistoryList<string>() {
				        "Marketplace Erstveröffentlichung"
				    }
				});
				#endregion

				this._isLoaded = true;
			}
		}
	}
}
