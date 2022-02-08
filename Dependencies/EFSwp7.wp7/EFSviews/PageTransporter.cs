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
using EFSresources.EFSlogging;

namespace EFSresources.Views
{
	public class PageTransporter
	{
		public static PageTransporter Transport;

		protected Uri ViewUri;
		public Assembly Source;

		//public PageTransporter() {
			
		//}

		public PageTransporter(Uri viewUri)
		{
			this.ViewUri = viewUri;
		}

		public void Navigate(NavigationService navi, PageTransporter transporter)
		{
			Transport = transporter;

			if (Transport != null)
			{
				if (Transport.Source != null)
				{
					navi.Navigate(ViewUri);
					return;
				}
			}

			EFSlogsystem.Logger.WriteWarning(this.GetType(), "Navigate, Navigation Failed");
			MessageBox.Show(EFSlanguage.PageTransporterNavigationFailed);
		}
	}
}
