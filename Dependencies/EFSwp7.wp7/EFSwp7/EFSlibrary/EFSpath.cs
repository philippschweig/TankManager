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
using System.Collections.Generic;

namespace EFSresources.EFSlibrary
{
	public class EFSpath
	{
		public static List<string> getList(string path)
		{
			if (path != null)
			{
				string[] temp = path.Split('/');

				return new List<string>(temp);
			}

			return new List<string>();
		}
	}
}
