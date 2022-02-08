using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Automanager.Models;
using System.Diagnostics;
using Automanager.Lokalisierung;
using System.Globalization;
using Microsoft.Phone.Shell;

namespace Automanager.Views
{
	public partial class TankenAddPage : PhoneApplicationPage
	{
		public TankenAddPage()
		{
			InitializeComponent();

			ApplicationTitle.Text = Translation.AppTitle.ToUpper(CultureInfo.CurrentCulture);

			ApplicationBarIconButton AppBarIconButtonSave = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
			AppBarIconButtonSave.Text = Translation.AppBarSave;

			ApplicationBarIconButton AppBarIconButtonCancle = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
			AppBarIconButtonCancle.Text = Translation.AppBarCancle;

			TextBox_Km.Text = App.ViewModel.aktuellesAuto.GesamtKilometer.ToString();
		}
		
		private void ApplicationBarIconButton_Save_Click(object sender, System.EventArgs e)
		{
			// TODO: Ereignishandlerimplementierung hier einfügen.
			App.ViewModel.aktuellesAuto.Tanke(Double.Parse(TextBox_Km.Text), Double.Parse(TextBox_Liter.Text), Double.Parse(TextBox_Kosten.Text));

			NavigationService.GoBack();
		}

		private void ApplicationBarIconButton_Cancel_Click(object sender, System.EventArgs e)
		{
			// TODO: Ereignishandlerimplementierung hier einfügen.
		}
	}
}