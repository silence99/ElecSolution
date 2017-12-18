using Spring.Context;
using Spring.Context.Support;

namespace Emergence_WPF.Util
{
	public static class ObjectFactory
	{
		private static IApplicationContext Context { get; set; }

		public static T GetInstance<T>(string instanceName) where T: class
		{
			if (Context == null)
			{
				Context = ContextRegistry.GetContext();
			}

			return Context.GetObject(instanceName) as T;
		}

	}
}
