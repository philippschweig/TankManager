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
using System.IO;
using Serialization;

namespace EFSresources.EFSextensions
{
	public static class ObjectExtensions
	{
		public static T DeepCopy<T>(this T o)
        {
			byte[] copy = SilverlightSerializer.Serialize(o);

			return (T)SilverlightSerializer.Deserialize(copy);
        }
	}
}
