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
using System.Windows.Data;
using System.Globalization;
using System.Threading;
using TankManager.Lokalisierung;
using TankManager.Model;

namespace TankManager.Helper
{
	public class StringFormatter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			String formatString = (String)parameter;

			if (!String.IsNullOrEmpty(formatString))
			{
				if (value.GetType() == (typeof(Double)) && (Double)value == 0)
				{
					return "---";
				}

				if (value != null)
				{
					return String.Format(Thread.CurrentThread.CurrentCulture, formatString, value);
				}
			}

			return value.ToString();
		}

		// No need to implement converting back on a one-way binding 
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class UnitFormatter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value.GetType() == (typeof(Double)) && (Double)value == 0)
			{
				return "---";
			}

			String formatString = ToFormatString((Units)(Int32.Parse((String)parameter)));

			if (!String.IsNullOrEmpty(formatString))
			{
				if (value != null)
				{
					return String.Format(Thread.CurrentThread.CurrentCulture, formatString, value);
				}
			}

			return value.ToString();
		}

		// No need to implement converting back on a one-way binding 
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		public enum Units { Volume, Length, Length2, Consuption, Temperature}

		public static String ToFormatString(Units unit)
        {
			switch (unit)
			{
				case Units.Volume:
					return "{0:n1} " + Translation.UnitSystemVolumeShort;
				case Units.Length:
					return "{0:n1} " + Translation.UnitSystemLengthShort;
				case Units.Length2:
					return "{0:n0} " + Translation.UnitSystemLengthShort;
				case Units.Consuption:
					return "{0:n2} " + Translation.UnitSystemConsuption;
				case Units.Temperature:
					return "{0:n1} " + Translation.UnitSystemVolumeShort;
				default:
					return null;
			}
        }
	}

	public class PictureSelector : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			PhoneTheme temp; 
			String theme = String.Empty;

			temp = App.CurrentTheme;

			if (parameter != null)
			{
				Boolean invers;
				Boolean.TryParse((string)parameter, out invers);

				if (invers)
				{
					switch (App.CurrentTheme)
					{
						case PhoneTheme.Dark:
							temp = PhoneTheme.Light;
							break;
						case PhoneTheme.Light:
							temp = PhoneTheme.Dark;
							break;
					}
				}
			}

			switch (temp)
			{
				case PhoneTheme.Dark:
					theme = "dark";
					break;
				case PhoneTheme.Light:
					theme = "light";
					break;
				default:
					theme = "dark";
					break;
			}

			if (value == null)
			{
				return new Uri("/icons/kanister.full."+ theme +".png", UriKind.RelativeOrAbsolute);
			}
			else if ((bool)value)
			{
				return new Uri("/icons/kanister.full." + theme + ".png", UriKind.RelativeOrAbsolute);
			}
			else
			{
				return new Uri("/icons/kanister.empty." + theme + ".png", UriKind.RelativeOrAbsolute);
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
