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
using Microsoft.Live;
using System.Collections.Generic;
using EFSresources.EFSlogging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using EFSresources.EFSlibrary;
using EFSresources;

namespace EFSresources.EFSlive.SkyDrive
{
	public class SkyDriveClient
	{
		private LiveConnect Live;

		public event EventHandler<LiveOperationCompletedEventArgs> UploadCompleted;
		public event EventHandler<SkyDriveClientArgs> UploadFailed;
		public event EventHandler<LiveDownloadCompletedEventArgs> DownloadCompleted;
		public event EventHandler<SkyDriveClientArgs> DownloadFailed;

        public bool IsOperation = false;

		public SkyDriveClient(LiveConnect live)
		{
			this.Live = new LiveConnect(live.Session);
		}

		// Returns FolderId if Folder Exists, else Returns Empty String
		public String CheckFolderExists(String folderName, LiveOperationCompletedEventArgs e)
		{
			if (e.Result != null)
			{
				Dictionary<string, object> folderData = (Dictionary<string, object>)e.Result;

				if (folderData.ContainsKey("data"))
				{
					List<object> folders = (List<object>)folderData["data"];

					foreach (object item in folders)
					{
						Dictionary<string, object> folder = (Dictionary<string, object>)item;
						if (folder["name"].ToString() == folderName)
						{
							EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Ordner<" + folderName + "> auf SkyDrive vorhanden.");
							return folder["id"].ToString();
						}
					}

					EFSlogsystem.Logger.WriteInfo(this.GetType(), "Ordner<" + folderName + "> nicht auf SkyDrive vorhanden.");
					return String.Empty;
				}

				EFSlogsystem.Logger.WriteInfo(this.GetType(), "Keine Data Result vorhanden");
				return String.Empty;
			}

			EFSlogsystem.Logger.WriteWarning(this.GetType(), "LiveOperationCompleted Result ist leer.", e.Error);
			return null;
		}

		public SkyDriveDataInformation CheckItemExists(String itemName, SkyDriveDataRepository skyData)
		{
			if (skyData != null && skyData.Data != null)
			{
				foreach (var item in skyData.Data)
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

			EFSlogsystem.Logger.WriteWarning(this.GetType(), "Keine Daten von SkyDrive erhalten");
			return null;
		}

		//public String CreateFolder(String folderName)
		//{
		//    String skyDriveFolderId = String.Empty;

		//    Dictionary<string, object> skyDriveFolderData = new Dictionary<string, object>();
		//    skyDriveFolderData.Add("name", folderName);

		//    skyDriveFolderId = this.CheckFolderExists(folderName).Result;

		//    if (String.IsNullOrEmpty(skyDriveFolderId))
		//    {
		//        if (!this.PostCompletedSet)
		//        {
		//            this.Live.Client.PostCompleted += (sender, args) =>
		//            {
		//                if (args.Error == null)
		//                {
		//                    Dictionary<string, object> folder = (Dictionary<string, object>)args.Result;
		//                    skyDriveFolderId = folder["id"].ToString(); //grab the folder ID
		//                }
		//                else
		//                {
		//                    skyDriveFolderId = String.Empty;
		//                    EFSlogsystem.Logger.WriteError(this.GetType(), "Fehler beim Erstellen des Ordners.", args.Error);
		//                }
		//            };

		//            this.PostCompletedSet = true;
		//        }

		//        this.Live.Client.PostAsync("me/skydrive", skyDriveFolderData); //creating the folder in Skydrive
		//    }

		//    return skyDriveFolderId;
		//}

		#region Upload
		public Boolean UploadFile(Stream fileStream, String fileName, PathTool folderPath = null, Boolean askOverwrite = true)
		{
            if (!this.IsOperation)
            {
                if (askOverwrite)
                {
                    if (MessageBoxResult.Cancel == MessageBox.Show(EFSlanguage.SkyDriveUploadOverwrite_MessageBox_Content, String.Empty, MessageBoxButton.OKCancel))
                    {
                        fileStream.Flush();
                        fileStream.Dispose();
                        return false;
                    }
                }

                Dictionary<string, object> userState = new Dictionary<string, object>();
                userState.Add("fileStream", fileStream);
                userState.Add("fileName", fileName);
                userState.Add("folderStack", folderPath.GetPathList());
                userState.Add("parentFolderId", "me/skydrive");
                userState.Add("folder", null);

                this.Live.Client.GetCompleted += new EventHandler<LiveOperationCompletedEventArgs>(UploadOnGetCompleted);

                try
                {
                    this.Live.Client.GetAsync(userState["parentFolderId"] + "/files", userState);
                    this.IsOperation = true;
                }
                catch (Exception ex)
                {
                    EFSlogsystem.Logger.WriteWarning(this.GetType(), "Eine Exception in UploadFile ist aufgetreten", ex);
                }

                return true;
            }

            return false;
		}

		//private void UploadOnGetCompleted(object sender, LiveOperationCompletedEventArgs e)
		//{
		//    this.Live.Client.GetCompleted -= UploadOnGetCompleted;

		//    Dictionary<string, object> operationDic = (Dictionary<string, object>)e.UserState;
		//    List<string> pathList = (List<string>)operationDic["folderPath"];

		//    String folderId;

		//    if (operationDic.ContainsKey("lastFolderId"))
		//    {
		//        folderId = (String)operationDic["lastFolderId"];
		//    }
		//    else
		//    {
		//        folderId = this.CheckFolderExists(pathList[0], e);
		//    }

		//    if (folderId == null)
		//    {
		//        EFSlogsystem.Logger.WriteWarning(this.GetType(), "Upload konnte nicht durchgeführt werden");

		//        if (this.UploadFailed != null)
		//        {
		//            this.UploadFailed.Invoke(sender, e);
		//        }

		//        return;
		//    }

		//    if (folderId == String.Empty)
		//    {
		//        Dictionary<string, object> folderData = new Dictionary<string, object>();
		//        folderData.Add("name", pathList[0]);

		//        if (pathList.Count > 1)
		//        {
		//            pathList.RemoveAt(0);
		//        }

		//        this.Live.Client.PostCompleted += new EventHandler<LiveOperationCompletedEventArgs>(UploadOnPostCompleted);
		//        this.Live.Client.PostAsync((String)operationDic["parentFolderId"], folderData, operationDic);
		//        return;
		//    }

		//    if (pathList.Count > 1)
		//    {
		//        pathList.RemoveAt(0);

		//        if (!String.IsNullOrEmpty(folderId))
		//        {
		//            operationDic["parentFolderId"] = folderId;

		//            this.Live.Client.GetCompleted += new EventHandler<LiveOperationCompletedEventArgs>(UploadOnGetCompleted);
		//            this.Live.Client.GetAsync(folderId + "/files", operationDic);
		//        }
		//    }
		//    else if (pathList.Count == 1)
		//    {
		//        this.Live.Client.UploadCompleted += new EventHandler<LiveOperationCompletedEventArgs>(OnUploadCompleted);
		//        this.Live.Client.UploadAsync(folderId, (string)operationDic["fileName"], true, (Stream)operationDic["fileStream"], operationDic);
		//    }
		//}

		private void UploadOnGetCompleted(object sender, LiveOperationCompletedEventArgs e)
		{
			this.Live.Client.GetCompleted -= UploadOnGetCompleted;

			Dictionary<string, object> userState = (Dictionary<string, object>)e.UserState;

			if (e.Error == null)
			{
				SkyDriveDataRepository skyData = new SkyDriveDataRepository(e.Result);
				List<string> folderStack = (List<string>)userState["folderStack"];

				if (folderStack.Count == 0)
				{
					SkyDriveDataInformation folder = userState["folder"] as SkyDriveDataInformation;

					this.Live.Client.UploadCompleted += new EventHandler<LiveOperationCompletedEventArgs>(OnUploadCompleted);

					if (folder != null)
					{
						this.Live.Client.UploadAsync(folder.Id, (string)userState["fileName"], (Stream)userState["fileStream"], OverwriteOption.Overwrite, userState);
					}
					else
					{
                        this.Live.Client.UploadAsync((string)userState["parentFolderId"], (string)userState["fileName"], (Stream)userState["fileStream"], OverwriteOption.Overwrite, userState);
					}
				}
				else if (folderStack.Count > 0)
				{
					SkyDriveDataInformation folder = this.CheckItemExists(folderStack[0], skyData);

					if (folder != null)
					{
						folderStack.RemoveAt(0);

						userState["folder"] = folder;

						this.Live.Client.GetCompleted += new EventHandler<LiveOperationCompletedEventArgs>(UploadOnGetCompleted);
						this.Live.Client.GetAsync(folder.Id + "/files", userState);
					}
					else
					{
						Dictionary<string, object> postData = new Dictionary<string, object>();
						postData.Add("name", folderStack[0]);

						folderStack.RemoveAt(0);

						this.Live.Client.PostCompleted += new EventHandler<LiveOperationCompletedEventArgs>(UploadOnPostCompleted);

						if (userState["folder"] != null)
						{
							folder = userState["folder"] as SkyDriveDataInformation;
							this.Live.Client.PostAsync(folder.Id, postData, userState);
						}
						else
						{
							this.Live.Client.PostAsync((String)userState["parentFolderId"], postData, userState);
						}
					}

					return;
				}
			}
			else
			{
				EFSlogsystem.Logger.WriteError(this.GetType(), "Verbindungsfehler während des Uploads", e.Error);

				((FileStream)userState["fileStream"]).Flush();
				((FileStream)userState["fileStream"]).Dispose();

				if (this.UploadFailed != null)
				{
					this.UploadFailed.Invoke(sender, new SkyDriveClientArgs { operationResult = OperationResult.LiveError });
				}

                this.IsOperation = false;
			}
		}

		private void UploadOnPostCompleted(object sender, LiveOperationCompletedEventArgs e)
		{
			this.Live.Client.PostCompleted -= UploadOnPostCompleted;

			Dictionary<string, object> userState = (Dictionary<string, object>)e.UserState;

			if (e.Error == null)
			{
				SkyDriveDataInformation folder = new SkyDriveDataInformation((Dictionary<string, object>)e.Result);

				EFSlogsystem.Logger.WriteVerbose(this.GetType(), "Ordner<" + folder.Name + "> auf SkyDrive erstellt");
				
				userState["folder"] = folder;

				this.Live.Client.GetCompleted += new EventHandler<LiveOperationCompletedEventArgs>(UploadOnGetCompleted);
				this.Live.Client.GetAsync(folder.Id + "/files", userState);
			}
			else
			{
				EFSlogsystem.Logger.WriteError(this.GetType(), "Abfragefehler aufgetreten", e.Error);

				((FileStream)userState["fileStream"]).Flush();
				((FileStream)userState["fileStream"]).Dispose();

				if (this.UploadFailed != null)
				{
					this.UploadFailed.Invoke(sender, new SkyDriveClientArgs { operationResult = OperationResult.LiveError });
				}

                this.IsOperation = false;
			}
		}

		private void OnUploadCompleted(object sender, LiveOperationCompletedEventArgs e)
        {
			this.Live.Client.UploadCompleted -= OnUploadCompleted;

			Dictionary<string, object> userState = (Dictionary<string, object>)e.UserState;
			((Stream)userState["fileStream"]).Flush();
			((Stream)userState["fileStream"]).Dispose();

			EFSlogsystem.Logger.WriteInfo(this.GetType(), "Datei<" + (string)userState["fileName"] + "> erfolgreich hochgeladen");

			if (this.UploadCompleted != null)
			{
				this.UploadCompleted.Invoke(sender, e);
			}

            this.IsOperation = false;
		}
		#endregion

		#region Download
		public Boolean DownloadFile(FileStream fileStream, String fileName, PathTool folderPath = null, Boolean askOverwrite = true)
		{
            if (!this.IsOperation)
            {
                if (askOverwrite)
                {
                    if (MessageBoxResult.Cancel == MessageBox.Show(EFSlanguage.SkyDriveUploadOverwrite_MessageBox_Content, String.Empty, MessageBoxButton.OKCancel))
                    {
                        fileStream.Flush();
                        fileStream.Dispose();
                        return false;
                    }
                }

                Dictionary<string, object> userState = new Dictionary<string, object>();
                userState.Add("fileStream", fileStream);
                userState.Add("fileName", fileName);
                userState.Add("folderStack", folderPath.GetPathList());
                userState.Add("parentFolderId", "me/skydrive");

                this.Live.Client.GetCompleted += new EventHandler<LiveOperationCompletedEventArgs>(DownloadOnGetCompleted);
                this.Live.Client.GetAsync(userState["parentFolderId"] + "/files", userState);

                this.IsOperation = true;

                return true;
            }

            return false;
		}

		private void DownloadOnGetCompleted(object sender, LiveOperationCompletedEventArgs e)
		{
			this.Live.Client.GetCompleted -= DownloadOnGetCompleted;

			Dictionary<string, object> userState = (Dictionary<string, object>)e.UserState;

			if (e.Error == null)
			{
				SkyDriveDataRepository skyData = new SkyDriveDataRepository(e.Result);
				List<string> folderStack = (List<string>)userState["folderStack"];

				if (folderStack.Count == 0)
				{
					SkyDriveDataInformation file = this.CheckItemExists((string)userState["fileName"], skyData);

					if (file != null)
					{
						this.Live.Client.DownloadCompleted += new EventHandler<LiveDownloadCompletedEventArgs>(OnDownloadCompleted);
						this.Live.Client.DownloadAsync(file.Id + "/content", userState);
						return;
					}
					else
					{
						EFSlogsystem.Logger.WriteError(this.GetType(), "Download abgebrochen (Datei nicht vorhanden)");

						((FileStream)userState["fileStream"]).Flush();
						((FileStream)userState["fileStream"]).Dispose();

						if (this.DownloadFailed != null)
						{
							this.DownloadFailed.Invoke(sender, new SkyDriveClientArgs { operationResult = OperationResult.FileNotExist });
						}
					}
					return;
				}
				else if (folderStack.Count > 0)
				{
					SkyDriveDataInformation folder = this.CheckItemExists(folderStack[0], skyData);
					folderStack.RemoveAt(0);

					if (folder != null)
					{
						this.Live.Client.GetCompleted += new EventHandler<LiveOperationCompletedEventArgs>(DownloadOnGetCompleted);
						this.Live.Client.GetAsync(folder.Id + "/files", userState);
						return;
					}
					else
					{
						EFSlogsystem.Logger.WriteError(this.GetType(), "Download abgebrochen (Dateipfad nicht vorhanden)");

						((FileStream)userState["fileStream"]).Flush();
						((FileStream)userState["fileStream"]).Dispose();

						if (this.DownloadFailed != null)
						{
							this.DownloadFailed.Invoke(sender, new SkyDriveClientArgs { operationResult = OperationResult.PathNotExist });
						}
					}
					return;
				}
			}
			else
			{
				EFSlogsystem.Logger.WriteError(this.GetType(), "Verbindungsfehler während des Downloads", e.Error);

				((FileStream)userState["fileStream"]).Flush();
				((FileStream)userState["fileStream"]).Dispose();

				if (this.DownloadFailed != null)
				{
					this.DownloadFailed.Invoke(sender, new SkyDriveClientArgs { operationResult = OperationResult.LiveError });
				}

                this.IsOperation = false;
			}
		}

		private void OnDownloadCompleted(object sender, LiveDownloadCompletedEventArgs e)
        {
			this.Live.Client.DownloadCompleted -= OnDownloadCompleted;

			Dictionary<string, object> userState = (Dictionary<string, object>)e.UserState;
			FileStream fileSream = (FileStream)userState["fileStream"];

			if (e.Error == null)
			{
				EFSlogsystem.Logger.WriteInfo(this.GetType(), "Download abgeschlossen");

				using (MemoryStream ms = (MemoryStream)e.Result)
				{
					ms.WriteTo(fileSream);
				}

				fileSream.Flush();
				fileSream.Dispose();

				if (this.DownloadCompleted != null)
				{
					this.DownloadCompleted.Invoke(sender, e);
				}

                this.IsOperation = false;
			}
			else
			{
				EFSlogsystem.Logger.WriteError(this.GetType(),"Download fehlgeschlagen", e.Error);

				fileSream.Flush();
				fileSream.Dispose();

				if (this.DownloadFailed != null)
				{
					this.DownloadFailed.Invoke(sender, new SkyDriveClientArgs { operationResult = OperationResult.DownloadFailed });
				}

                this.IsOperation = false;
			}
        }

		#endregion
	}

	public enum OperationResult { FileNotExist, PathNotExist, LiveError, DownloadFailed, UploadFailed }
}
