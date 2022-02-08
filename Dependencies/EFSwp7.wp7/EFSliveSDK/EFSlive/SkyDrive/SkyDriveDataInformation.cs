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
using EFSresources.EFSmvvm;
using System.Collections.Generic;
using System.Diagnostics;

namespace EFSresources.EFSlive.SkyDrive
{
	public class SkyDriveDataInformation : ModelBase
	{
		#region Propertys
		private string _id;
		public string Id
		{
			get
			{
				return _id;
			}
			set
			{
				if (value != _id)
				{
					_id = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Id");
				}
			}
		}

		private SkyDriveDataInformation _from;
		public SkyDriveDataInformation From
		{
			get
			{
				return _from;
			}
			set
			{
				if (value != _from)
				{
					_from = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("From");
				}
			}
		}

		private string _name;
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				if (value != _name)
				{
					_name = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Name");
				}
			}
		}

		private string _description;
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				if (value != _description)
				{
					_description = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Description");
				}
			}
		}

		private string _parent_id;
		public string Parent_Id
		{
			get
			{
				return _parent_id;
			}
			set
			{
				if (value != _parent_id)
				{
					_parent_id = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Parent_Id");
				}
			}
		}

		private string _upload_location;
		public string Upload_Location
		{
			get
			{
				return _upload_location;
			}
			set
			{
				if (value != _upload_location)
				{
					_upload_location = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Upload_Location");
				}
			}
		}

		private bool _is_embeddable;
		public bool Is_Embeddable
		{
			get
			{
				return _is_embeddable;
			}
			set
			{
				if (value != _is_embeddable)
				{
					_is_embeddable = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Is_Embeddable");
				}
			}
		}

		private int _count;
		public int Count
		{
			get
			{
				return _count;
			}
			set
			{
				if (value != _count)
				{
					_count = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Count");
				}
			}
		}

		private string _link;
		public string Link
		{
			get
			{
				return _link;
			}
			set
			{
				if (value != _link)
				{
					_link = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Link");
				}
			}
		}

		private string _type;
		public string Type
		{
			get
			{
				return _type;
			}
			set
			{
				if (value != _type)
				{
					_type = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Type");
				}
			}
		}

		private SkyDriveDataInformation _shared_with;
		public SkyDriveDataInformation Shared_With
		{
			get
			{
				return _shared_with;
			}
			set
			{
				if (value != _shared_with)
				{
					_shared_with = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Shared_With");
				}
			}
		}

		#region Shared Width
		private string _access;
		public string Access
		{
			get
			{
				return _access;
			}
			set
			{
				if (value != _access)
				{
					_access = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Access");
				}
			}
		}
		#endregion

		private DateTime _created_time;
		public DateTime Created_Time
		{
			get
			{
				return _created_time;
			}
			set
			{
				if (value != _created_time)
				{
					_created_time = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Created_Time");
				}
			}
		}

		private DateTime _updated_time;
		public DateTime Updated_Time
		{
			get
			{
				return _updated_time;
			}
			set
			{
				if (value != _updated_time)
				{
					_updated_time = value;
					//Call NotifyPropertyChanged, when the source property is updated
					NotifyPropertyChanged("Updated_Time");
				}
			}
		}
		#endregion

		public SkyDriveDataInformation()
		{
			
		}

		public SkyDriveDataInformation(Dictionary<string, object> dataItems)
		{
			this.Id = this.SetPropety<string>("id", ref dataItems);
			this.From = this.SetPropety("from", ref dataItems);
			this.Name = this.SetPropety<string>("name", ref dataItems);
			this.Description = this.SetPropety<string>("description", ref dataItems);
			this.Parent_Id = this.SetPropety<string>("parent_id", ref dataItems);
			this.Upload_Location = this.SetPropety<string>("upload_location", ref dataItems);
			this.Is_Embeddable = this.SetPropety<bool>("is_embeddable", ref dataItems);
			this.Count = this.SetPropety<int>("count", ref dataItems);
			this.Link = this.SetPropety<string>("link", ref dataItems);
			this.Type = this.SetPropety<string>("type", ref dataItems);
			this.Shared_With = this.SetPropety("shared_with", ref dataItems);
			this.Access = this.SetPropety<string>("access", ref dataItems);
			this.Created_Time = this.SetPropetyDateTime("created_time", ref dataItems);
			this.Updated_Time = this.SetPropetyDateTime("updated_time", ref dataItems);
		}

		private T SetPropety<T>(string key, ref Dictionary<string, object> dataItems)
        {
			if (dataItems.ContainsKey(key))
			{
				return (T)dataItems[key];
			}
			return default(T);
        }

		private SkyDriveDataInformation SetPropety(string key, ref Dictionary<string, object> dataItems)
		{
			if (dataItems.ContainsKey(key))
			{
				return new SkyDriveDataInformation((Dictionary<string, object>)dataItems[key]);
			}

			return null;
		}

		private DateTime SetPropetyDateTime(string key, ref Dictionary<string, object> dataItems)
		{
			if (dataItems.ContainsKey(key))
			{
				DateTime dt;
				DateTime.TryParse(dataItems[key] as String, out dt);
				return dt;
			}

			return new DateTime(0);
		}
	}
}
