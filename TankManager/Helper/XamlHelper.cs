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
using EFSresources.EFSlibrary;
using TankManager.Lokalisierung;
using System.Windows.Data;
using System.Globalization;
using System.Diagnostics;
using System.Threading;
using System.Windows.Markup;
using Microsoft.Phone.Info;
using EFSresources;
using EFSresources.Library;

namespace TankManager.Helper
{
	public class XamlHelper
	{
		public static String AppTitle
		{
			get { return AppInfo.Title; }
		}
		
		public static String AppTitleUpperCase
		{
			get { return AppInfo.TitleUpperCase; }
		}

		public static String AppVersion
		{
			get { return AppInfo.Version; }
		}

		public static DateTime LastUpdate
		{
			get { return AppInfo.BuildDateTime; }
		}

		public static String AppCompany
		{
			get { return AppInfo.Company; }
		}

		public static String Copyright
		{
			get { return AppInfo.Copyright; }
		}

		public static XmlLanguage XmlSprache
		{
			get
			{
				return XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);
			}
		}

		public static String Sprache
		{
			get { return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName; }
		}

		public static String Length
		{
			get
			{
				if (App.ViewModel.Fahrzeuge[App.ViewModel.AktuelleFahrzeugID].ZähleMitTageskilometer)
				{
					return Translation.UnitSystemDriveLength;
				}
				else
				{
					return Translation.UnitSystemDriveTotalLength;
				}
			}
		}

		public static String CurrencySymbol
		{
			get { return CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol; }
		}

		#region Microsoft.Phone.Info
		public static String AppCurrentMemUsage
		{
			get { return UnitPrefix.ConvertToBestString(DeviceStatus.ApplicationCurrentMemoryUsage, "B", true, 2); }
		}

		public static String AppMemUsageLimit
		{
			get { return UnitPrefix.ConvertToBestString(DeviceStatus.ApplicationMemoryUsageLimit, "B", true, 2); }
		}

		// Höchster Verbauch
		public static String AppPeakMemUsage
		{
			get { return UnitPrefix.ConvertToBestString(DeviceStatus.ApplicationPeakMemoryUsage, "B", true, 2); }
		}
		#endregion
	}
}
