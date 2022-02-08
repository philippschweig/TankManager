using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFSresources.WP7
{
	public class SystemInfo
	{
		private static Version TargetedWP8 = new Version(8, 0);
		private static Version TargetedWP75 = new Version(7, 5);

		public static bool IsTargetedWP8 {
			get { return Environment.OSVersion.Version >= TargetedWP8; }
		}
		public static bool IsTargetedWP75
		{
			get { return Environment.OSVersion.Version >= TargetedWP75; }
		}
	}
}
