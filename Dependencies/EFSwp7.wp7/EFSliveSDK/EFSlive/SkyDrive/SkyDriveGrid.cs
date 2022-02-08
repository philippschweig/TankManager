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
using Microsoft.Live.Controls;

namespace EFSresources.EFSlive.SkyDrive
{
	public class SkyDriveGrid : Grid
	{
		#region FrameworkElements
		/* FrameworkElements */
		// Grid
		public StackPanel MainStackPanel = new StackPanel();

		// MainStackPanel
		public TextBlock Header = new TextBlock();
		public StackPanel SubStackPanel = new StackPanel();

		// SubstackPanel
		public TextBlock Info = new TextBlock();
		public SignInButton ConnectButton = new SignInButton();
		public TextBlock Welcome = new TextBlock();
		#endregion

		#region Fields
		public static readonly DependencyProperty ClientIdProperty;
		#endregion

		#region Propertys
		/* Propertys */
		// ClientId
		public string ClientId
		{
			get
			{
				return (base.GetValue(ClientIdProperty) as string);
			}
			set
			{
				base.SetValue(ClientIdProperty, value);
			}
		}

		#endregion

		public SkyDriveGrid()
		{
			this.HorizontalAlignment = HorizontalAlignment.Stretch;
			this.VerticalAlignment = VerticalAlignment.Stretch;

			this.Resources.Add("MainStackPanel", this.MainStackPanel);
			this.MainStackPanel.Resources.Add("Header", this.Header);
			this.MainStackPanel.Resources.Add("SubStackPanel", this.SubStackPanel);
			this.SubStackPanel.Resources.Add("Info", this.Info);
			this.SubStackPanel.Resources.Add("ConnectButton", this.ConnectButton);
			this.SubStackPanel.Resources.Add("Welcome", this.Welcome);

			this.MainStackPanel.Orientation = Orientation.Vertical;

			this.Header.Style = (Style)Application.Current.Resources["PhoneTextTitle2Style"];
			this.Header.TextWrapping = TextWrapping.Wrap;
			this.Header.Text = "SkyDrive";
		}
	}
}
