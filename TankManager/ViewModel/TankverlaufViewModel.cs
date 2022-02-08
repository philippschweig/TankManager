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
using System.Windows.Data;
using System.ComponentModel;
using EFSresources.EFSlogging;
using TankManager.Model;
using EFSresources.EFSmvvm;

namespace TankManager.ViewModel
{
	public class TankverlaufViewModel : ViewModelBase
	{
		private FahrzeugModel kfz = null;
		public CollectionViewSource SortedTankverlauf { get; private set; }

		public TankverlaufViewModel(FahrzeugModel _kfz)
		{
			this.kfz = _kfz;
			this.Load();
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "TankverlaufViewModel initialisert");
		}

		private void Load()
        {
			this.SortedTankverlauf = new CollectionViewSource();
			this.SortedTankverlauf.SortDescriptions.Add(new SortDescription("Datum", ListSortDirection.Descending));
			this.SortedTankverlauf.Source = this.kfz.Tankverlauf;
        }
	}
}
