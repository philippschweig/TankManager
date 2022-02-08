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
using Microsoft.Phone.Controls.Primitives;

namespace Trainerhilfe.ViewModels
{
	public class IntDatasource : ILoopingSelectorDataSource
	{
		// Eigenschaften
		private int _minValue;
		public int MinValue
		{
			get
			{
				return this._minValue;
			}
			set
			{
				if (value >= this.MaxValue)
				{
					throw new ArgumentOutOfRangeException("MinValue", "MinValue cannot be equal or greater than MaxValue");
				}

				this._minValue = value;
			}
		}

		private int _maxValue;
		public int MaxValue
		{
			get
			{
				return this._maxValue;
			}
			set
			{
				if (value <= this.MinValue)
				{
					throw new ArgumentOutOfRangeException("MaxValue", "MaxValue cannot be equal or lower than MinValue");
				}

				this._maxValue = value;
			}
		}

		private int _increment;
		public int Increment
		{
			get
			{
				return this._increment;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("Increment", "Increment cannot be less than or equal to zero");
				}
				
				this._increment = value;
			}
		}

		private int _selectedItem;
		public object SelectedItem
		{
			get { return _selectedItem; }
			set
			{
				if ((int)value != _selectedItem)
				{
					int valueWrapper = (int)value;

					if ((valueWrapper != _selectedItem))
					{
						object previousSelectedItem = _selectedItem;
						_selectedItem = valueWrapper;

						EventHandler handler = SelectionChanged;

						if (null != handler)
						{
							handler(this, new SelectionChangedEventArgs(new object[] { previousSelectedItem }, new object[] { _selectedItem }));
						}
					}
				}
			}
		}

		public event EventHandler SelectionChanged;

		// Konstruktor
		public IntDatasource()
		{
			this.MaxValue = 10;
			this.MinValue = 0;
			this.Increment = 1;
			this.SelectedItem = 0;
		}
		
		// Methoden
		public object GetNext(object relativeTo)
		{
			int nextValue = (int)relativeTo + this.Increment;
			
			if (nextValue > this.MaxValue)
			{
				nextValue = this.MinValue;
			}

			return nextValue;
		}

		public object GetPrevious(object relativeTo)
		{
			int prevValue = (int)relativeTo - this.Increment;

			if (prevValue < this.MinValue)
			{
				prevValue = this.MaxValue;
			}

			return prevValue;
		}
	}

}
