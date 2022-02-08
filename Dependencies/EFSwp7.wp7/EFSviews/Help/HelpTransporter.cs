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

namespace EFSresources.Views.Help
{
	public class HelpTransporter : PageTransporter
	{
		public Uri HelpFileBuild;
		public string HelpPath;
		public Uri HelpFile;

		public HelpTransporter(Uri viewUri) : base(viewUri) { }
	}
}
