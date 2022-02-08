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
	public class ExtrasModel : ModelBase
	{
		//Fotoeigenschaft
		private String _anmerkungen;
		public String Anmerkungen
		{
			get
			{
				return _anmerkungen;
			}
			set
			{
				if (value != _anmerkungen)
				{
					_anmerkungen = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Anmerkungen");
				}
			}
		}

		public ExtrasModel() { }
	}
}
