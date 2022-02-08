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

namespace EFSresources.EFSexceptions
{
	public class EFSinnerException
	{
		public String Message;
		public String StackTrace;

		public EFSinnerException()
		{
		}
		public EFSinnerException(Exception e)
		{
			this.Message = e.Message;
			this.StackTrace = e.StackTrace;
		}
	}
}
