using System.Diagnostics;

namespace EFSresources.EFSdebugging
{
    public static class EFSdebug
    {
		public static void ObservableCollection<T>(object oc)
        {
            //oc as ObservableCollection<T>;
        }

		public static void DebugFunction(string function_name, object content)
		{
			EFSdebug.DebugFunction(function_name, content.ToString());
		}

		public static void DebugFunction(string function_name, string content)
		{
			Debug.WriteLine("Aufgerufen von: {0}\n#Inhalt#\n{1}", function_name, content);
		}


    }
}
