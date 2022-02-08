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
using TankManager.Lokalisierung;
using System.Collections.Generic;

namespace TankManager.Model
{
	public class SpritQuelleModel
	{
		public static SpritQuelleModel Zapfsaeule = new SpritQuelleModel() { Id = 0, Name = Translation.DicPetrolPump};
		public static SpritQuelleModel Kanister = new SpritQuelleModel() { Id = 1, Name = Translation.DicCanister };

		public static List<SpritQuelleModel> SpritQuellen = new List<SpritQuelleModel>() { SpritQuelleModel.Zapfsaeule, SpritQuelleModel.Kanister };

		public Int32 Id;
		public String Name;

		public override string ToString()
		{
			//return base.ToString();
			return this.Name;
		}
	}
}
