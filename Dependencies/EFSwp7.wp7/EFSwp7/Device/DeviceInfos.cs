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
using Microsoft.Phone.Info;

namespace EFSresources.Device
{
	public class DeviceInfos
	{
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
	}
}
