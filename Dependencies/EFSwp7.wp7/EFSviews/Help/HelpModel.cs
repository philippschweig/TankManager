using System;
using EFSresources.EFSmvvm;

namespace EFSresources.Views.Help
{
	public class HelpModel : ModelBase
	{
		public DateTime LastRefesh = DateTime.MinValue;

		public string HtmlString { get; set; }
	}
}
