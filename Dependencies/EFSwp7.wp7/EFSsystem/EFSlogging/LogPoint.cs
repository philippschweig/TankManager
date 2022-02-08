using System;

namespace EFSresources.EFSlogging
{
	public class LogPoint
	{
		public String ClassName { set; get; }
		public DateTime Zeitstempel { set; get; }
		public String Inhalt { set; get; }
		public String Zusatz { set; get; }
		public LogLevel Level { set; get; }

		public string GetString()
		{
			String zusatz = String.Empty;
			if (this.Zusatz != null && this.Zusatz != String.Empty)
			{
				zusatz = this.Zusatz + "\n";
			}

			return "--------- * LogPoint * ---------\n" + this.ClassName + "; " + this.Zeitstempel.ToString() + "; " + this.Level.ToString() + ";\n" + this.Inhalt + ";\n" + zusatz + "--------- # LogPoint # ---------\n";
		}
	}
}
