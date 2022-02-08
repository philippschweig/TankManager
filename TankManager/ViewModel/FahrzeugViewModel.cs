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
using TankManager.Model;
using EFSresources.EFSmvvm;
using EFSresources.EFSlogging;
using System.Diagnostics;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TankManager.ViewModel
{
	public class FahrzeugViewModel : ViewModelBase
	{
		private FahrzeugModel _aktuellesFahrzeug = null;
		public FahrzeugModel AktuellesFahrzeug
		{
			get { return _aktuellesFahrzeug; }
			set
			{
				if (_aktuellesFahrzeug != value)
				{
					_aktuellesFahrzeug = value;
					NotifyPropertyChanged("AktuellesFahrzeug");
					NotifyPropertyChanged("Tankverlauf");
					NotifyPropertyChanged("TankverlaufDesc");
					NotifyPropertyChanged("TankverlaufLast5");
					NotifyPropertyChanged("TankverlaufLast10");
				}
			}
		}

		public CollectionViewSource Tankverlauf
		{
			get
			{
				CollectionViewSource temp = new CollectionViewSource();

				if (this.AktuellesFahrzeug != null)
				{
					temp = this.SortTankVerlauf(this.AktuellesFahrzeug.Tankverlauf, ListSortDirection.Ascending);
				}

				return temp;
			}
		}

		public CollectionViewSource TankverlaufDesc
		{
			get
			{
				CollectionViewSource temp = new CollectionViewSource();

				if (this.AktuellesFahrzeug != null)
				{
					temp = this.SortTankVerlauf(this.AktuellesFahrzeug.Tankverlauf, ListSortDirection.Descending);
				}

				return temp;
			}
		}

		private CollectionViewSource _tankverlaufLast5;
		public CollectionViewSource TankverlaufLast5
		{
			get
			{
				return _tankverlaufLast5;
			}
			set
			{
				if (value != _tankverlaufLast5)
				{
					_tankverlaufLast5 = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("TankverlaufLast5");
				}
			}
		}

		private CollectionViewSource _tankverlaufLast10;
		public CollectionViewSource TankverlaufLast10
		{
			get
			{
				return _tankverlaufLast10;
			}
			set
			{
				if (value != _tankverlaufLast10)
				{
					_tankverlaufLast10 = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("TankverlaufLast10");
				}
			}
		}

		private CollectionViewSource _tankverlaufZeitraum;
		public CollectionViewSource TankverlaufZeitraum
		{
			get
			{
				if (_tankverlaufZeitraum == null)
				{
					return this.Tankverlauf;
				}

				return _tankverlaufZeitraum;
			}

			private set
			{
				this._tankverlaufZeitraum = value;
				NotifyPropertyChanged("TankverlaufZeitraum");
			}
		}

		public FahrzeugViewModel()
        {
			this.Load();
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "FahrzeugViewModel initialisiert");
        }

		public void Load()
        {
			this.SetzeFahrzeug();
        }

		public void SetzeFahrzeug()
		{
			if (App.ViewModel.AktuelleFahrzeugID >= 0)
			{
				if (this.AktuellesFahrzeug != App.ViewModel.Fahrzeuge[App.ViewModel.AktuelleFahrzeugID])
				{
					this.AktuellesFahrzeug = App.ViewModel.Fahrzeuge[App.ViewModel.AktuelleFahrzeugID];
					EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Fahrzeug ausgewählt");
				}
			}
			else
			{
				this.AktuellesFahrzeug = null;
				EFSlogsystem.Logger.WriteWarning(this.GetType(), "Kein Fahrzeug ausgewählt");
			}
		}

		private CollectionViewSource SortTankVerlauf(IList<TankvorgangModel> tankverlauf, ListSortDirection dir)
		{
			CollectionViewSource temp = new CollectionViewSource();
			temp.SortDescriptions.Add(new SortDescription("Datum", dir));
			temp.SortDescriptions.Add(new SortDescription("GesamtKilometer", dir));

			temp.Source = tankverlauf;

			return temp;
		}

		public void RefreshTankverlaufLast5()
		{
			ObservableCollection<TankvorgangModel> collection = new ObservableCollection<TankvorgangModel>();

			if (this.AktuellesFahrzeug != null)
			{
				for (int i = this.AktuellesFahrzeug.Tankverlauf.Count - 1, a = 0; i >= 0 && a < 5; i--, a++)
				{
					collection.Add(this.AktuellesFahrzeug.Tankverlauf[i]);
				}
			}

			this.TankverlaufLast5 = this.SortTankVerlauf(collection, ListSortDirection.Descending);
		}

		public void RefreshTankverlaufLast10()
        {
			ObservableCollection<TankvorgangModel> collection = new ObservableCollection<TankvorgangModel>();

			if (this.AktuellesFahrzeug != null)
			{
				for (int i = this.AktuellesFahrzeug.Tankverlauf.Count - 1, a = 0; i >= 0 && a < 10; i--, a++)
				{
					collection.Add(this.AktuellesFahrzeug.Tankverlauf[i]);
				}
			}

			this.TankverlaufLast10 = this.SortTankVerlauf(collection, ListSortDirection.Descending);
        }

		private DateTime _von;
		private DateTime _bis;

		public void RefreshTankverlaufZeitraum(DateTime von, DateTime bis, Boolean Asc = true)
		{
			this._von = von;
			this._bis = bis;

			CollectionViewSource temp = this.Tankverlauf;

			if (!Asc)
			{
				temp = this.TankverlaufDesc;
			}

			temp.Filter += new FilterEventHandler(temp_Filter);

			this.TankverlaufZeitraum = temp;
		}

		private void temp_Filter(object sender, FilterEventArgs e)
		{
			TankvorgangModel Item = (TankvorgangModel)e.Item;

			if (this._von.Ticks <= Item.Datum.Ticks && Item.Datum.Ticks <= this._bis.Ticks)
			{
				e.Accepted = true;
			}
			else
			{
				e.Accepted = false;
			}
		}
	}
}
