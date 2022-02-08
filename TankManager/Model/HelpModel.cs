using System;
using EFSresources.EFSmvvm;

namespace TankManager.Model
{
	public class HelpModel : ModelBase
	{
		public DateTime LastRefesh = DateTime.MinValue;

		public string HtmlString { get; set; }
	}
}
