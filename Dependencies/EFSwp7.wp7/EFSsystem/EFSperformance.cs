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

namespace EFSresources
{
	public class EFSperformance
	{
		private static EFSperformance _meter;
		public static EFSperformance Meter
		{
			get
			{
				if (_meter == null)
				{
					_meter = new EFSperformance();
				}
				return _meter;
			}
		}

		private Int64 _startTicks;
		private Int64 _stopTicks;

		public void Start()
		{
			this._startTicks = DateTime.Now.Ticks;
		}

		public void Stop()
		{
			this._stopTicks = DateTime.Now.Ticks;
		}

		public TimeSpan GetTimeSpan()
		{
			Int64 difference = this._stopTicks - this._startTicks;

			return new TimeSpan(difference);
		}
	}
}
