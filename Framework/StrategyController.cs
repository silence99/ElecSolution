using System.Collections.Generic;

namespace Framework
{
	public class StrategyController : Strategy
	{
		protected IList<Strategy> Strategies { get; set; }
		protected NotificationObject UIModel;
		public StrategyController(Strategy parent, NotificationObject uiModel, IList<Strategy> strategies) :
			base(parent)
		{
			Strategies = strategies;

			UIModel = uiModel;
			InitializationUiModel();
			RegisterProperties();
			PostInitializationUiModel();
		}


		public virtual void InitializationUiModel() { }

		public virtual void PostInitializationUiModel() { }

		public override void RegisterProperties()
		{
			if (Strategies != null)
			{
				foreach (var strategy in Strategies)
				{
					strategy.RegisterProperties();
				}
			}
		}
	}
}
