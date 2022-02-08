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
using System.Diagnostics;

namespace EFSresources.EFSconverter
{
	public class UnitSystem
	{
		public static readonly UnitSystem Metrical = new UnitSystem()
		{
			Name = EFSlanguage.UnitSystem_Metrical_Name,
			Length2 = new Unit() { Name = EFSlanguage.UnitSystem_Metrical_Length2_Name, Shortcut = EFSlanguage.UnitSystem_Metrical_Length2_Shortcut },
			Volume2 = new Unit() { Name = EFSlanguage.UnitSystem_Metrical_Volume2_Name, Shortcut = EFSlanguage.UnitSystem_Metrical_Volume2_Shortcut }
		};
		public static readonly UnitSystem ImperialUK = new UnitSystem()
		{
			Name = EFSlanguage.UnitSystem_ImperialUK_Name,
			Length2 = new Unit() { Name = EFSlanguage.UnitSystem_ImperialUS_Length2_Name, Shortcut = EFSlanguage.UnitSystem_ImperialUS_Length2_Shortcut, Converter = 0.621371192 },
			Volume2 = new Unit() { Name = EFSlanguage.UnitSystem_ImperialUS_Volume2_Name, Shortcut = EFSlanguage.UnitSystem_ImperialUS_Volume2_Shortcut, Converter = 0.219969 }
		};
		public static readonly UnitSystem ImperialUS = new UnitSystem()
		{
			Name = EFSlanguage.UnitSystem_ImperialUS_Name,
			Length2 = new Unit() { Name = EFSlanguage.UnitSystem_ImperialUS_Length2_Name, Shortcut = EFSlanguage.UnitSystem_ImperialUS_Length2_Shortcut, Converter = 0.621371192 },
			Volume2 = new Unit() { Name = EFSlanguage.UnitSystem_ImperialUS_Volume2_Name, Shortcut = EFSlanguage.UnitSystem_ImperialUS_Volume2_Shortcut, Converter = 0.264172 }
		};

		public String Name;

		public Unit Volume;
		public Unit Volume2;
		public Unit Length;
		public Unit Length2;
		public Unit Temperature;
		public Unit Temperature2;

		public UnitSystem() { }
	}

	public partial class Unit
	{
		public String Name;
		public String Shortcut;
		public Double Converter = 1;

		public Double Format(Double value)
        {
			Debug.WriteLine("value = " + value + "; return = " + value * Converter);
			return value * Converter;
        }

		public Double FormatBack(Double value)
		{
			Debug.WriteLine("value = " + value + "; return = " + value / Converter);
			return value / Converter;
		}
	}
}
