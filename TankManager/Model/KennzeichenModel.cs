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

namespace TankManager.Model
{
	public class KennzeichenModel : ModelBase
	{
		private String _region;
		public String Region
		{
			get
			{
				return _region;
			}
			set
			{
				if (value != _region)
				{
					_region = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Region");
				}
			}
		}

		private String _kennung;
		public String Kennung
		{
			get
			{
				return _kennung;
			}
			set
			{
				if (value != _kennung)
				{
					_kennung = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Kennung");
				}
			}
		}

		public KennzeichenModel() { }

		public override string ToString()
		{
			return this.Region + "-" + this.Kennung;
		}
	}
}
