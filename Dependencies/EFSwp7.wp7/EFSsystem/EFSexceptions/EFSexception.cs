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
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Xml.Linq;

namespace EFSresources.EFSexceptions
{
	public class EFSexception
	{
		public String XmlData;
		public EFSinnerException InnerException;
		public String Message;
		public String StackTrace;
		public int HResult;

		public EFSexception()
		{

		}

		public EFSexception(Exception ex)
		{
			if (ex.InnerException != null)
			{
				this.InnerException = new EFSinnerException(ex.InnerException);
			}
			this.Message = ex.Message;
			this.StackTrace = ex.StackTrace;
			//this.HResult = SysException.getHResult(ex);

			/*DataContractSerializer serializer = new DataContractSerializer(e.Data.GetType());

			using (StringWriter sw = new StringWriter())
			{
				using (XmlWriter writer = new XmlWriter())
				{
					// add formatting so the XML is easy to read in the log

					serializer.WriteObject(writer, dict);

					writer.Flush();

					return sw.ToString();
				}
			}*/

		}
	}
}
