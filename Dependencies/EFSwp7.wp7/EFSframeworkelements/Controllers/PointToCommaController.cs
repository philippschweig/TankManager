using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using EFSresources.Controllers;
using System.Diagnostics;

namespace EFSresources.Frameworkelements.Controllers
{
    public class PointToCommaController : Controller
    {
        public PointToCommaController() : base() { }

		public void Add(TextBox element)
		{
			element.TextChanged += element_TextChanged;

			base.Add(element);
		}

		private void element_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!this._deactivatedElements.Contains(sender as TextBox))
			{
                if ((sender as TextBox).Text.Contains(".") && !(sender as TextBox).Text.Contains(","))
				{
                    int start = (sender as TextBox).SelectionStart;
					(sender as TextBox).Text = (sender as TextBox).Text.Replace('.', ',');

                    if ((sender as TextBox).Text.Length > 0)
                    {
                        (sender as TextBox).Select(start, 0);
                    }
				}
			}
		}
    }
}
