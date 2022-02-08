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

namespace EFSresources.EFSextensions
{
	public static class DateTimeExtensions
	{
		public static long ToUnixTime(this DateTime dt)
		{
			return (dt.Ticks - new DateTime(1970, 1, 1).Ticks) / TimeSpan.TicksPerSecond;
		}

		public static String To_YYYY_MM_DD_HH_MM_SS(this DateTime dt, string seperator = "-")
		{
			return dt.Year + seperator + dt.Month + seperator + dt.Day + seperator + dt.Hour + seperator + dt.Minute + seperator + dt.Second;
		}

		// only Static
		public static DateTime FromUnixTime(long unixTime)
		{
			return new DateTime(unixTime * TimeSpan.TicksPerSecond + new DateTime(1970, 1, 1).Ticks);
		}
	}
}
