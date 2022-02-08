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
using System.Threading;
using Microsoft.Phone.Net.NetworkInformation;

namespace EFSresources.EFSnetwork
{
	public class NetworkMonitor
	{
		//private NetworkInterfaceType _currNetType;
		private volatile bool _valueRetrieved = false;

		public bool InternetConnected = false;
		public EventHandler OnTestInternetConnectionCompleted;

		public NetworkMonitor()
		{
			
		}

		#region Internet

		public void TestInternetConnection()
		{
			this.InternetConnected = false;

			WebClient c = new WebClient();
			c.OpenReadCompleted += new OpenReadCompletedEventHandler(c_OpenReadCompleted);
			c.OpenReadAsync(new Uri("http://www.google.com/"));
		}

		void c_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
		{

		}
		#endregion

		#region Network
		public bool WiFiEnabled
		{
			get { return DeviceNetworkInformation.IsWiFiEnabled; }
		}
		public NetworkInterfaceType CurrentNetworkType
		{
			get
			{
				return NetworkInterface.NetworkInterfaceType;
			}
			private set { }
		}

		public bool isNetworkReady()
		{
			if (false == _valueRetrieved) return false;

			switch (CurrentNetworkType)
			{
				//Low speed networks
				case NetworkInterfaceType.MobileBroadbandCdma:
				case NetworkInterfaceType.MobileBroadbandGsm:
					return true;
				//High speed networks
				case NetworkInterfaceType.Wireless80211:
				case NetworkInterfaceType.Ethernet:
					return true;
				//No Network
				case NetworkInterfaceType.None:
				default:
					return false;
			}
		}
		#endregion
	}
}
