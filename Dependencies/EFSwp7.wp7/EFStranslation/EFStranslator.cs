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

namespace EFSresources
{
	public class EFStranslator
	{
		private readonly EFSlanguage _resources = new EFSlanguage();

		public EFSlanguage Resources
		{
			get { return _resources; }
		} 
	}
}
