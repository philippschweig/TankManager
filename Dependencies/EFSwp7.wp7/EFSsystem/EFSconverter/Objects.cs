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
using System.Xml.Serialization;
using System.IO;
using EFSresources.EFSexceptions;
using System.Reflection;

namespace EFSresources.EFSconverter
{
	public class Objects
	{
		public static String ConvertToString<T>(T obj)
		{
			StringWriter sw = new StringWriter();

			XmlSerializer ser = new XmlSerializer(typeof(T));
			ser.Serialize(sw, obj);

			return sw.ToString();
		}

		public static String ConvertToString(Object obj)
		{
			if( obj.GetType().IsSubclassOf(typeof(Exception)) || obj.GetType() == typeof(Exception) )
			{
				return ConvertToString((Exception)obj);
			}
			else
			{
				StringWriter sw = new StringWriter();

				XmlSerializer ser = new XmlSerializer(obj.GetType());
				ser.Serialize(sw, obj);

				return sw.ToString();
			}
		}

		public static String ConvertToString(Exception obj)
		{
			EFSexception e = new EFSexception(obj);

			StringWriter sw = new StringWriter();

			XmlSerializer ser = new XmlSerializer(e.GetType());
			ser.Serialize(sw, e);

			return sw.ToString();
		}
	}
}
