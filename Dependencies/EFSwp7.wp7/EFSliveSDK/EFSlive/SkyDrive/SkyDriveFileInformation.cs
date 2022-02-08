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

namespace EFSresources.EFSlive.SkyDrive
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Windows.Media.Imaging;
	using EFSresources.EFSmvvm;

	/// <summary> 
	/// Helper Class for the SkyDriveRepository. 
	/// </summary> 
	public class SkyDriveFileInformation : ModelBase
	{
		#region Private Members
		/// <summary> 
		/// The Filename of the current entry. 
		/// </summary> 
		private string _FileName;

		/// <summary> 
		/// The FileId of the current entry. 
		/// </summary> 
		private string _FileId;

		/// <summary> 
		/// The Upload Location of the current entry. 
		/// </summary> 
		private string _UploadLocation;

		/// <summary> 
		/// The Type of the Current Entry. 
		/// </summary> 
		private string _Type;

		/// <summary> 
		/// The Parent Id of the current Element. 
		/// </summary> 
		private string _ParentId;

		/// <summary> 
		/// The Creation Date-Tim of the Current Element. 
		/// </summary> 
		private string _CreatedTime;

		/// <summary> 
		/// The File Extenstion of the Current Entry. 
		/// </summary> 
		private string _FileExtension;

		/// <summary> 
		/// The File Description for the Current Entry. 
		/// </summary> 
		private string _FileDescription;

		/// <summary> 
		/// The icon to show beneath the filename. 
		/// </summary> 
		private string _Icon;

		/// <summary> 
		/// The source uri of the file 
		/// </summary>   
		private string _FileSource;

		/// <summary> 
		/// The link for the file 
		/// </summary> 
		private string _Link;

		/// <summary> 
		/// Save here the file-size and other informations. 
		/// </summary> 
		private string _FileSizeInformation;

		/// <summary> 
		/// Information about sharing level of the selected folder. 
		/// </summary> 
		private string _FileSharingInformation;

		/// <summary> 
		/// The tumbnail url. 
		/// </summary> 
		private string _Picture;

		#endregion

		#region PublicMembers
		/// <summary> 
		/// The Filename of the current entry. 
		/// </summary> 
		public string FileName
		{
			get
			{
				return this._FileName;
			}
			set
			{
				if (this._FileName != value)
				{
					this._FileName = value;
					this.NotifyPropertyChanged("FileName");
				}

			}
		}


		/// <summary> 
		/// The FileId of the current entry. 
		/// </summary> 
		public string FileId
		{
			get
			{
				return this._FileId;
			}
			set
			{
				this._FileId = value;
			}
		}

		/// <summary> 
		/// The Upload Location of the current entry. 
		/// </summary> 
		public string UploadLocation
		{
			get
			{
				return this._UploadLocation;
			}
			set
			{
				if (this._UploadLocation != value)
				{
					this._UploadLocation = value;
					this.NotifyPropertyChanged("UploadLocation");
				}
			}
		}

		/// <summary> 
		/// The Type of the Current Entry. 
		/// </summary> 
		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if (this._Type != value)
				{
					this._Type = value;
					this.NotifyPropertyChanged("Type");
				}
			}
		}

		/// <summary> 
		/// The Parent Id of the current Element. 
		/// </summary> 
		public string ParentId
		{
			get
			{
				return this._ParentId;
			}
			set
			{
				if (this._ParentId != value)
				{
					this._ParentId = value;
					this.NotifyPropertyChanged("ParentId");
				}
			}
		}

		/// <summary> 
		/// The Creation Date-Tim of the Current Element. 
		/// </summary> 
		public string CreatedTime
		{
			get
			{
				return this._CreatedTime;
			}
			set
			{
				if (this._CreatedTime != value)
				{
					this._CreatedTime = value;
					this.NotifyPropertyChanged("CreatedTime");
				}
			}
		}

		/// <summary> 
		/// The File Extenstion of the Current Entry. 
		/// </summary> 
		public string FileExtension
		{
			get
			{
				return this._FileExtension;
			}
			set
			{
				if (this._FileExtension != value)
				{
					this._FileExtension = value;
					this.NotifyPropertyChanged("FileExtension");
				}
			}
		}

		/// <summary> 
		/// The File Description for the Current Entry. 
		/// </summary> 
		public string FileDescription
		{
			get
			{
				return this._FileDescription;
			}
			set
			{
				if (this._FileDescription != value)
				{
					this._FileDescription = value;
					this.NotifyPropertyChanged("FileDescription");
				}
			}
		}

		/// <summary> 
		/// The icon to show beneath the filename. 
		/// </summary> 
		public string EntryImage
		{
			get
			{
				return this._Icon;
			}
			set
			{
				if (this._Icon != value)
				{
					this._Icon = value;
					this.NotifyPropertyChanged("Icon");
				}
			}
		}

		/// <summary> 
		/// The source uri of the file 
		/// </summary> 
		public string FileSource
		{
			get
			{
				return this._FileSource;
			}
			set
			{
				if (this._FileSource != value)
				{
					this._FileSource = value;
					this.NotifyPropertyChanged("FileSource");
				}
			}
		}

		/// <summary> 
		/// The link for the file 
		/// </summary> 
		public string Link
		{
			get
			{
				return this._Link;
			}
			set
			{

				if (this._Link != value)
				{
					this._Link = value;
					this.NotifyPropertyChanged("Link");
				}
			}
		}

		/// <summary> 
		/// Save here the file-size and other informations. 
		/// </summary> 
		public string FileSizeInformation
		{
			get
			{
				return this._FileSizeInformation;
			}
			set
			{
				if (this._FileSizeInformation != value)
				{
					this._FileSizeInformation = value;
					this.NotifyPropertyChanged("FileSizeInformation");
				}
			}
		}

		/// <summary> 
		/// Information about sharing level of the selected folder. 
		/// </summary> 
		public string FileSharingInformation
		{
			get
			{
				return this._FileSharingInformation;
			}
			set
			{
				if (_FileSharingInformation != value)
				{
					this._FileSharingInformation = value;
					this.NotifyPropertyChanged("FileSharingInformation");
				}
			}
		}

		/// <summary> 
		/// The tumbnail url. 
		/// </summary> 
		public string Picture
		{
			get
			{
				return this._Picture;
			}
			set
			{
				if (_Picture != value)
				{
					this._Picture = value;
					this.NotifyPropertyChanged("Picture");
				}
			}
		}

		#endregion
	}
}