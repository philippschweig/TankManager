namespace TankManager.Lokalisierung
{
	public class LanguageProvider
	{
		private readonly Translation _resources = new Translation();

		public Translation Resources
		{
			get { return _resources; }
		} 
	}
}
