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
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Data;
using System.ComponentModel;
using EFSresources.EFSlogging;

namespace EFSresources.EFSlogging.View
{
	public class LogViewModel
	{
		public ObservableCollection<LogHistory> Logs { get; private set; }
		public CollectionViewSource SortedLogs { get; private set; }

		public LogViewModel()
		{  
			this.Logs = new ObservableCollection<LogHistory>(EFSlogsystem.Logger.getLogs());
			this.SortedLogs = new CollectionViewSource();
			this.SortedLogs.SortDescriptions.Add(new SortDescription("Timestamp", ListSortDirection.Descending));
			this.SortedLogs.Source = this.Logs;
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "LogViewModel initialisiert");
		}
	}
}
