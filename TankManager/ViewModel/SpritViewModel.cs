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
using TankManager.Model;
using EFSresources.EFSmvvm;

namespace TankManager.ViewModel
{
	public class SpritViewModel : ViewModelBase
	{
		public List<SpritModel> Kraftstoffe
		{
			get
			{
				return SpritModel.KraftstoffListe;
			}
		}
	}
}
