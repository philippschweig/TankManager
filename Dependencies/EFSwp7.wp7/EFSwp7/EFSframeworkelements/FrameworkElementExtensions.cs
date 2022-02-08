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
using Microsoft.Phone.Controls;
using System.Collections.Generic;
using System.Windows.Navigation;
using System.Diagnostics;

namespace EFSresources.EFSframeworkelements
{
	public static class FrameworkElementExtensions
	{
		public static Boolean UpdateToggleSwitch(this PhoneApplicationPage page, ToggleSwitch tw, String twOn, String twOff)
        {
			if ((Boolean)tw.IsChecked)
			{
				tw.Content = twOn;
				return true;
			}
			else
			{
				tw.Content = twOff;
				return false;
			}
        }

		public static Boolean Refresh(this ToggleSwitch tw, String twOn, String twOff)
		{
			if ((Boolean)tw.IsChecked)
			{
				tw.Content = twOn;
				return true;
			}
			else
			{
				tw.Content = twOff;
				return false;
			}
		}

		//public static T ElementAt<T>(this List<T> collection, int index)
		//{
			
		//}

		public static void RootFrameDateTimePicker_Navigated(this Application app, object sender, NavigationEventArgs e)
		{
			// this is a dirty hack to circumvent the hard coded title of the datepicker

			try
			{
				if (e.Uri == null || e.Content == null || !(e.Content is DatePickerPage) || e.Uri.OriginalString != "/Microsoft.Phone.Controls.Toolkit;component/DateTimePickers/DatePickerPage.xaml")
					return;

				DatePickerPage objDatePickerPage = (DatePickerPage)e.Content;
				FrameworkElement objSystemTrayPlaceholder = (FrameworkElement)objDatePickerPage.FindName("SystemTrayPlaceholder");
				Grid objParentGrid = (Grid)objSystemTrayPlaceholder.Parent;

				//TextBlock objTitleTextBox = (TextBlock)objParentGrid.Children.(c => c.GetType() == typeof(TextBlock));
				//objTitleTextBox.Text = "Datum wählen"; // put your resource access here
			}
			catch
			{

			}
		}
	}
}
