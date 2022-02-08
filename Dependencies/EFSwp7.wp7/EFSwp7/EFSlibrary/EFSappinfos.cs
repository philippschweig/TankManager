using System;
using System.Reflection;
using System.Diagnostics;

namespace EFSresources.EFSlibrary
{
	public class EFSappinfos
	{
		public static Assembly Source = Assembly.GetCallingAssembly();

		public static DateTime TestZeitraumsEnde;

		/// <summary>
		/// Gibt den Assemblyname des aufrufenden Assemblys zurück.
		/// </summary>
		/// <returns></returns>
		public static String AppName
		{
			get
			{
				String fullname = Assembly.GetCallingAssembly().FullName;

				String[] infoparts = fullname.Split(',');

				String appname = infoparts[0];

				return appname;
			}
		}

		public static String GetAppName()
		{
			String fullname = Assembly.GetCallingAssembly().FullName;

			String[] infoparts = fullname.Split(',');

			String appname = infoparts[0];

			return appname;
		}

		/// <summary>
		/// Gibt den Assemblyname des aufrufenden Assemblys in Großbuchstaben zurück.
		/// </summary>
		/// <returns></returns>
		public static String AppNameUpperCase
		{
			get
			{
				String fullname = Assembly.GetCallingAssembly().FullName;

				String[] infoparts = fullname.Split(',');

				String appname = infoparts[0];

				return appname.ToUpper();
			}
		}

		public static String GetAppNameUpperCase()
		{
			String fullname = Assembly.GetCallingAssembly().FullName;

			String[] infoparts = fullname.Split(',');

			String appname = infoparts[0];

			return appname.ToUpper();
		}

		/// <summary>
		/// Gibt die Assemblyverion des aufrufenden Assemblys zurück.
		/// </summary>
		/// <param name="beta"></param>
		/// <returns></returns>
		public static String AppVersion
		{
			get
			{
				String fullname = Assembly.GetCallingAssembly().FullName;

				String[] infoparts = fullname.Split(',');

				String version = infoparts[1].Split('=')[1];

				return version;
			}
		}

		public static String GetAppVersion(Boolean beta = false)
		{
			String fullname = Assembly.GetCallingAssembly().FullName;

			String[] infoparts = fullname.Split(',');

			String version = infoparts[1].Split('=')[1];

			return version;
		}

		/// <summary>
		/// Gibt das Builddatum des aufrufenden Assemblys zurück.
		/// </summary>
		/// <returns></returns>
		public static DateTime AppBuildDateTime
		{
			get
			{
				String fullname = Assembly.GetCallingAssembly().FullName;

				String[] infoparts = fullname.Split(',');

				String version = infoparts[1].Split('=')[1];

				String[] versionparts = version.Split('.');

				var buildDateTime = new DateTime(2000, 1, 1).Add(new TimeSpan(TimeSpan.TicksPerDay * int.Parse(versionparts[2]) + // days since 1 January 2000
				TimeSpan.TicksPerSecond * 2 * int.Parse(versionparts[3]))); // seconds since midnight, (multiply by 2 to get original)

				// a valid date-String can now be constructed like this

				return buildDateTime;
			}
		}
		
		public static DateTime GetAppBuildDateTime()
		{
			String fullname = Assembly.GetCallingAssembly().FullName;

			String[] infoparts = fullname.Split(',');

			String version = infoparts[1].Split('=')[1];

			String[] versionparts = version.Split('.');

			var buildDateTime = new DateTime(2000, 1, 1).Add(new TimeSpan(TimeSpan.TicksPerDay * int.Parse(versionparts[2]) + // days since 1 January 2000
			TimeSpan.TicksPerSecond * 2 * int.Parse(versionparts[3]))); // seconds since midnight, (multiply by 2 to get original)

			// a valid date-String can now be constructed like this

			return buildDateTime;
		}

		/// <summary>
		/// Gibt die Assemblybeschreibung des aufrufenden Assemblys zurück.
		/// </summary>
		/// <returns></returns>
		public static String AppDescription
		{
			get
			{
				var attribute = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

				return ((AssemblyDescriptionAttribute)attribute[0]).Description;
			}
		}
		
		public static String GetAppDescription()
		{
			var attribute = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

			return ((AssemblyDescriptionAttribute)attribute[0]).Description;
		}

		/// <summary>
		/// Gibt die Assemblybeschreibung des aufrufenden Assemblys zurück.
		/// </summary>
		/// <returns></returns>
		public static String AppCompany
		{
			get
			{
				var attribute = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);

				return ((AssemblyCompanyAttribute)attribute[0]).Company;
			}
		}
		
		public static String GetAppCompany()
		{
			var attribute = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);

			return ((AssemblyCompanyAttribute)attribute[0]).Company;
		}

		/// <summary>
		/// Gibt den Assemblytitle des aufrufenden Assemblys zurück.
		/// </summary>
		/// <returns></returns>
		public static String AppTitle
		{
			get
			{
				var attribute = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				
				return ((AssemblyTitleAttribute)attribute[0]).Title;
			}
		}

		public static String GetAppTitle()
		{
			var attribute = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

			return ((AssemblyTitleAttribute)attribute[0]).Title;
		}

		/// <summary>
		/// Gibt den Assemblytitle des aufrufenden Assemblys in Großbuchstaben zurück.
		/// </summary>
		/// <returns></returns>
		public static String AppTitleUpperCase
		{
			get
			{
				var attribute = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				
				return ((AssemblyTitleAttribute)attribute[0]).Title.ToUpper();
			}
		}

		public static String GetAppTitleUpperCase()
		{
			var attribute = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

			return ((AssemblyTitleAttribute)attribute[0]).Title.ToUpper();
		}

		/// <summary>
		/// Gibt den Copyrighttest des aufrufenden Assemblys zurück.
		/// </summary>
		/// <returns></returns>
		public static String Copyright
		{
			get
			{
				var attribute = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

				return ((AssemblyCopyrightAttribute)attribute[0]).Copyright;
			}
		}
		
		public static String GetCopyright()
		{
			var attribute = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

			return ((AssemblyCopyrightAttribute)attribute[0]).Copyright;
		}

		/// <summary>
		/// Prüft ob der Testzeitraum der Anwendung gültig ist.
		/// </summary>
		/// <returns></returns>
		public static bool AppTestZeitraumsEnde
		{
			get
			{
				if (TestZeitraumsEnde.Ticks < DateTime.Now.Ticks)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		
		public static bool GetAppTestZeitraumsEnde()
		{
			if (TestZeitraumsEnde.Ticks < DateTime.Now.Ticks)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Prüft ob der Testzeitraum der Anwendung mit dem übergebenden Datum gültig ist.
		/// </summary>
		/// <returns></returns>
		public static bool GetAppTestZeitraumsEnde(DateTime dt)
		{
			if (dt.Ticks < DateTime.Now.Ticks)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}