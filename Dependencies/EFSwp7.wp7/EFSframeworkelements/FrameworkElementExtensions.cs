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
using Microsoft.Phone.Controls;

namespace EFSresources.Frameworkelements
{
	public enum ToggleSwitchState
	{
		None, Checked, Unchecked
	}

	public static class FrameworkElementExtensions
	{
		public static Boolean Refresh(this ToggleSwitch tw, String twOn, String twOff, ToggleSwitchState state = ToggleSwitchState.None)
		{
			Boolean switcher;

			switch (state)
			{
				case ToggleSwitchState.Checked:
					switcher = true;
					break;
				case ToggleSwitchState.Unchecked:
					switcher = false;
					break;
				case ToggleSwitchState.None:
				default:
					switcher = tw.IsChecked.Value;
					break;
			}

			if (switcher)
			{
				tw.Content = twOn;
				return true;
			}
			else
			{
				tw.Content = twOff;
				return false;
			}
		}
	}
}
