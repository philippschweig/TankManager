using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using EFSresources.EFSlibrary;
using EFSresources.EFSlogging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TombstoneHelper;
using EFSresources;

namespace EFSresources.EFSlogging.View
{
    public partial class LogView : PhoneApplicationPage
    {
		private LogViewModel ViewModel;

		private Boolean SelectedItemViewing = false;

        public LogView()
        {
			InitializeComponent();

			this.ViewModel = (LogViewModel)this.Resources["ViewModel"];

			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "LogView initialisiert");
        }

		private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (this.SelectedItemViewing)
			{
				e.Cancel = true;
				this.GridMainEnable();
				this.GridSelectedItemEnable();
			}
		}

		private void GridMainEnable(Boolean _disabled = false)
        {
			if (_disabled)
			{
				this.Grid_Main.Opacity = 0.25;
				this.ListPickerLogs.IsEnabled = false;
				this.ListBoxLog.IsEnabled = false;
			}
			else
			{
				this.Grid_Main.Opacity = 1.0;
				this.ListPickerLogs.IsEnabled = true;
				this.ListBoxLog.IsEnabled = true;
			}
        }

		private void GridSelectedItemEnable(Boolean _disabled = true)
		{
			if (_disabled)
			{
				this.Grid_SelectedItem.Visibility = Visibility.Collapsed;
				this.SelectedItemViewing = false;
			}
			else
			{
				this.Grid_SelectedItem.Visibility = Visibility.Visible;
				this.SelectedItemViewing = true;
			}
		}

        private void LogViewPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
			//this.ListPickerLogs.SelectedIndex = App.AppLog.listLogs().Count - 1;
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "LogView geladen");
		}

		private void ListPickerLogs_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			//if (this.ListBoxLog.Items.Count > 0)
			//{
			//    this.ListBoxLog.ScrollIntoView(this.ListBoxLog.Items[this.ListBoxLog.Items.Count - 1]);
			//}
		}

		private void ListBoxLog_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
		{
			this.GridMainEnable(true);
			this.GridSelectedItemEnable(false);
		}

		#region navigation
		protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
		{
			this.SaveState(e);

			base.OnNavigatingFrom(e);
		}

		protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
		{
			this.RestoreState();

			base.OnNavigatedTo(e);
		}
		#endregion
	}
}
