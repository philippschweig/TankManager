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
using System.Windows.Data;
using System.IO;

namespace EFSresources.EFSnetwork
{
	public class HttpRequest
	{
		public const string POST = "POST";
		public const string GET = "GET";

		private HttpWebRequest req;

		// @http://www.abzcompany.net/infoservice.svc/json/GetAllData
		// "application/json; charset=utf-8"
		public HttpRequest(string uri, string contentType, string method, AsyncCallback callback)
		{
			this.req = (HttpWebRequest)WebRequest.Create(uri);
			this.req.Method = method;
			this.req.ContentType = contentType;
			this.req.BeginGetResponse(callback, this.req);
		}

		public static void GetResponse(IAsyncResult asyncResult)
		{
			WebResponse response = ((HttpWebRequest)asyncResult.AsyncState).EndGetResponse(asyncResult);

			using (StreamReader reader = new StreamReader(response.GetResponseStream()))
			{
				string responseString = reader.ReadToEnd();
				reader.Close();

				//deserialize using datacontract serializer (not shown)

			}
		}
	}
}
