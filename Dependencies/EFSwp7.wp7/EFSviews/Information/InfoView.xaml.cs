using EFSresources;
using EFSresources.EFSlibrary;
using EFSresources.EFSlogging;
using EFSresources.EFSsupporting;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows;
using EFSresources.Device;
using EFSresources.Library;
using System.Diagnostics;

namespace EFSresources.Views.Information
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

			this.InitLogic();
			this.InitUI();
		}

		private void InitLogic()
		{
			AppInfo.Source = InformationTransporter.Transport.Source;

			this.historyViewModel = new HistoryViewModel();
			this.creditsViewModel = new CreditsViewModel();
        }

		private void InitUI()
		{
			#region Header
			this.Image_Logo.Source = InformationTransporter.Transport.AppLogo;
			this.Border_Logo.DoubleTap += new EventHandler<System.Windows.Input.GestureEventArgs>(Border_Logo_DoubleTap);

			this.tblAppTitle.Text = AppInfo.Title;

			this.tblVersionHeader.SetBinding(TextBlock.TextProperty, new Binding() { Source = EFSlanguage.DicVersion });
			this.tblVersion.Text = AppInfo.Version;

			this.tblLastUpdateHeader.SetBinding(TextBlock.TextProperty, new Binding() { Source = EFSlanguage.DicLastUpdate });
			this.tblLastUpdate.Text = AppInfo.BuildDateTime.ToShortDateString();
			#endregion

			this.Pivot_Info.SelectionChanged += new SelectionChangedEventHandler(Pivot_Info_SelectionChanged);

			#region About
			this.PivotItem_About.SetBinding(PivotItem.HeaderProperty, new Binding() { Source = EFSlanguage.EFSinfoView_About });

			this.tblAppCompanyHeader.Text = EFSlanguage.DicCompany;
			this.tblAppCompany.Text = AppInfo.Company;

			this.tblEmailHeader.Text = EFSlanguage.DicEmail;
			this.hlbtnEmail.Click += new RoutedEventHandler(hlbtnEmail_Click);
			this.hlbtnEmail.Content = InformationTransporter.Transport.DeveloperEmail;

			this.tblSupportHeader.Text = EFSlanguage.DicSupport;
			this.hlbtnSupport.Content = InformationTransporter.Transport.SupportEmail;
			
			this.Button_RateReview.Content = EFSlanguage.DicRateReview;
			this.Button_RateReview.Click += new RoutedEventHandler(Button_RateReview_Click);

			this.tblCopyright.Text = "Copyright © " + AppInfo.Company + " " + AppInfo.BuildDateTime.Year;
			#endregion

			#region History
			if (InformationTransporter.Transport.HistoryViewModel == null)
			{
				this.PivotItem_History.Visibility = Visibility.Collapsed;
			}

			this.PivotItem_History.SetBinding(PivotItem.HeaderProperty, new Binding() { Source = EFSlanguage.EFSinfoView_History });
			#endregion

			#region Credits
			if (InformationTransporter.Transport.CreditsViewModel == null)
			{
				this.PivotItem_Credits.Visibility = Visibility.Collapsed;
			}
			
			this.PivotItem_Credits.SetBinding(PivotItem.HeaderProperty, new Binding() { Source = EFSlanguage.EFSinfoView_Credits });
			#endregion

			#region Diagnostic
			this.PivotItem_Diagnostic.SetBinding(PivotItem.HeaderProperty, new Binding() { Source = EFSlanguage.EFSinfoView_Diagnostic });

			this.TextBlock_StartTimeHeader.Text = EFSlanguage.DicAppLoadTime;
			this.TextBlock_CurrentRamHeader.Text = EFSlanguage.DicCurrentRamUsage;
			this.TextBlock_PeakRamHeader.Text = EFSlanguage.DicPeakRamUsage;
			this.TextBlock_MaxRamHeader.Text = EFSlanguage.DicMaxRamUsage;

			this.Button_Logs.Content = EFSlanguage.EFSlogsystem_LogViewTitle;
			this.Button_Logs.Click += new RoutedEventHandler(Button_Logs_Click);
			#endregion

			#region Feedback
			this.PivotItem_Feedback.SetBinding(PivotItem.HeaderProperty, new Binding() { Source = EFSlanguage.EFSinfoView_Feedback });
			this.PivotItem_Feedback.Loaded += new System.Windows.RoutedEventHandler(PivotItem_Feedback_Loaded);
			this.ListPicker_Feedback.ItemsSource = Feedback.All;
			
			this.TextBox_Feedback.GotFocus += new RoutedEventHandler(TextBox_Feedback_GotFocus);
			this.TextBox_Feedback.LostFocus += new RoutedEventHandler(TextBox_Feedback_LostFocus);
			this.TextBox_Feedback.TextChanged += new TextChangedEventHandler(TextBox_Feedback_TextChanged);
			#endregion
		}

		private void RefreshPerformance()
		{
			this.TextBlock_StartTime.Text = EFSperformance.Meter.GetTimeSpan().Milliseconds.ToString() + " ms";
			this.TextBlock_CurrentRam.Text = DeviceInfos.AppCurrentMemUsage;
			this.TextBlock_PeakRam.Text = DeviceInfos.AppPeakMemUsage;
			this.TextBlock_MaxRam.Text = DeviceInfos.AppMemUsageLimit;
		}

		private void HistoryLoad()
		{
			if (!this.historyViewModel.IsLoaded)
			{
				this.historyViewModel.Load(InformationTransporter.Transport.HistoryViewModel.Items);
				this.ListBox_History.SetBinding(ListBox.DataContextProperty, new Binding() { Source = this.historyViewModel });
				this.ListBox_History.SetBinding(ListBox.ItemsSourceProperty, new Binding() { Path = new PropertyPath("Items") });
			}
		}

		private void CreditsLoad()
		{
			if (!this.creditsViewModel.IsLoaded)
			{
				this.creditsViewModel.Load(InformationTransporter.Transport.CreditsViewModel.Items);
				this.ListBox_Credits.SetBinding(ListBox.DataContextProperty, new Binding() { Source = this.creditsViewModel });
				this.ListBox_Credits.SetBinding(ListBox.ItemsSourceProperty, new Binding() { Path = new PropertyPath("Items") });
			}
		}

		private void Border_Logo_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
		{
			if (this.Pivot_Info.SelectedItem == this.PivotItem_Diagnostic)
			{
				this.Button_Logs.Visibility = Visibility.Visible;
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

		private void Button_Logs_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.NavigationService.Navigate(EFSlogsystem.LogViewUri);
		}

		private void PivotItem_Feedback_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			this.AppBarIconButtonSend = new ApplicationBarIconButton() { IconUri = new Uri("/Icons/appbar.mail.send.png", UriKind.RelativeOrAbsolute), Text = EFSlanguage.DicSendOff };
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