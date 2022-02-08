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
using EFSresources.Views.Information;
using System.Collections.ObjectModel;

namespace TankManager.Sys
{
	public class SysConst
	{
		public static Uri HelpFileBuild = new Uri("Help.htm", UriKind.Relative);
        public static Uri HelpFile = new Uri("http://www.efsdev.de/page=tm&action=helpfile");
        public static Uri HelpFile2 = new Uri("http://tankmanager.efsdev.de/Help.txt");
		public static string HelpPath = "Help.dat";
        public static Uri DonateLink = new Uri("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=2ZH3SCXWK7JG6");

		public static InformationTransporter InfoTransport = new InformationTransporter()
		{
			Source = App.CurrentAssembly,
			//AppLogo = Logo,
			DeveloperEmail = "efsdeveloping@gmail.com",
			SupportEmail = "efsdeveloping@gmail.com",
			CreditsViewModel = new CreditsViewModel() { Items = new ObservableCollection<ListItemModel>()
			{
				new ListItemModel()
				{
					Title = "Coding4Fun Toolkit",
					Content = "Coding4Fun Windows Phone Toolkit"
				},
				new ListItemModel()
				{
					Title = "WP7 Tombstone Helper",
					Content = "This library adds extension methods to PhoneApplicationPage [...]\nhttp://tombstonehelper.codeplex.com/"
				},
				new ListItemModel()
				{
					Title = "Live Connect API",
					Content = "Microsoft Live SDK"
				},
				new ListItemModel()
				{
					Title = "MetroStudio Icons",
					Content = "Syncfusion"
				}
			} },
			HistoryViewModel = new HistoryViewModel() { Items = new ObservableCollection<ListItemModel>()
			{
				new ListItemModel()
				{
					Title = "1.0",
					Contents = new HistoryList<string>() {
				        "First public marketplace release"
				    }
				}
			} }
		};
	}
}
