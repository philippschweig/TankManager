using System;
using System.IO.IsolatedStorage;
using System.Windows.Navigation;
using System.Diagnostics;
using Microsoft.Phone.Shell;

namespace EFSresources.EFSlibrary
{
	public static class EFStools
	{
		public static ApplicationBarMenuItem GetAppBarMenuItem(IApplicationBar AppBar, Int32 itemnumber)
		{
			return (ApplicationBarMenuItem)AppBar.MenuItems[itemnumber - 1];
		}

		public static ApplicationBarIconButton GetAppBarIconButton(IApplicationBar AppBar, Int32 itemnumber)
		{
			return (ApplicationBarIconButton)AppBar.Buttons[itemnumber - 1];
		}

		public static void DeactivateAllIconButtons(IApplicationBar AppBar)
		{
			foreach (ApplicationBarIconButton item in AppBar.Buttons)
			{
				item.IsEnabled = false;
			}
		}

		/*public static void SetAppBarMenuItemsText(ApplicationBar AppBar, string text)
		{
			SetAppBarMenuItemsText(AppBar, text, 0);
		}

		public static void SetAppBarMenuItemsText(ApplicationBar AppBar, string text, int appBarPos)
		{
			((ApplicationBarMenuItem)AppBar.MenuItems[appBarPos]).Text = text;
		}*/

		public static void SetAppBarIconButtonsText(IApplicationBar AppBar, params string[] texte)
		{
			for (int i = 0; i < texte.Length; i++)
			{
				((ApplicationBarIconButton)AppBar.Buttons[i]).Text = texte[i];
			}
		}

		public static void SetAppBarMenuItemsText(IApplicationBar AppBar, params string[] texte)
		{
			for (int i = 0; i < texte.Length; i++)
			{
				((ApplicationBarMenuItem)AppBar.MenuItems[i]).Text = texte[i];
			}
		}

		public static void DeleteNavigationHistory(NavigationService ns)
        {
			while (ns.CanGoBack)
			{
				ns.RemoveBackEntry();
			}
        }

		public static int Betrag(int _zahl)
        {
			if (_zahl < 0)
			{
				return -_zahl;
			}
			else
			{
				return _zahl;
			}
			
        }

		public static long Betrag(long _zahl)
		{
			if (_zahl < 0)
			{
				return -_zahl;
			}
			else
			{
				return _zahl;
			}

		}

		public static double Betrag(double _zahl)
		{
			if (_zahl < 0)
			{
				return -_zahl;
			}
			else
			{
				return _zahl;
			}

		}

		public static String FormatDateTo_YYYY_MM_DD_HH_MM_SS(DateTime dt)
		{
			return dt.Year + "-" + dt.Month + "-" + dt.Day + "-" + dt.Hour + "-" + dt.Minute + "-" + dt.Second;
		}

		public static void PruefePfad(String path, IsolatedStorageFile isoStore)
		{
			String[] dics = path.Split('\\');
			Array.Resize<String>(ref dics, dics.Length - 1);

			if (dics.Length > 0)
			{
				CreateDics(dics, 0, isoStore);
			}
		}

		public static void CreateDics(String[] dics, Int32 currentDic, IsolatedStorageFile isoStore)
		{
			String path = "";

			for (int i = 0; i <= currentDic; i++)
			{
				path = System.IO.Path.Combine(path, dics[i]);
			}

			if (currentDic < dics.Length - 1)
			{
				try
				{
					isoStore.CreateDirectory(path);
					CreateDics(dics, currentDic + 1, isoStore);
				}
				catch (Exception)
				{
					
				}
			}
			else
			{
				isoStore.CreateDirectory(path);
			}
		}
	}
}
