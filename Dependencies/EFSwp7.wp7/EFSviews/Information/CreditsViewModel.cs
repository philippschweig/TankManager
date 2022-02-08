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

namespace EFSresources.Views.Information
{
	public class CreditsViewModel : ListItemViewModel
	{
		public CreditsViewModel() { }

		public void Load(ObservableCollection<ListItemModel> credits = null)
		{
			if (!this._isLoaded)
			{
				EFSlogsystem.Logger.WriteInfo(this.GetType(), "Credits laden");

				this.Items = new ObservableCollection<ListItemModel>();

				if (credits != null)
				{
					this.Items = credits;
				}

				this._isLoaded = true;
			}
		}
	}
}
