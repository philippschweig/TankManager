using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using System.Diagnostics;
using EFSresources.EFSlibrary;
using EFSresources.EFSlogging;
using System.Windows;
using System.Windows.Resources;
using EFSresources.Library;

namespace EFSresources.EFSdatastorage
{
	public class DataStorage
	{
		public static void WriteObject(String filepath, Object o)
        {
			WriteObject(filepath, o, FileMode.OpenOrCreate);
        }

		public static void WriteObject(String filepath, Object o, FileMode mode)
		{
			IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();
			EFStools.PruefePfad(filepath, isoStore);

			try
			{
				using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(filepath, mode, isoStore))
				{
					XmlSerializer xser = new XmlSerializer(o.GetType());

					TextWriter tx = new StreamWriter(stream);
					xser.Serialize(tx, o);
					tx.Close();
				}
			}
			catch (IsolatedStorageException e)
			{
				EFSlogsystem.Logger.WriteError(typeof(DataStorage), "WriteObject", e);
				Debug.WriteLine(e.Message, e.StackTrace);
			}
			catch (FileNotFoundException e)
			{
				EFSlogsystem.Logger.WriteError(typeof(DataStorage), "WriteObject", e);
				Debug.WriteLine(e.Message, e.StackTrace);
			}
		}

		public static T ReadObject<T>(String filepath)
		{
			IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

			if (isoStore.FileExists(filepath))
			{
				try
				{
					using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(filepath, FileMode.Open, isoStore))
					{
						XmlSerializer xser = new XmlSerializer(typeof(T));

						TextReader rx = new StreamReader(stream);
						T o = (T)xser.Deserialize(rx);
						rx.Close();
						stream.Dispose();

						return o;
					}
				}
				catch (IsolatedStorageException e)
				{
					EFSlogsystem.Logger.WriteError(typeof(DataStorage), "ReadObject<T>", e);
					Debug.WriteLine(e.Message, e.StackTrace);
				}
				catch (FileNotFoundException e)
				{
					EFSlogsystem.Logger.WriteError(typeof(DataStorage), "ReadObject<T>", e);
					Debug.WriteLine(e.Message, e.StackTrace);
				}
				catch (InvalidOperationException e)
				{
					EFSlogsystem.Logger.WriteError(typeof(DataStorage), "ReadObject<T>", e);
					Debug.WriteLine(e.Message, e.StackTrace);
				}
			}

			return default(T);
		}
		public static Object ReadObject(String filepath, Type typ)
		{
			IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

			if (isoStore.FileExists(filepath))
			{
				try
				{
					using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(filepath, FileMode.Open, isoStore))
					{
						XmlSerializer xser = new XmlSerializer(typ);

						TextReader rx = new StreamReader(stream);
						Object o = xser.Deserialize(rx);
						rx.Close();
						stream.Dispose();

						return o;
					}
				}
				catch (IsolatedStorageException e)
				{
					EFSlogsystem.Logger.WriteError(typeof(DataStorage), "ReadObject", e);
					Debug.WriteLine(e.Message, e.StackTrace);
				}
				catch (FileNotFoundException e)
				{
					EFSlogsystem.Logger.WriteError(typeof(DataStorage), "ReadObject", e);
					Debug.WriteLine(e.Message, e.StackTrace);
				}
				catch (InvalidOperationException e)
				{
					EFSlogsystem.Logger.WriteError(typeof(DataStorage), "ReadObject", e);
					Debug.WriteLine(e.Message, e.StackTrace);
				}
			}

			return null;
		}

		public static void DeleteObject(String filepath)
		{
			DeleteFile(filepath);
		}

		public static IsolatedStorageFileStream ReadFile(String filePath)
		{
			IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

			if (isoStore.FileExists(filePath))
			{
				try
				{
					//return new IsolatedStorageFileStream(filepath, FileMode.Open, isoStore);
					return isoStore.OpenFile(filePath, FileMode.Open);
				}
				catch (IsolatedStorageException e)
				{
					EFSlogsystem.Logger.WriteError(typeof(DataStorage), "ReadFile", e);
					Debug.WriteLine(e.Message, e.StackTrace);
				}
				catch (FileNotFoundException e)
				{
					EFSlogsystem.Logger.WriteError(typeof(DataStorage), "ReadFile", e);
					Debug.WriteLine(e.Message, e.StackTrace);
				}
			}

			return null;
		}
		public static Stream ReadResource(Uri resourceUri)
		{
			Application current = Application.Current;

			StreamResourceInfo sri = Application.GetResourceStream(resourceUri);

			if (sri != null)
			{
				return sri.Stream;
			}

			return null;
		}

		public static IsolatedStorageFileStream WriteFile(String filePath)
		{
			IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

			//return new IsolatedStorageFileStream(filePath, FileMode.Create, isoStore);
			return isoStore.OpenFile(filePath, FileMode.Create);
		}

		public static void DeleteFile(String filepath)
		{
			IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

			try
			{
				if (isoStore.FileExists(filepath))
				{
					isoStore.DeleteFile(filepath);
				}
				else
				{
					throw new FileNotFoundException("Datei " + filepath + " nicht gefunden.");
				}
			}
			catch (IsolatedStorageException e)
			{
				EFSlogsystem.Logger.WriteError(typeof(DataStorage), "DeleteFile", e);
				Debug.WriteLine(e.Message, e.StackTrace);
			}
			catch (FileNotFoundException e)
			{
				EFSlogsystem.Logger.WriteError(typeof(DataStorage), "DeleteFile", e);
				Debug.WriteLine(e.Message, e.StackTrace);
			}
		}

		public static List<String> ListFiles(String dir, String extension = null)
        {
			IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

			if (extension == null)
			{
				extension = "";
			}
			else
			{
				extension = "." + extension;
			}

			if (isoStore.DirectoryExists(dir))
			{
				return new List<String>(isoStore.GetFileNames(Path.Combine(dir, "*" + extension)));
			}
			else
			{
				return new List<String>();
			}
			
        }

		public static List<String> ListFilenames(String dir, String extension = null)
		{
			List<String> files = ListFiles(dir, extension);

			for (int i = 0; i < files.Count; i++)
			{
				files[i] = files[i].Remove(files[i].LastIndexOf('.'));
			}

			return files;
		}
	}
}
