using System;
using System.Windows;
using EFSresources.EFSxamlhelpers;

namespace EFSresources.EFSlogging.View
{
	public class LogPointTemplateSelector : DataTemplateSelector
	{
		public DataTemplate Error { get; set; }
		public DataTemplate Warning { get; set; }
		public DataTemplate Info { get; set; }
		public DataTemplate Verbose { get; set; }

		public override DataTemplate SelectTemplate(Object item, DependencyObject container)
		{
			LogPoint logeintrag = item as LogPoint;
			if (logeintrag != null)
			{
				if (logeintrag.Level == LogLevel.Error)
				{
					return Error;
				}
				else if (logeintrag.Level == LogLevel.Warning)
				{
					return Warning;
				}
				else if (logeintrag.Level == LogLevel.Info)
				{
					return Info;
				}
				else
				{
					return Verbose;
				}
			}

			return base.SelectTemplate(item, container);
		}
	}
}
