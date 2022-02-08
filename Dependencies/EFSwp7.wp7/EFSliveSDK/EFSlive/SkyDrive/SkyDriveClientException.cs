﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace EFSresources.EFSlive.SkyDrive
{
	public class SkyDriveClientException : Exception
	{
		public SkyDriveClientException() { }
		public SkyDriveClientException(string message) : base(message) { }
		public SkyDriveClientException(string message, Exception inner) : base(message, inner) { }
	}
}
