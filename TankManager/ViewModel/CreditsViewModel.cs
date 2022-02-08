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
	public class CreditsViewModel : ListItemViewModel
	{
		public CreditsViewModel() { }

		public void Load()
		{
			if (!this._isLoaded)
			{
				EFSlogsystem.Logger.WriteInfo(this.GetType(), "Credits laden");

				this.Items = new ObservableCollection<ListItemModel>();
				this.Items.Add(new ListItemModel()
				{
					Title = "Coding4Fun Toolkit",
					Content = "Coding4Fun Windows Phone Toolkit"
				});
				this.Items.Add(new ListItemModel()
				{
					Title = "WP7 Tombstone Helper",
					Content = "This library adds extension methods to PhoneApplicationPage [...]\nhttp://tombstonehelper.codeplex.com/"
				});
				this.Items.Add(new ListItemModel()
				{
					Title = "Live Connect API",
					Content = "Microsoft Live SDK"
				});
				this.Items.Add(new ListItemModel()
				{
					Title = "MetroStudio Icons",
					Content = "Syncfusion"
				});

				this._isLoaded = true;
			}
		}
	}
}
