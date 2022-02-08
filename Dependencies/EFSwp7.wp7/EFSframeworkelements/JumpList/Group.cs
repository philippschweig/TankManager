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
using System.Collections.Generic;

namespace EFSresources.JumpList
{
	/// <summary>
	/// Generic Group class which has a string Title and a generic list of items
	/// that are related to the title - for example the Title could be 'k' and then
	/// all the items could have entry such as a persons surname that being with 'k'
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Group<T> : IEnumerable<T>
	{
		/// <summary>
		/// Group title - this provides the jump list titles
		/// </summary>
		public string Title
		{
			get;
			set;
		}

		/// <summary>
		/// List of items in the group
		/// </summary>
		public IList<T> Items
		{
			get;
			set;
		}

		/// <summary>
		/// True when there are items - it's used primarily to create the tiles in the jump list
		/// to indicate visibly where items exist
		/// </summary>
		public bool HasItems
		{
			get
			{
				return Items.Count > 0;
			}
		}

		/// <summary>
		/// This is used to colour the tiles - greying out those that have no entries
		/// </summary>
		public Brush GroupBackgroundBrush
		{
			get
			{
				return (SolidColorBrush)Application.Current.Resources[(HasItems) ?
							"PhoneAccentBrush" : "PhoneChromeBrush"];
			}
		}

		/// <summary>
		/// Group constructor
		/// </summary>
		/// <param name="name"></param>
		/// <param name="items"></param>
		public Group(string name, IEnumerable<T> items)
		{
			this.Title = name;
			this.Items = new List<T>(items);
		}

		/// <summary>
		/// Test for Title equality (and that object exists)
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			Group<T> that = obj as Group<T>;
			return (that != null) && (this.Title.Equals(that.Title));
		}

		/// <summary>
		/// When overriding Equals the GetHashCode has to also be overridden
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return this.Title.GetHashCode();
		}

		/// <summary>
		/// Item collection enumerator
		/// </summary>
		/// <returns></returns>
		public IEnumerator<T> GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}

		/// <summary>
		/// Item collection enumerator
		/// </summary>
		/// <returns></returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}
	}
}
