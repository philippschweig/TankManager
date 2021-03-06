using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using EFSresources.EFSdatastorage;
using EFSresources.EFSlogging;
using EFSresources.EFSmvvm;
using EFSresources.Library;
using MyToolkit.Networking;

namespace EFSresources.App.Help
{
	public class HelpViewModel : ViewModelBase
	{
		public HelpModel Help;

		public Uri HelpFileBuild;
		public string HelpPath;
		public Uri HelpFile;

		public EventHandler<EventArgs> ResponseCompleted;

		public HelpViewModel() { }

		public void Load()
		{
			this.LoadFromApplication();
		}

		public void LoadFromApplication()
		{
			using (Stream stream = DataStorage.ReadResource(this.HelpFileBuild))
			{
				if (stream != null)
				{
					StreamReader sr = new StreamReader(stream);
					this.Help = new HelpModel();
					this.Help.LastRefesh = AppInfo.BuildDateTime.AddHours(1.0);
					this.Help.HtmlString = sr.ReadToEnd();
				}
			}

			this.LoadFromIsoStore();
		}

		public void LoadFromIsoStore()
		{
			HelpModel help = DataStorage.ReadObject<HelpModel>(this.HelpPath);

			if (this.Help == null)
			{
				this.Help = new HelpModel();
			}

			if (help != null)
			{
				if (this.Help.LastRefesh.Ticks < help.LastRefesh.Ticks && !(this.Help.LastRefesh.Date.Ticks < DateTime.Now.Date.Ticks))
				{
					this.Help = help;
				}
			}

			if (this.Help.LastRefesh.Date.Ticks < DateTime.Now.Date.Ticks || Debugger.IsAttached)
			{
				this.LoadFromWeb();
			}
		}

		public void LoadFromWeb()
		{
			HttpGetRequest req = new HttpGetRequest(this.HelpFile);
			req.AutomaticDecompression = System.Net.DecompressionMethods.None;
			req.UseCache = false;

			Http.Get(req, RequestFinished);
		}

		private void RequestFinished(HttpResponse response)
		{
			if (response.Successful)
			{
				// TODO: process response outside UI thread
				Deployment.Current.Dispatcher.BeginInvoke(() =>
				{
					this.Help = new HelpModel();
					this.Help.HtmlString = response.Response;
					this.Help.LastRefesh = DateTime.Now;

					if (this.ResponseCompleted != null)
					{
						this.ResponseCompleted.Invoke(this, null);
					}

					DataStorage.WriteObject(this.HelpPath, this.Help, FileMode.Create);
				});

			}
			else
			{
				if (!response.Canceled)
				{
					this.ResponseCanceled(response);
				}
			}
		}

		private void ResponseCanceled(HttpResponse response)
		{
			// display exception
			Deployment.Current.Dispatcher.BeginInvoke(() =>
			{
				EFSlogsystem.Logger.WriteError(this.GetType(), "ResponseCanceled", response.Exception);
			});
		}
	}
}