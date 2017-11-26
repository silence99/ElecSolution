using System.Collections.Generic;

namespace Framework
{
    public class StrategyController<T> : Strategy
    {
        protected IList<Strategy> _stategies = null;
        protected T UIModel;
        public StrategyController(Strategy parent, T uiModel, IList<Strategy> strategies) :
            base(parent)
        {
            _stategies = strategies;

            UIModel = uiModel;
            InitializationUiModel();
            RegisterProperties();
            PostInitializationUiModel();
        }


        public virtual void InitializationUiModel() { }

        public virtual void PostInitializationUiModel() { }

        public override void RegisterProperties()
        {
            if (_stategies != null)
            {
                foreach (var strategy in _stategies)
                {
                    strategy.RegisterProperties();
                }
            }
        }
    }
}
