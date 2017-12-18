using System.Collections.Generic;

namespace Framework
{
	public class StrategyController<T> : StrategyController
	{
		protected T UIModel;
		public StrategyController(Strategy parent, T uiModel, IList<Strategy> strategies) :
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

	public abstract class StrategyController : Strategy
	{
		protected IList<Strategy> Strategies { get; set; }
		public StrategyController(Strategy parent) : base(parent)
		{
		}

	}
}
