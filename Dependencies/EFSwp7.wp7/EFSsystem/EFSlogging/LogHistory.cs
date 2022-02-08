using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace EFSresources.EFSlogging
{
    public enum LogStatus
    {
        Nothing, OnInitialize, OnApplicationLifecycle, RegularEnded, Running
    }

	public class LogHistory
	{
        public DateTime Timestamp { set; get; }
        public LogStatus Status { set; get; }

		public ObservableCollection<LogPoint> Logpoints { set; get; }

		public LogHistory() {}

		public LogHistory(DateTime _timestamp)
		{
			this.Timestamp = _timestamp;
            this.Logpoints = new ObservableCollection<LogPoint>();
            this.Status = LogStatus.Nothing;
		}

		public void add(Type _type, DateTime _dt, String _inhalt, LogLevel _level)
        {
			this.add(_type, _dt, _inhalt, null, _level);
        }

		public void add(Type _type, DateTime _dt, String _inhalt, String _zusatz, LogLevel _level)
		{
			LogPoint lp = new LogPoint() { ClassName = _type.Name, Zeitstempel = _dt, Inhalt = _inhalt, Zusatz = _zusatz, Level = _level };
			this.Logpoints.Add(lp);
		}

		public String getHistory()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine(this.Timestamp.ToString());
			sb.AppendLine("Status=" + this.Status.ToString());

			foreach (LogPoint item in Logpoints)
			{
				sb.AppendLine(item.GetString());
			}

			return sb.ToString();
		}

		public String getCrash()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine(this.Timestamp.ToString());
            sb.AppendLine("Status=" + this.Status.ToString());
			sb.AppendLine(this.Logpoints[this.Logpoints.Count - 1].GetString());

			return sb.ToString();
		}
	}
}
