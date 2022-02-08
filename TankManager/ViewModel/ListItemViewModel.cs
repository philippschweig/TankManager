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
using TankManager.Model;

namespace TankManager.ViewModel
{
	public class ListItemViewModel : ViewModelBase
	{
		protected Boolean _isLoaded = false;
		public Boolean IsLoaded
		{
			get
			{
				return _isLoaded;
			}
		}

		private ObservableCollection<ListItemModel> _items;
		public ObservableCollection<ListItemModel> Items
		{
			get
			{
				return _items;
			}
			set
			{
				if (value != _items)
				{
					_items = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Items");
				}
			}
		}
	}
}
