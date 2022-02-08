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
using Microsoft.Phone.Shell;
using TankManager.Lokalisierung;
using TankManager.Model;
using EFSresources.EFSframeworkelements;
using EFSresources.EFSlibrary;
using TankManager.ViewModel;
using System.Windows.Data;
using TankManager.Helper;
using EFSresources.Controllers;
using TombstoneHelper;
using EFSresources.Frameworkelements.Controllers;

namespace TankManager
{
    public partial class TankenEditView : PhoneApplicationPage
    {
		private RotateController TabController;
        private PointToCommaController CommaController;
		private Boolean isInit = false;

		private IApplicationBar AppBar;
		private IApplicationBarIconButton AppBarIconButtonNext;
		private IApplicationBarIconButton AppBarIconButtonSave;
		private IApplicationBarIconButton AppBarIconButtonDelete;

		private FahrzeugViewModel viewModel;
		public TankvorgangModel tankvorgang;

        public TankenEditView()
        {
            InitializeComponent();

			this.InitUI();
        }

		private void Init()
        {
			this.viewModel = new FahrzeugViewModel();

			if (App.NavigationObject != null)
			{
				this.tankvorgang = (TankvorgangModel)App.NavigationObject;
				App.NavigationObject = null;

				this.TextBlock_Created.Text = this.tankvorgang.ErstelltAm.ToLongDateString();
				this.DatePicker_Datum.Value = this.tankvorgang.Datum;
				this.TextBox_Liter.Text = this.tankvorgang.Liter.ToString();
				this.TextBox_Kosten.Text = this.tankvorgang.Preis.ToString();

				if (this.tankvorgang == this.viewModel.AktuellesFahrzeug.LetzterTankvorgang())
				{
					this.AppBarIconButtonDelete.IsEnabled = true;
				}

				if (this.tankvorgang.SpritQuelle.Id == SpritQuelleModel.Kanister.Id)
				{
					this.StackPanel_Kosten.Visibility = Visibility.Collapsed;
				}

				this.UpdateSaveButton();
			}
        }

		private void InitUI()
        {
			this.Loaded += new RoutedEventHandler(TankenEditView_Loaded);

			this.TabController = new RotateController(this);
			this.TabController.Add(this.TextBox_Liter);
			this.TabController.Add(this.TextBox_Kosten);

            this.CommaController = new PointToCommaController();
            this.CommaController.Add(this.TextBox_Liter);
            this.CommaController.Add(this.TextBox_Kosten);
			
			this.AppBar = new ApplicationBar();

			this.AppBarIconButtonNext = new ApplicationBarIconButton(new Uri("/icons/appbar.next.rest.png", UriKind.RelativeOrAbsolute));
			this.AppBarIconButtonNext.Text = Translation.DicNext;
			this.AppBarIconButtonNext.Click += new EventHandler(AppBarIconButtonNext_Click);
			this.AppBar.Buttons.Add(this.AppBarIconButtonNext);

			this.AppBarIconButtonSave = new ApplicationBarIconButton(new Uri("/icons/appbar.save.rest.png", UriKind.RelativeOrAbsolute));
			this.AppBarIconButtonSave.Text = Translation.DicSave;
			this.AppBarIconButtonSave.Click += new EventHandler(AppBarIconButtonSave_Click);
			this.AppBar.Buttons.Add(this.AppBarIconButtonSave);

			this.AppBarIconButtonDelete = new ApplicationBarIconButton(new Uri("/icons/appbar.delete.rest.png", UriKind.RelativeOrAbsolute));
			this.AppBarIconButtonDelete.Text = Translation.DicDelete;
			this.AppBarIconButtonDelete.Click += new EventHandler(AppBarIconButtonDelete_Click);
			this.AppBarIconButtonDelete.IsEnabled = false;
			this.AppBar.Buttons.Add(this.AppBarIconButtonDelete);

			this.ApplicationBar = this.AppBar;
        }

		private void InitEvents()
		{
			if (!this.isInit)
			{
				this.DatePicker_Datum.ValueChanged += new EventHandler<DateTimeValueChangedEventArgs>(DatePicker_Datum_ValueChanged);
				this.isInit = true;
			}

			this.TextBox_Liter.TextChanged += new TextChangedEventHandler(TextBox_TextChanged);
			this.TextBox_Kosten.TextChanged += new TextChangedEventHandler(TextBox_TextChanged);
		}

		private Boolean CheckChanging()
		{
			Boolean changed = false;

			if (this.DatePicker_Datum.Value.Value != this.tankvorgang.Datum)
			{
				changed = true;
			}

			Double liter;
			Double.TryParse(this.TextBox_Liter.Text, out liter);
			if (liter != this.tankvorgang.Liter)
			{
				changed = true;
			}

			Double kosten;
			Double.TryParse(this.TextBox_Kosten.Text, out kosten);
			if (kosten != this.tankvorgang.Preis)
			{
				changed = true;
			}

			return changed;
		}

		private void UpdateSaveButton()
		{
			if (this.CheckChanging())
			{
				this.AppBarIconButtonSave.IsEnabled = true;
			}
			else
			{
				this.AppBarIconButtonSave.IsEnabled = false;
			}
		}

		private void TankenEditView_Loaded(object sender, RoutedEventArgs e)
		{
			this.InitEvents();
		}

		private void DatePicker_Datum_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
		{
			Boolean isTrue = true;
			TankvorgangModel
				tvVor = this.viewModel.AktuellesFahrzeug.TankvorgangVor(this.tankvorgang),
				tvNach = this.viewModel.AktuellesFahrzeug.TankvorgangNach(this.tankvorgang);

			if (tvVor != null)
			{
				if (e.NewDateTime.Value.Ticks < tvVor.Datum.Ticks)
				{
					isTrue = false;
				}
			}

			if (tvNach != null)
			{
				if (e.NewDateTime.Value.Ticks > tvNach.Datum.Ticks)
				{
					isTrue = false;
				}
			}

			if (!isTrue)
			{
				this.DatePicker_Datum.Value = e.OldDateTime;

				MessageBox.Show(Translation.MessageBoxTankenEditViewDateTimeFault_Content, Translation.MessageBoxTankenEditViewDateTimeFault_Header, MessageBoxButton.OK);
			}

			this.UpdateSaveButton();
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			this.UpdateSaveButton();
		}

		private void AppBarIconButtonNext_Click(object sender, EventArgs e)
		{
			this.TabController.Rotate();
		}

		private void AppBarIconButtonSave_Click(object sender, EventArgs e)
		{
			DateTime dt = this.DatePicker_Datum.Value.Value;
			Double liter, kosten;

			Double.TryParse(this.TextBox_Liter.Text, out liter);
			Double.TryParse(this.TextBox_Kosten.Text, out kosten);

			this.viewModel.AktuellesFahrzeug.TankvorgangÄndern(tankvorgang, dt, liter, kosten);

			this.UpdateSaveButton();
		}

		private void AppBarIconButtonDelete_Click(object sender, EventArgs e)
		{
			MessageBoxResult mBox = MessageBox.Show(
				String.Format(
					Translation.MessageBoxDeleteRefueling_Content,
					this.tankvorgang.Datum.ToLongDateString(),
					String.Format(
						UnitFormatter.ToFormatString(UnitFormatter.Units.Length2),
						this.tankvorgang.GesamtKilometer)),
				Translation.MessageBoxDeleteRefueling_Header,
				MessageBoxButton.OKCancel);

			if (mBox == MessageBoxResult.OK)
			{
				if (this.viewModel.AktuellesFahrzeug.TankvorgangLöschen(tankvorgang))
				{
					this.NavigationService.GoBack();
				}
			}
		}

        #region navigation
        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            this.SaveState(e);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.RestoreState();

            this.Init();
        }
        #endregion
    }
}
