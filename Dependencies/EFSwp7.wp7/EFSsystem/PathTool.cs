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

namespace EFSresources
{
	public class PathTool
	{
		public const char seperator = '/';

		public List<String> Path;

		public PathTool(params string[] paths)
		{
			this.Path = new List<string>();

			this.Add(paths);
		}

		public void Add(params string[] paths)
        {
			foreach (string item in paths)
			{
				this.Path.Add(item);
			}
        }

		public List<String> GetPathList()
		{
			if (this.Path.Count == 0)
			{
				return new List<string>();
			}
			else
			{
				return new List<string>(this.Path);
			}
		}

		public override string ToString()
		{
			string url = seperator.ToString();

			foreach (string item in this.Path)
			{
				url += item + seperator;
			}

			return url;
		}
	}
}
