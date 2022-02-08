using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using EFSresources;
using EFSresources.EFSdatastorage;
using EFSresources.EFSlogging;
using EFSresources.EFSmvvm;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TankManager.Lokalisierung;
using TankManager.ViewModel;
using System.Reflection;
using EFSresources.Library;

namespace TankManager
{
	public partial class App : Application
	{
		public static String Language;
		
		private const string modelKey = "ViewModel";
		public const string dataFile = "Data.xml";
		public static PathTool skydrivePath = new PathTool("ProgramData", "WindowsPhone", "EFSdeveloping", "TankManager");
		
		public static object NavigationObject;
		public static bool DormantState = false;
		//private const string pageKey = "Page";

		private static Color lightThemeBackground = Color.FromArgb(255, 255, 255, 255);
		private static Color darkThemeBackground = Color.FromArgb(255, 0, 0, 0);
		private static SolidColorBrush backgroundBrush;
		internal static PhoneTheme CurrentTheme
		{
			get
			{
				if (backgroundBrush == null)
					backgroundBrush = Application.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush;

				if (backgroundBrush.Color == lightThemeBackground)
					return PhoneTheme.Light;
				else if (backgroundBrush.Color == darkThemeBackground)
					return PhoneTheme.Dark;

				return PhoneTheme.Dark;
			}
		}

		//public static Einstellungen Setting;
		private static MainViewModel _viewModel = null;
		/// <summary>
		/// Eine statisches ViewModel, an das die Ansichten gebunden werden.
		/// </summary>
		/// <returns>MainViewModel-Objekt.</returns>
		public static MainViewModel ViewModel
		{
			get
			{
				// Erstellung des Ansichtsmodells verzögern bis erforderlich
				if (_viewModel == null)
					_viewModel = new MainViewModel();

				return _viewModel;
			}

			private set
			{
				_viewModel = value;
			}
		}

		/// <summary>
		/// Bietet einen einfachen Zugriff auf den Stammframe der Phone-Anwendung.
		/// </summary>
		/// <returns>Der Stammframe der Phone-Anwendung.</returns>
		public PhoneApplicationFrame RootFrame { get; private set; }

		public static App CurrentApp
		{
			get
			{
				return (App.Current) as App;
			}
		}
		public static Assembly CurrentAssembly
		{
			get
			{
				return Assembly.GetExecutingAssembly();
			}
		}

		/// <summary>
		/// Konstruktor für das Application-Objekt.
		/// </summary>
		public App()
		{
			EFSperformance.Meter.Start();
			MVVM.NotifyOn = true;
			EFSlogsystem.Logger.logcount = 8;
			EFSlogsystem.Logger.InitLogging(LogLevel.Info);

			// Globaler Handler für nicht abgefangene Ausnahmen. 
			UnhandledException += Application_UnhandledException;

			// Silverlight-Standardinitialisierung
			InitializeComponent();

			// Phone-spezifische Initialisierung
			InitializePhoneApplication();

			// Während des Debuggens Profilerstellungsinformationen zur Grafikleistung anzeigen.
			if (System.Diagnostics.Debugger.IsAttached)
			{
				EFSlogsystem.Logger.WriteWarning(this.GetType(), "Debugmodus aktiviert");
				// Zähler für die aktuelle Bildrate anzeigen.
				Application.Current.Host.Settings.EnableFrameRateCounter = true;

				// Bereiche der Anwendung hervorheben, die mit jedem Bild neu gezeichnet werden.
				//Application.Current.Host.Settings.EnableRedrawRegions = true;

				// Nicht produktiven Visualisierungsmodus für die Analyse aktivieren, 
				// in dem Bereiche einer Seite angezeigt werden, die mit einer Farbüberlagerung an die GPU übergeben wurden.
				//Application.Current.Host.Settings.EnableCacheVisualization = true;

				// Deaktivieren Sie die Leerlauferkennung der Anwendung, indem Sie die UserIdleDetectionMode-Eigenschaft des
				// PhoneApplicationService-Objekts der Anwendung auf "Disabled" festlegen.
				// Vorsicht: Nur im Debugmodus verwenden. Eine Anwendung mit deaktivierter Benutzerleerlauferkennung wird weiterhin ausgeführt
				// und verbraucht auch dann Akkuenergie, wenn der Benutzer das Handy nicht verwendet.
				PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
			}
		}

		// Code, der beim Starten der Anwendung ausgeführt werden soll (z. B. über "Start")
		// Dieser Code wird beim Reaktivieren der Anwendung nicht ausgeführt
		private void Application_Launching(object sender, LaunchingEventArgs e)
		{
            EFSlogsystem.Logger.StartLogging();
            EFSlogsystem.Logger.ChangeStatus(LogStatus.OnApplicationLifecycle);
            
			AppInfo.Source = CurrentAssembly;

			EFSlogsystem.Logger.WriteInfo(this.GetType(), "Application wird gestartet");

			if (System.Diagnostics.Debugger.IsAttached)
			{
				//IsolatedStorageExplorer.Explorer.Start("192.168.0.231");
			}

			this.LoadFromIsolatedStorage();
			ViewModel.Load();
            ViewModel.Update();
			this.SetLanguage();
			this.RootFrame.DataContext = ViewModel;

			EFSlogsystem.Logger.WriteInfo(this.GetType(), "Application gestartet");
			EFSperformance.Meter.Stop();
            EFSlogsystem.Logger.ChangeStatus(LogStatus.Running);
		}

		// Code, der ausgeführt werden soll, wenn die Anwendung aktiviert wird (in den Vordergrund gebracht wird)
		// Dieser Code wird beim ersten Starten der Anwendung nicht ausgeführt
		private void Application_Activated(object sender, ActivatedEventArgs e)
		{
			EFSlogsystem.Logger.ActivateLogging();
            EFSlogsystem.Logger.ChangeStatus(LogStatus.OnApplicationLifecycle);
			EFSlogsystem.Logger.WriteInfo(this.GetType(), "Application wird aktiviert");

			if (System.Diagnostics.Debugger.IsAttached)
			{
				//IsolatedStorageExplorer.Explorer.RestoreFromTombstone();
			}

			// Sicherstellen, dass der Anwendungszustand ordnungsgemäß wiederhergestellt wird
			if (e.IsApplicationInstancePreserved)
			{
				App.DormantState = true;
				EFSlogsystem.Logger.WriteInfo(this.GetType(), "Aktivierung aus Dormant-State");
				EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Application-State wiederhergestellt");
			}
			else
			{
				EFSlogsystem.Logger.WriteInfo(this.GetType(), "Aktivierung aus Tombstoned-State");

				this.LoadFromAppState();
				ViewModel.Load();
				this.RootFrame.DataContext = ViewModel;
			}

			EFSlogsystem.Logger.WriteInfo(this.GetType(), "Application aktiviert");
			EFSperformance.Meter.Stop();
            EFSlogsystem.Logger.ChangeStatus(LogStatus.Running);
		}

		// Code, der ausgeführt werden soll, wenn die Anwendung deaktiviert wird (in den Hintergrund gebracht wird)
		// Dieser Code wird beim Schließen der Anwendung nicht ausgeführt
		private void Application_Deactivated(object sender, DeactivatedEventArgs e)
		{
			EFSlogsystem.Logger.WriteInfo(this.GetType(), "Application wird deaktiviert");
			// Sicherstellen, dass der erforderliche Anwendungszustand hier beibehalten wird
			this.SaveToAppState();
			this.SaveToIsolatedStorage();

			EFSlogsystem.Logger.WriteInfo(this.GetType(), "Application deaktiviert");
			EFSlogsystem.Logger.DeactivateLogging();
		}

		// Code, der beim Schließen der Anwendung ausgeführt wird (z. B. wenn der Benutzer auf "Zurück" klickt)
		// Dieser Code wird beim Deaktivieren der Anwendung nicht ausgeführt
		private void Application_Closing(object sender, ClosingEventArgs e)
		{
			EFSlogsystem.Logger.WriteInfo(this.GetType(), "Application wird geschlossen");
			this.SaveToIsolatedStorage();

			EFSlogsystem.Logger.WriteInfo(this.GetType(), "Application geschlossen");
			EFSlogsystem.Logger.EndLogging();
		}

		// Code, der bei einem Navigationsfehler ausgeführt wird
		private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
		{
			EFSlogsystem.Logger.WriteWarning(this.GetType(), "Ein Navigationsfehler ist während des Ausführen aufgetreten", e.Exception.GetBaseException());
			if (System.Diagnostics.Debugger.IsAttached)
			{
				// Navigationsfehler. Unterbrechen und Debugger öffnen
				System.Diagnostics.Debugger.Break();
			}
		}

		// Code, der bei nicht behandelten Ausnahmen ausgeführt wird
		private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
		{
			EFSlogsystem.Logger.WriteError(this.GetType(), "Eine unbehandelte Exception ist während des Ausführen aufgetreten", e.ExceptionObject);
			MessageBox.Show(Translation.MessageBoxApplicationUnhandeldException);

			if (System.Diagnostics.Debugger.IsAttached)
			{
				// Eine nicht behandelte Ausnahme ist aufgetreten. Unterbrechen und Debugger öffnen
				System.Diagnostics.Debugger.Break();
			}
		}

		private void SaveToAppState()
		{
			PhoneApplicationService.Current.State[modelKey] = ViewModel;
			//PhoneApplicationService.Current.State[pageKey] = this.CurrentPage();
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "In Tombstone gesichert");
		}

		private void LoadFromAppState()
		{
			if (PhoneApplicationService.Current.State.ContainsKey(modelKey))
			{
				ViewModel = (MainViewModel)PhoneApplicationService.Current.State[modelKey];
				EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Aus Tombstone geladen");
			}
			//if (PhoneApplicationService.Current.State.ContainsKey(pageKey))
			//{
			//    PhoneApplicationPage temp = this.CurrentPage();
			//    temp = (PhoneApplicationPage)PhoneApplicationService.Current.State[pageKey];
			//}
		}

		public void SaveToIsolatedStorage()
		{
			// persist the data using isolated storage
			//IsolatedStorageSettings.ApplicationSettings["Language"] = App.Language;
			DataStorage.WriteObject(dataFile, ViewModel, FileMode.Create);
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "In IsoStore gesichert");
		}

		public void LoadFromIsolatedStorage()
		{
			// load the view model from isolated storage
			//App.Language = (String)IsolatedStorageSettings.ApplicationSettings["Language"];
			ViewModel = (MainViewModel)DataStorage.ReadObject(dataFile, typeof(MainViewModel));
			EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Aus IsoStore geladen");
		}

		private PhoneApplicationPage CurrentPage()
		{
			return (PhoneApplicationPage)this.RootFrame.Content;
		}

		private void SetLanguage()
		{
			if (ViewModel.UserEinstellungen.Sprache != null)
			{
				CultureInfo c = new CultureInfo(ViewModel.UserEinstellungen.Sprache);

				Thread.CurrentThread.CurrentCulture = c;
				Thread.CurrentThread.CurrentUICulture = c;
				EFSlogsystem.Logger.WriteInfo(this.GetType(), "Sprache geladen");
			}

			EFSlogsystem.Logger.WriteInfo(this.GetType(), "Sprache auf " + Thread.CurrentThread.CurrentCulture.DisplayName + " gesetzt");
		}

		#region Initialisierung der Phone-Anwendung

		// Doppelte Initialisierung vermeiden
		private bool phoneApplicationInitialized = false;

		// Fügen Sie keinen zusätzlichen Code zu dieser Methode hinzu
		private void InitializePhoneApplication()
		{
			if (phoneApplicationInitialized)
				return;

			// Frame erstellen, aber noch nicht als RootVisual festlegen. Dadurch kann der Begrüßungsbildschirm
			// aktiv bleiben, bis die Anwendung bereit für das Rendern ist.
			RootFrame = new PhoneApplicationFrame();
			RootFrame.Navigated += CompleteInitializePhoneApplication;

			// Navigationsfehler behandeln
			RootFrame.NavigationFailed += RootFrame_NavigationFailed;

			// Sicherstellen, dass keine erneute Initialisierung erfolgt
			phoneApplicationInitialized = true;
		}

		// Fügen Sie keinen zusätzlichen Code zu dieser Methode hinzu
		private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
		{
			// Visuelle Stammkomponente festlegen, sodass die Anwendung gerendert werden kann
			if (RootVisual != RootFrame)
				RootVisual = RootFrame;

			// Dieser Handler wird nicht mehr benötigt und kann entfernt werden
			RootFrame.Navigated -= CompleteInitializePhoneApplication;
		}

		#endregion
	}

	public enum PhoneTheme { Dark, Light }
}