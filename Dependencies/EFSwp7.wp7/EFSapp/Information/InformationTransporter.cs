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
using System.Reflection;
using System.Windows.Navigation;
using System.Windows.Media.Imaging;

namespace EFSresources.App.Information
{
	public class InformationTransporter
	{
		private static Uri InfoViewUri = new Uri("/EFSviews;component/Information/InfoView.xaml", UriKind.Relative);
		public static InformationTransporter Transport;

		public Assembly Source;
		public HistoryViewModel HistoryViewModel;
		public CreditsViewModel CreditsViewModel;
		public BitmapImage AppLogo;

		public string DeveloperEmail, SupportEmail;

		public static void Navigate(NavigationService navi, InformationTransporter transporter)
		{
			Transport = transporter;

			if (Transport != null)
			{
				if (Transport.Source != null)
				{
					navi.Navigate(InfoViewUri);
					return;
				}
			}

			MessageBox.Show("Navigation Failed");
		}
	}
}
