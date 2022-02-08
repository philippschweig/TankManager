using System;
using System.Windows;
using System.Windows.Controls;

namespace EFSresources.EFSxamlhelpers
{
	public class DataTemplateSelector : ContentControl
	{
		public virtual DataTemplate SelectTemplate(Object item, DependencyObject container)
		{
			return null;
		}

		protected override void OnContentChanged(Object oldContent, Object newContent)
		{
			base.OnContentChanged(oldContent, newContent);

			ContentTemplate = SelectTemplate(newContent, this);
		}
	}
}
