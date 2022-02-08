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
using EFSresources;

namespace EFSresources
{
	public class UnitPrefix
	{
		public static UnitPrefix Exa = new UnitPrefix() { Prefix = "Exa", PrefixShortcut = "E", Meaning = EFSlanguage.DicQuintillion, Factor = 1000000000000000000 };
		public static UnitPrefix Peta = new UnitPrefix() { Prefix = "Peta", PrefixShortcut = "P", Meaning = EFSlanguage.DicQuadrillion, Factor = 1000000000000000 };
		public static UnitPrefix Tera = new UnitPrefix() { Prefix = "Tera", PrefixShortcut = "T", Meaning = EFSlanguage.DicBillion, Factor = 1000000000000 };
		public static UnitPrefix Giga = new UnitPrefix() { Prefix = "Giga", PrefixShortcut = "G", Meaning = EFSlanguage.DicMilliard, Factor = 1000000000 };
		public static UnitPrefix Mega = new UnitPrefix() { Prefix = "Mega", PrefixShortcut = "M", Meaning = EFSlanguage.DicMillion, Factor = 1000000 };
		public static UnitPrefix Kilo = new UnitPrefix() { Prefix = "Kilo", PrefixShortcut = "k", Meaning = EFSlanguage.DicThousand, Factor = 1000 };
		public static UnitPrefix Hekto = new UnitPrefix() { Prefix = "Hekto", PrefixShortcut = "h", Meaning = EFSlanguage.DicHundert, Factor = 100 };
		public static UnitPrefix Deka = new UnitPrefix() { Prefix = "Deka", PrefixShortcut = "da", Meaning = EFSlanguage.DicTen, Factor = 10 };

		public String Prefix;
		public String PrefixShortcut;
		public String Meaning;
		public Double Factor;

		public static Double ConvertTo(Double normalNumber, UnitPrefix up)
		{
			return normalNumber / up.Factor;
		}

		public static Double ConvertBack(Double unitNumber, UnitPrefix up)
		{
			return unitNumber * up.Factor;
		}

		public static String ConvertToString(Double normalNumber, UnitPrefix up, String type, Boolean shortc = false, Int32 decimalPoint = -1)
		{
			String ending;

			if (shortc)
			{
				ending = up.PrefixShortcut + type;
			}
			else
			{
				ending = up.Prefix + type;
			}

			if (decimalPoint > -1)
			{
				normalNumber = Math.Round(UnitPrefix.ConvertTo(normalNumber, up), decimalPoint);
			}
			else
			{
				normalNumber = UnitPrefix.ConvertTo(normalNumber, up);
			}

			return normalNumber + " " + ending;
		}

		public static String ConvertToBestString(Double normalNumber, String type, Boolean shortc = false, Int32 decimalPoint = -1)
		{
			return UnitPrefix.ConvertToString(normalNumber, UnitPrefix.GetBestPrefix(normalNumber), type, shortc, decimalPoint);
		}

		public static UnitPrefix GetBestPrefix(Double normalNumber)
		{
			if (normalNumber < UnitPrefix.Deka.Factor)
			{
				return UnitPrefix.Deka;
			}
			else if (normalNumber < UnitPrefix.Hekto.Factor)
			{
				return UnitPrefix.Deka;
			}
			else if (normalNumber < UnitPrefix.Kilo.Factor)
			{
				return UnitPrefix.Hekto;
			}
			else if (normalNumber < UnitPrefix.Mega.Factor)
			{
				return UnitPrefix.Kilo;
			}
			else if (normalNumber < UnitPrefix.Giga.Factor)
			{
				return UnitPrefix.Mega;
			}
			else if (normalNumber < UnitPrefix.Tera.Factor)
			{
				return UnitPrefix.Giga;
			}
			else if (normalNumber < UnitPrefix.Peta.Factor)
			{
				return UnitPrefix.Tera;
			}
			else if (normalNumber < UnitPrefix.Exa.Factor)
			{
				return UnitPrefix.Peta;
			}
			else
			{
				return UnitPrefix.Exa;
			}
		}
	}
}
