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
using System.Windows.Controls.Primitives;
using Microsoft.Phone.Shell;

namespace EFSresources.Controllers
{
	public class ElementNotFoundException : SystemException
	{
		public ElementNotFoundException() { }
		public ElementNotFoundException(string message) : base(message) { }
		public ElementNotFoundException(string message, Exception inner) : base(message, inner) { }
	}

	public class Controller
	{
		protected List<Control> _elements;
		protected List<Control> _deactivatedElements;

		protected ButtonBase _controllerButton;
		protected IApplicationBarMenuItem _controllerAppBarButton;

		public Controller()
		{
			this._elements = new List<Control>();
			this._deactivatedElements = new List<Control>();
		}

		virtual public void Add(Control element)
		{
			this._elements.Add(element);
		}

		virtual public void ActivateElement(Control element)
		{
			if (this._elements.Contains(element))
			{
				this._deactivatedElements.Remove(element);
			}
			else
			{
				throw new ElementNotFoundException("Element not found in collection.");
			}
		}

		virtual public void DeactivateElement(Control element)
		{
			if (this._elements.Contains(element))
			{
				this._deactivatedElements.Add(element);
			}
			else
			{
				throw new ElementNotFoundException("Element not found in collection.");
			}
		}
	}
}
