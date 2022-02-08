using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using Microsoft.Phone.Shell;

namespace EFSresources.Controllers
{
	public class RotateController : Controller
	{
		private Control nextElement
		{
			get
			{
				if (this.focusedElementIndex < this._elements.Count - 1 && this.focusedElementIndex >= 0)
				{
					return this._elements[this.focusedElementIndex + 1];
				}

				return this._elements[0];
			}
		}

		private int focusedElementIndex
		{
			get
			{
				if (this.focusedElement != null && this.focusedElement != this.rotationStop)
				{
					return this._elements.IndexOf(this.focusedElement);
				}

				return -1;
			}
		}
		private Control focusedElement;
		public Control FocusedElement
		{
			get { return focusedElement; }
		}

		private Control rotationStop;

		private bool canRotate
		{
			get
			{
				if (this._elements.Count > 0)
				{
					return true;
				}

				return false;
			}
		}

		// Konstruktor
		public RotateController(Control _rotationStop) : base()
		{
			this.rotationStop = _rotationStop;
		}

		public RotateController(Control _rotationStop, ButtonBase _rotationControl) : this(_rotationStop)
		{
			base._controllerButton = _rotationControl;
			base._controllerButton.Click += new RoutedEventHandler(rotationButton_Click);
		}

		public RotateController(Control _rotationStop, IApplicationBarMenuItem _rotationControl) : this(_rotationStop)
		{
			base._controllerAppBarButton = _rotationControl;
			base._controllerAppBarButton.Click += new EventHandler(rotationAppBarButton_Click);
		}

		// Methoden
		override public void Add(Control element)
		{
			base.Add(element);

			element.GotFocus += new RoutedEventHandler(GotFocus);
		}

		public void Rotate()
		{
			if (this.canRotate)
			{
				if (this.focusedElementIndex == this._elements.Count - 1)
				{
					this.focusedElement = this.rotationStop;
				}
				else
				{
					this.focusedElement = this.nextElement;

					if (this._deactivatedElements.Contains(this.focusedElement))
					{
						this.Rotate();
						return;
					}
				}

				this.Focus();
			}
		}

		private void Focus()
		{
			this.focusedElement.Focus();

			if (this.focusedElement.GetType() == typeof(TextBox))
			{
				if ((this.focusedElement as TextBox).Text.Length > 0)
				{
					(this.focusedElement as TextBox).Select((this.focusedElement as TextBox).Text.Length, 0);
				}
			}
		}

		private void GotFocus(object sender, RoutedEventArgs e)
		{
			if (this.focusedElement != (sender as Control))
			{
				this.focusedElement = (sender as Control);
			}

			if (!(this.focusedElementIndex + 1 < this._elements.Count))
			{
				this.focusedElement.LostFocus += new RoutedEventHandler(LostFocus);
			}
		}

		private void LostFocus(object sender, RoutedEventArgs e)
		{
			(sender as Control).LostFocus -= LostFocus;

			this.focusedElement = this.rotationStop;
		}

		private void rotationAppBarButton_Click(object sender, EventArgs e)
		{
			this.Rotate();
		}

		private void rotationButton_Click(object sender, RoutedEventArgs e)
		{
			this.Rotate();
		}
	}
}
