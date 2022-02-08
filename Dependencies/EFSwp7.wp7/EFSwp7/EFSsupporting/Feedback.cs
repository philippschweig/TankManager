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
using EFSresources;

namespace EFSresources.EFSsupporting
{
	public class Feedback
	{
		public static Feedback Common = new Feedback() { Title = EFSlanguage.FeedbackItemCommon };
		public static Feedback Bug = new Feedback() { Title = EFSlanguage.FeedbackItemBug };
		public static Feedback Feature = new Feedback() { Title = EFSlanguage.FeedbackItemFeature };

		public static List<Feedback> All = new List<Feedback>() { Common, Bug, Feature };

		public string Title;

		public override string ToString()
		{
			//return base.ToString();
			return this.Title;
		}
	}
}
