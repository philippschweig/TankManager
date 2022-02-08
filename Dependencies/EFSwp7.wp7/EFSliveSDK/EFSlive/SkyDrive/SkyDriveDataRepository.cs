﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using EFSresources.EFSmvvm;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using EFSresources.EFSlogging;

namespace EFSresources.EFSlive.SkyDrive
{
	public class SkyDriveDataRepository : ModelBase
	{

		#region Private Members
		/// <summary>
		/// All Files in the CurrentDirectory.
		/// </summary>
		private ObservableCollection<SkyDriveFileInformation> _Files;


		/// <summary>
		/// All Data in the CurrentDirectory.
		/// </summary>
		private ObservableCollection<SkyDriveDataInformation> _Data;

		
		/// <summary> 
		/// Is the client still connected? 
		/// </summary> 
		private bool _IsConnected;

		#endregion

		#region Public Members 
		/// <summary> 
		/// All Files in the CurrentDirectory. 
		/// </summary> 
		public ObservableCollection<SkyDriveFileInformation> Files
		{
			get
			{
				if (this._Files == null)
				{
					this._Files = new ObservableCollection<SkyDriveFileInformation>();
				}
				return this._Files;
			}
			set
			{
				if (_Files != value) 
				{
					this._Files = value;
					this.NotifyPropertyChanged("Files");
				}
			}
		}


		/// <summary> 
		/// All Data in the CurrentDirectory. 
		/// </summary> 
		public ObservableCollection<SkyDriveDataInformation> Data
		{
			get
			{
				return this._Data;
			}
			set
			{
				if (_Data != value)
				{
					this._Data = value;
					this.NotifyPropertyChanged("Data");
				}
			}
		}
		

		/// <summary> 
		/// Is the client still connected? 
		/// </summary> 
		public bool IsConnected 
		{
			get 
			{
				return this._IsConnected;
			}
			set 
			{
				if (_IsConnected != value) 
				{
					this._IsConnected = value; 
					this.NotifyPropertyChanged("IsConnected");
				}
			}
		}

		#endregion

		public SkyDriveDataRepository()
		{
				this.Files = new ObservableCollection<SkyDriveFileInformation>();
		}

		public SkyDriveDataRepository(IDictionary<string, object> result)
		{
			if (result.ContainsKey("data"))
			{
				List<object> data = (List<object>)result["data"];
				this.Data = new ObservableCollection<SkyDriveDataInformation>();

				foreach (var item in data)
				{
					this.Data.Add(new SkyDriveDataInformation((Dictionary<string, object>)item));
				}
			}
		}

		public SkyDriveDataInformation CheckItemExists(String itemName)
		{
			foreach (var item in this.Data)
			{
				if (item.Name == itemName)
				{
					EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Item<" + itemName + "> auf SkyDrive vorhanden");
					return item;
				}
			}

			EFSlogsystem.Logger.WriteWarning(this.GetType(), "Item<" + itemName + "> nicht auf SkyDrive vorhanden");
			return null;
		}
	}
}