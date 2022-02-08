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
using System.Collections.Generic;

namespace EFSresources.App.Information
{
	public class ListItemModel : ModelBase
	{
		private String _title;
		public String Title
		{
			get
			{
				return _title;
			}
			set
			{
				if (value != _title)
				{
					_title = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Title");
				}
			}
		}

		private String _content;
		public String Content
		{
			get
			{
				return _content;
			}
			set
			{
				if (value != _content)
				{
					_content = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Content");
				}
			}
		}

		private HistoryList<String> _contents;
		public HistoryList<String> Contents
		{
			get
			{
				return _contents;
			}
			set
			{
				if (value != _contents)
				{
					_contents = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Contents");
				}
			}
		}

		public void Add(ListItemModel item)
		{

		}
	}

	public class HistoryList<T> : List<T>
	{
		public override string ToString()
		{
			String list = String.Empty;

			foreach (var item in this)
			{
				list += "- " + item.ToString();

				if (this.IndexOf(item) < this.Count)
				{
					list += "\n";
				}
			}

			return list;
		}
	}
}
