namespace Emergence_WPF.Util
{
	public class ServiceManager
	{
		private static ServiceManager _instance = new ServiceManager();
		private ServiceManager() { }
		public static ServiceManager Instance
		{
			get { return _instance; }
		}

		public T GetService<T>(string serviceName) where T : class
		{
			return ObjectFactory.GetInstance<T>(serviceName);
		}
	}
}
