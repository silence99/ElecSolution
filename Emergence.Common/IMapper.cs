using Framework;

namespace Emergence.Common
{
    public interface IMapper<Model, ViewModel>
        where ViewModel : NotificationObject
    {
        void MapToViewModel(Model data, ViewModel viewModel);
        void MapToDataModel(ViewModel viewModel, Model model);
    }
}
