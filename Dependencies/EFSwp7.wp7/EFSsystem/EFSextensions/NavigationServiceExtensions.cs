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
using System.Windows.Navigation;

namespace EFSresources.EFSextensions
{
	public static class NavigationServiceExtensions
	{
		public static void RemoveBackStack(this NavigationService navigation)
        {
			while (navigation.CanGoBack)
			{
				navigation.RemoveBackEntry();
			}
        }
	}
}
