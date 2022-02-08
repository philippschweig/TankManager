using System;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Resources;

namespace EFSresources.App
{
	public class AppInfo
	{
		public static Assembly Source = Assembly.GetCallingAssembly();

		/// <summary>
		/// Gibt den Assemblyname des aufrufenden Assemblys zurück.
		/// </summary>
		/// <returns></returns>
		public static String Name
		{
			get
			{
				String fullname = Source.FullName;

				String[] infoparts = fullname.Split(',');

				String appname = infoparts[0];

				return appname;
			}
		}

		/// <summary>
		/// Gibt den Assemblyname des aufrufenden Assemblys in Großbuchstaben zurück.
		/// </summary>
		/// <returns></returns>
		public static String NameUpperCase
		{
			get
			{
				String fullname = Source.FullName;

				String[] infoparts = fullname.Split(',');

				String appname = infoparts[0];

				return appname.ToUpper();
			}
		}

		/// <summary>
		/// Gibt die Assemblyverion des aufrufenden Assemblys zurück.
		/// </summary>
		/// <param name="beta"></param>
		/// <returns></returns>
		public static String Version
		{
			get
			{
				String fullname = Source.FullName;

				String[] infoparts = fullname.Split(',');

				String version = infoparts[1].Split('=')[1];

				return version;
			}
		}

		/// <summary>
		/// Gibt das Builddatum des aufrufenden Assemblys zurück.
		/// </summary>
		/// <returns></returns>
		public static DateTime BuildDateTime
		{
			get
			{
				String fullname = Source.FullName;

				String[] infoparts = fullname.Split(',');

				String version = infoparts[1].Split('=')[1];

				String[] versionparts = version.Split('.');

				var buildDateTime = new DateTime(2000, 1, 1).Add(new TimeSpan(TimeSpan.TicksPerDay * int.Parse(versionparts[2]) + // days since 1 January 2000
				TimeSpan.TicksPerSecond * 2 * int.Parse(versionparts[3]))); // seconds since midnight, (multiply by 2 to get original)

				// a valid date-String can now be constructed like this

				return buildDateTime;
			}
		}

		/// <summary>
		/// Gibt die Assemblybeschreibung des aufrufenden Assemblys zurück.
		/// </summary>
		/// <returns></returns>
		public static String Description
		{
			get
			{
				var attribute = Source.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

				return ((AssemblyDescriptionAttribute)attribute[0]).Description;
			}
		}

		/// <summary>
		/// Gibt die Assemblybeschreibung des aufrufenden Assemblys zurück.
		/// </summary>
		/// <returns></returns>
		public static String Company
		{
			get
			{
				var attribute = Source.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);

				return ((AssemblyCompanyAttribute)attribute[0]).Company;
			}
		}

		/// <summary>
		/// Gibt den Assemblytitle des aufrufenden Assemblys zurück.
		/// </summary>
		/// <returns></returns>
		public static String Title
		{
			get
			{
				var attribute = Source.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				
				return ((AssemblyTitleAttribute)attribute[0]).Title;
			}
		}

		/// <summary>
		/// Gibt den Assemblytitle des aufrufenden Assemblys in Großbuchstaben zurück.
		/// </summary>
		/// <returns></returns>
		public static String TitleUpperCase
		{
			get
			{
				var attribute = Source.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				
				return ((AssemblyTitleAttribute)attribute[0]).Title.ToUpper();
			}
		}

		/// <summary>
		/// Gibt den Copyrighttest des aufrufenden Assemblys zurück.
		/// </summary>
		/// <returns></returns>
		public static String Copyright
		{
			get
			{
				var attribute = Source.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

				return ((AssemblyCopyrightAttribute)attribute[0]).Copyright;
			}
		}

		// Methoden
		public static Stream LoadContent(Uri contentUri, Application source = null)
		{
			if (source == null)
			{
				source = Application.Current;
			}

			StreamResourceInfo sri = null;
			sri = Application.GetResourceStream(contentUri);
			return sri.Stream;
		}
	}
}