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
using Microsoft.Live;
using EFSresources.EFSlogging;
using EFSresources.EFSlive.SkyDrive;

namespace EFSresources.EFSlive
{
	public class LiveConnect
	{
		public LiveConnectClient Client;
		public LiveConnectSession Session;

		public LiveConnect(LiveConnectSession session)
		{
			this.Client = new LiveConnectClient(session);
			this.Session = this.Client.Session;

			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "LiveConnect initialisert");
		}

		public void GetSkyDriveData()
		{
			//this.Client.GetAsync("me/skydrive/files?filter=folders,albums", null);
			this.Client.GetAsync("me/skydrive/files", null);
		}
	}
}
