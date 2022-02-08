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
using EFSresources.EFSlogging;

namespace TankManager.Model
{
	public class SpritModel
	{
		public static SpritModel Super = new SpritModel() { id = 0, typ = 0, name = "Super" };
		public static SpritModel SuperSplus = new SpritModel() { id = 1, typ = 0, name = "Super Plus" };
		public static SpritModel SuperE10 = new SpritModel() { id = 2, typ = 0, name = "Super E10" };
		public static SpritModel Diesel = new SpritModel() { id = 3, typ = 1, name = "Diesel" };
		public static SpritModel Autogas = new SpritModel() { id = 4, typ = 2, name = "Autogas" };
		public static SpritModel Erdgas = new SpritModel() { id = 5, typ = 3, name = "Erdgas" };
		public static SpritModel Elektro = new SpritModel() { id = 6, typ = 4, name = "Elektrizität" };

		public static List<SpritModel> KraftstoffListe = new List<SpritModel>() { Super, SuperSplus, SuperE10, Diesel, Autogas, Erdgas };

		public Int16 id { get; set; }

		/* Typen:
		 * 0 - Benzin
		 * 1 - Diesel
		 */
		public Int16 typ { get; set; }
		public String name { get; set; }

		public SpritModel()
		{
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "SpritModel initialisiert");
		}

		public override string ToString()
		{
			//return base.ToString();
			return this.name;
		}
	}
}
