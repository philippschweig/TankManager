using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using EFSresources.EFSdatastorage;
using EFSresources.EFSconverter;

namespace EFSresources.EFSlogging
{
	public class EFSlogsystem
	{
		public static Uri LogViewUri = new Uri("/EFSsystem;component/EFSlogging/View/LogView.xaml", UriKind.Relative);

		private static EFSlogsystem _logger = null;
		public static EFSlogsystem Logger
		{
			get
			{
				if (_logger == null)
				{
					_logger = new EFSlogsystem();
				}

				return _logger;
			}
		}

		private String filedir = "EFSlog";
		private String filename;
		private String extension = "xml";
		private Int32 _logcount = 3;

		public Int32 logcount
		{
			set
			{
				if (value > 0)
				{
					_logcount = value;
				}
			}

			get { return _logcount; }
		}
		public LogLevel level = LogLevel.Verbose;
		public LogHistory log { private set; get; }

		private const string LOGFORMAT = "{0} class - {1}: {2}";

		public EFSlogsystem() { }

		public void InitLogging(LogLevel _level = LogLevel.Verbose)
		{
			this.deleteLastLogs();

            DateTime dt = DateTime.Now;
			this.log = new LogHistory(dt);
            this.log.Status = LogStatus.OnInitialize;
			this.filename = dt.ToString("yyyy_MM_dd_HH_mm_ss");

			this.level = LogLevel.Info;
			this.WriteInfo(this.GetType(), "EFSlogsystem initialisert");
			this.level = _level;
		}

        public void StartLogging()
        {
            this.log.Status = LogStatus.Running;

            LogLevel temp = this.level;
            this.level = LogLevel.Info;
            this.WriteInfo(this.GetType(), "EFSlogsystem gestartet");
            this.level = temp;
        }

		public void ActivateLogging()
        {
            this.log.Status = LogStatus.Running;

			LogLevel temp = this.level;
			this.level = LogLevel.Info;
			this.WriteInfo(this.GetType(), "EFSlogsystem aktiviert");
			this.level = temp;
        }

		public void DeactivateLogging()
		{
            this.log.Status = LogStatus.RegularEnded;

			LogLevel temp = this.level;
			this.level = LogLevel.Info;
			this.WriteInfo(this.GetType(), "EFSlogsystem deaktiviert/beendet");
			this.level = temp;
		}

		public void EndLogging()
		{
            this.log.Status = LogStatus.RegularEnded;

			LogLevel temp = this.level;
			this.level = LogLevel.Info;
			this.WriteInfo(this.GetType(), "EFSlogsystem beendet");
			this.level = temp;
		}

        public void ChangeStatus(LogStatus status)
        {
            this.log.Status = status;
            DataStorage.WriteObject(Path.Combine(this.filedir, this.filename + "." + this.extension), this.log);
        }

		public Boolean RegularEnded()
		{
			LogHistory lastLog = this.lastLog();
			if (lastLog != null)
			{
                if (lastLog.Status == LogStatus.Running && lastLog.Logpoints.Count > 0) return false;
			}

			return true;
		}

		private void Write(Type _type, String _inhalt, LogLevel _level, Object _zusatz = null)
        {
			if ((int)this.level > -1)
			{
				DateTime dt = DateTime.Now;

				Debug.WriteLine(String.Format(LOGFORMAT, _type.Name, dt, _inhalt));

				if (_zusatz == null)
				{
					this.log.add(_type, dt, _inhalt, _level);
				}
				else
				{
					String zusatz = Objects.ConvertToString(_zusatz);
					this.log.add(_type, dt, _inhalt, zusatz, _level);
					Debug.WriteLine(zusatz);
				}

				DataStorage.WriteObject(Path.Combine(this.filedir, this.filename + "." + this.extension), this.log);
			}
        }

		public void WriteError(Type _type, String _inhalt, Object _zusatz = null)
        {
			if ((int)this.level >= 0)
			{
				this.Write(_type, _inhalt, LogLevel.Error, _zusatz);
			}
        }

		public void WriteWarning(Type _type, String _inhalt, Object _zusatz = null)
		{
			if ((int)this.level >= 1)
			{
				this.Write(_type, _inhalt, LogLevel.Warning, _zusatz);
			}
		}

		public void WriteInfo(Type _type, String _inhalt, Object _zusatz = null)
		{
			if ((int)this.level >= 2)
			{
				this.Write(_type, _inhalt, LogLevel.Info, _zusatz);
			}
		}

		public void WriteVerbose(Type _type, String _inhalt, Object _zusatz = null)
		{
			if ((int)this.level >= 3)
			{
				this.Write(_type, _inhalt, LogLevel.Verbose, _zusatz);
			}
		}

		public List<String> listLogs()
		{
			return DataStorage.ListFilenames(this.filedir, this.extension);
		}

		public LogHistory getLog(String _filename)
		{
			return (LogHistory)DataStorage.ReadObject(Path.Combine(this.filedir, _filename + "." + this.extension), typeof(LogHistory));
		}

		public ObservableCollection<LogHistory> getLogs()
		{
			List<String> logfiles = listLogs();
			ObservableCollection<LogHistory> logs = new ObservableCollection<LogHistory>();
			
			for (int i = 0; i < logfiles.Count; i++)
			{
				if (logfiles[i] != this.filename)
				{
					logs.Add(getLog(logfiles[i]));
				}
			}

			logs.Add(this.log);

			return logs;
		}

		public LogHistory lastLog()
		{
			List<String> logliste = this.listLogs();

			if (logliste.Count > 1)
			{
				logliste.Sort();
				return this.getLog(logliste[logliste.Count - 2]);
			}

			return null;
		}

		public void deleteLastLogs()
		{
			List<String> files = DataStorage.ListFiles(filedir, this.extension);
			files.Sort();

			while (files.Count > 0 && files.Count >= this.logcount)
			{
				DataStorage.DeleteObject(Path.Combine(this.filedir, files[0]));
				files.RemoveAt(0);
			}
		}
	}
}
