using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Windows;

namespace EFSresources.EFSmvvm
{
	public class ModelBase : INotifyPropertyChanged//, IDataErrorInfo
	{
		protected void NotifyPropertyChanged(string propertyName)
		{
			if (MVVM.NotifyOn)
			{
				Debug.WriteLine(Deployment.Current.Dispatcher.CheckAccess() ? "On UI Thread" : "On Background Thread");
				PropertyChangedEventHandler handler = PropertyChanged;
				if (handler != null)
					handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region IDataErrorInfo Members

		public virtual string Error
		{
			get { return String.Empty; }
		}

		public virtual string this[string columnName]
		{
			get { return String.Empty; }
		}

		#endregion
	}
}
