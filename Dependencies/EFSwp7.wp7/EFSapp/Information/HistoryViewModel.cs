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

namespace EFSresources.App.Information
{
	public class HistoryViewModel : ListItemViewModel
	{
		public HistoryViewModel() { }

		public void Load(ObservableCollection<ListItemModel> history = null)
		{
			if (!this._isLoaded)
			{
				EFSlogsystem.Logger.WriteInfo(this.GetType(), "History laden");

				this.Items = new ObservableCollection<ListItemModel>();

				if (history != null)
				{
					this.Items = history;
				}

				this._isLoaded = true;
			}
		}
	}
}
