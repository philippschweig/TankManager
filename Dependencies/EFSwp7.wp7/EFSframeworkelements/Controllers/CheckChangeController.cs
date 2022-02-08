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
using Microsoft.Phone.Controls;

namespace EFSresources.Controllers
{
	public enum CheckChangeResult
	{
		Nothing, Changed
	}

	public class CheckChangeController : Controller
	{
		new private Dictionary<Control, object> _elements;

		public CheckChangeController()
        {
			this._elements = new Dictionary<Control, object>();
        }

		public void Add(Control element, object reference)
		{
			this._elements.Add(element, reference);
		}

		public override void ActivateElement(Control element)
		{
			if (this._elements.ContainsKey(element))
			{
				this._deactivatedElements.Remove(element);
			}
			else
			{
				throw new ElementNotFoundException("Element not found in collection.");
			}
		}

		public override void DeactivateElement(Control element)
		{
			if (this._elements.ContainsKey(element))
			{
				this._deactivatedElements.Add(element);
			}
			else
			{
				throw new ElementNotFoundException("Element not found in collection.");
			}
		}

		public CheckChangeResult CheckChange()
        {
			foreach (var item in this._elements)
			{
				if (item.Key.GetType() == typeof(TextBox))
				{
					if ((item.Key as TextBox).Text != ((string)item.Value))
					{
						return CheckChangeResult.Changed;
					}
				}
				if (item.Key.GetType() == typeof(PasswordBox))
				{
					if ((item.Key as PasswordBox).Password != ((string)item.Value))
					{
						return CheckChangeResult.Changed;
					}
				}

				if (item.Key.GetType() == typeof(CheckBox))
				{
					if ((item.Key as CheckBox).IsChecked.Value != ((bool)item.Value))
					{
						return CheckChangeResult.Changed;
					}
				}

				if (item.Key.GetType() == typeof(ToggleSwitch))
				{
					if ((item.Key as ToggleSwitch).IsChecked.Value != ((bool)item.Value))
					{
						return CheckChangeResult.Changed;
					}
				}
			}

			return CheckChangeResult.Nothing;
        }
	}
}
