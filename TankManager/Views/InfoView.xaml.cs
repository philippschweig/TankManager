using EFSresources;
using EFSresources.EFSlibrary;
using EFSresources.EFSlogging;
using EFSresources.EFSsupporting;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using TankManager.Helper;
using System;
using TankManager.Lokalisierung;
using TankManager.ViewModel;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows;
using TankManager.Sys;

namespace TankManager.Views
{
	public partial class InfoView : PhoneApplicationPage
	{
		private IApplicationBar FeedbackAppBar;
		private ApplicationBarIconButton AppBarIconButtonSend;

		private HistoryViewModel historyViewModel;
		private CreditsViewModel creditsViewModel;

		public InfoView()
		{
			InitializeComponent();

            this.Loaded += InfoView_Loaded;

			this.historyViewModel = new HistoryViewModel();
			this.creditsViewModel = new CreditsViewModel();

			this.PivotItem_Feedback.Loaded += new System.Windows.RoutedEventHandler(PivotItem_Feedback_Loaded);
			this.Border_Logo.DoubleTap += new EventHandler<System.Windows.Input.GestureEventArgs>(Border_Logo_DoubleTap);

			this.ListPicker_Feedback.ItemsSource = Feedback.All;

			this.Button_RateReview.Content = Translation.DicRateReview;
		}

        private void InfoView_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.NavigationContext.QueryString.ContainsKey("Pivot"))
            {
                switch (this.NavigationContext.QueryString["Pivot"])
                {
                    case "1":
                        this.Pivot_Info.SelectedItem = this.PivotItem_History;
                        break;
                }
            }
        }

		private void Border_Logo_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
		{
			if (this.Pivot_Info.SelectedItem == this.PivotItem_Diagnostic)
			{
				this.Button_Logs.Visibility = Visibility.Visible;
			}
		}

		private void RefreshPerformance()
		{
			this.TextBlock_StartTime.Text = EFSperformance.Meter.GetTimeSpan().Milliseconds.ToString() + " ms";
			this.TextBlock_CurrentRam.Text = XamlHelper.AppCurrentMemUsage;
			this.TextBlock_PeakRam.Text = XamlHelper.AppPeakMemUsage;
			this.TextBlock_MaxRam.Text = XamlHelper.AppMemUsageLimit;
		}

		private void HistoryLoad()
        {
			if (!this.historyViewModel.IsLoaded)
			{
				this.historyViewModel.Load();
				this.ListBox_History.SetBinding(ListBox.DataContextProperty, new Binding() { Source = this.historyViewModel });
				this.ListBox_History.SetBinding(ListBox.ItemsSourceProperty, new Binding() { Path = new PropertyPath("Items") });
			}
        }

		private void CreditsLoad()
		{
			if (!this.creditsViewModel.IsLoaded)
			{
				this.creditsViewModel.Load();
				this.ListBox_Credits.SetBinding(ListBox.DataContextProperty, new Binding() { Source = this.creditsViewModel });
				this.ListBox_Credits.SetBinding(ListBox.ItemsSourceProperty, new Binding() { Path = new PropertyPath("Items") });
			}
		}

		private void PhoneApplicationPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "InfoView geladen");
			this.RefreshPerformance();
		}

		private void Pivot_Info_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			this.ApplicationBar = null;
			this.Focus();

			if (this.Pivot_Info.SelectedItem == this.PivotItem_About)
			{
				EFSlogsystem.Logger.WriteVerbose(this.GetType(), "About Pivot ausgewählt");
			}
			else if (this.Pivot_Info.SelectedItem == this.PivotItem_History)
			{
				EFSlogsystem.Logger.WriteVerbose(this.GetType(), "History Pivot ausgewählt");
				this.HistoryLoad();
			}
			else if (this.Pivot_Info.SelectedItem == this.PivotItem_Credits)
			{
				EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Credits Pivot ausgewählt");
				this.CreditsLoad();
			}
			else if (this.Pivot_Info.SelectedItem == this.PivotItem_Diagnostic)
			{
				EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Diagnostic Pivot ausgewählt");
				this.RefreshPerformance();
			}
			else if (this.Pivot_Info.SelectedItem == this.PivotItem_Feedback)
			{
				EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Feddback Pivot ausgewählt");
				this.ApplicationBar = this.FeedbackAppBar;
				this.ApplicationBar.IsVisible = true;
			}
		}

		private void hlbtnEmail_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "E-Mail versenden");
			//Create a new task
			EmailComposeTask email = new EmailComposeTask();
			//Add the current item’s EMail address
			email.To = "efsdeveloping@gmail.com";
			//Just a little text for the message
			email.Subject = EFSappinfos.GetAppTitle() + " Mailing";
			email.Body = "\n\n\n\nApplication Infos:\n" + EFSappinfos.GetAppVersion();
			//Launch the task
			email.Show();
		}

		private void Button_RateReview_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "MarketplaceReviewTask gestartet");
			MarketplaceReviewTask task = new MarketplaceReviewTask();
			task.Show();
		}

        private void Button_Donate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            WebBrowserTask task = new WebBrowserTask();
            task.Uri = SysConst.DonateLink;
            task.Show();
        }

		private void Button_Logs_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			NavigationService.Navigate(EFSlogsystem.LogViewUri);
		}

		private void PivotItem_Feedback_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			this.AppBarIconButtonSend = new ApplicationBarIconButton() { IconUri = new Uri("/icons/appbar.mail.send.png", UriKind.RelativeOrAbsolute), Text = Translation.DicSendOff };
			this.AppBarIconButtonSend.Click += new EventHandler(AppBarIconButtonSend_Click);
			this.AppBarIconButtonSend.IsEnabled = false;

			this.FeedbackAppBar = new ApplicationBar();
			this.FeedbackAppBar.Buttons.Add(this.AppBarIconButtonSend);
		}

		private void TextBox_Feedback_GotFocus(object sender, System.Windows.RoutedEventArgs e)
		{
			if (this.ApplicationBar != null)
			{
				this.ApplicationBar.IsVisible = false;
			}
		}

		private void TextBox_Feedback_LostFocus(object sender, System.Windows.RoutedEventArgs e)
		{
			if (this.ApplicationBar != null)
			{
				this.ApplicationBar.IsVisible = true;
			}
		}

		private void AppBarIconButtonSend_Click(object sender, EventArgs e)
		{
			//Create a new task
			EmailComposeTask email = new EmailComposeTask();
			//Add the current item’s EMail address
			email.To = "efsdeveloping@gmail.com";
			//Just a little text for the message
			email.Subject = EFSappinfos.GetAppTitle() + " - " + ((Feedback)this.ListPicker_Feedback.SelectedItem).Title;
			email.Body = this.TextBox_Feedback.Text +
				"\n\n\nApplication Infos:\n" +
				EFSappinfos.GetAppVersion() + "\n" +
				this.TextBlock_StartTime.Text + "\n" +
				this.TextBlock_CurrentRam.Text + "\n" +
				this.TextBlock_PeakRam.Text + "\n" +
				this.TextBlock_MaxRam.Text + "\n";
			//Launch the task
			email.Show();

			this.TextBox_Feedback.Text = String.Empty;
		}

		private void TextBox_Feedback_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (this.TextBox_Feedback.Text != String.Empty)
			{
				this.AppBarIconButtonSend.IsEnabled = true;
			}
			else
			{
				this.AppBarIconButtonSend.IsEnabled = false;
			}
		}
	}
}