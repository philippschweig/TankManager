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
	public enum CheckControlResult
	{
		CheckSuccess, CheckFailed, CheckPasswordFailed, CheckEmailFailed
	}

	public class CheckControlEventArgs : EventArgs
	{
		public CheckControlResult Result;

		public CheckControlEventArgs() { }
	}

	public class CheckController : Controller
	{
		private Dictionary<Control, Control> _passwordElements;
		private List<Control> _emailElements;

		public EventHandler<CheckControlEventArgs> CheckHandler;

		// Konstruktor
		public CheckController() : base()
		{
			this._passwordElements = new Dictionary<Control, Control>();
			this._emailElements = new List<Control>();
		}

		public CheckController(ButtonBase checkButton) : this()
		{
			this._controllerButton = checkButton;
			this._controllerButton.Click += new RoutedEventHandler(_checkButton_Click);
		}

		public CheckController(IApplicationBarMenuItem checkControl) : this()
		{
			this._controllerAppBarButton = checkControl;
			this._controllerAppBarButton.Click += new EventHandler(_checkAppBarMenuItem_Click);
		}

		// Methoden
		public void Add(PasswordBox passwordElement, PasswordBox passwordElement2)
		{
			this.Add(passwordElement);
			this.Add(passwordElement2);
			this._passwordElements.Add(passwordElement, passwordElement2);
		}

		public void AddEmail(Control element)
		{
			this.Add(element);
			this._emailElements.Add(element);
			
		}

		public bool Check()
        {
			bool check = true;

			foreach (var item in this._elements)
			{
				if (!this._deactivatedElements.Contains(item))
				{
					if (item.GetType() == typeof(TextBox))
					{
						if ((item as TextBox).Text == String.Empty)
						{
							check = false;
						}
					}

					if (item.GetType() == typeof(PasswordBox))
					{
						if ((item as PasswordBox).Password == String.Empty)
						{
							check = false;
						}
					}
				}
			}

			return check;
        }

		public bool CheckPasswordElements()
        {
			bool check = true;

			foreach (var item in this._passwordElements)
			{
				if (item.Key.GetType() == typeof(PasswordBox) && item.Value.GetType() == typeof(PasswordBox))
				{
					if ((item.Key as PasswordBox).Password != (item.Value as PasswordBox).Password)
					{
						check = false;
					}
				}
			}

			return check;
        }

		public bool CheckEmail()
        {
			bool check = true;

			foreach (var item in this._emailElements)
			{
				if (item.GetType() == typeof(TextBox))
				{
					// Email Controll !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
					//
					//
					//
					//
					//
					//
					//
					//
				}
			}

			return check;
        }

		private void CheckMethod(object sender)
        {
            if (!this.Check())
			{
				if (this.CheckHandler != null)
				{
					this.CheckHandler.Invoke(sender, new CheckControlEventArgs() { Result = CheckControlResult.CheckFailed });
				}
			}
			else if (!this.CheckPasswordElements())
			{
				if (this.CheckHandler != null)
				{
					this.CheckHandler.Invoke(sender, new CheckControlEventArgs() { Result = CheckControlResult.CheckPasswordFailed });
				}
			}
			else if (!this.CheckEmail())
			{
				if (this.CheckHandler != null)
				{
					this.CheckHandler.Invoke(sender, new CheckControlEventArgs() { Result = CheckControlResult.CheckEmailFailed });
				}
			}
			else
			{
				if (this.CheckHandler != null)
				{
					this.CheckHandler.Invoke(sender, new CheckControlEventArgs() { Result = CheckControlResult.CheckSuccess });
				}
			}
        }

		private void _checkButton_Click(object sender, RoutedEventArgs e)
		{
			this.CheckMethod(sender);
		}

		private void _checkAppBarMenuItem_Click(object sender, EventArgs e)
		{
			this.CheckMethod(sender);
		}
	}
}
